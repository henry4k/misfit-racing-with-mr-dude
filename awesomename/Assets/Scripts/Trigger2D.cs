/// <copyright file="Trigger2D.cs" author="René Globig">
/// Copyright (c) 11/11/2016 All Right Reserved
/// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public delegate void TriggerAction<T>(T component) where T : Component;

public enum TriggerEventType
{
    Enter,
    Exit,
}

public interface ITriggerEvent
{
    TriggerEventType eventType { get; }
    bool active { get; }
}

public class TriggerEvent<T> : ITriggerEvent where T : Component
{
    public TriggerEvent(TriggerAction<T> callback, TriggerEventType eventType, bool active = true)
    {
        this.eventType = eventType;
        this.active = active;
        _callback = callback;
    }

    public TriggerEventType eventType { get; private set; }
    public bool active { get; set; }

    public void Invoke(T component)
    {
        if (_callback != null)
        {
            _callback(component);
        }
    }

    TriggerAction<T> _callback;
}

[RequireComponent(typeof(Collider2D))]
public class Trigger2D : MonoBehaviour
{

    public Component[] observingComponents { get { return _observingComponents.ToArray(); } }
    public bool active { get { return _collider2D.enabled; } set { _collider2D.enabled = value; } }

    [SerializeField]
    List<Component> _observingComponents = new List<Component>();

    Collider2D _collider2D;

    const string _invokeMethodName = "Invoke";

    Dictionary<Type, IList> _triggerTypeLists = new Dictionary<Type, IList>();
    Queue<KeyValuePair<Type, ITriggerEvent>> _removeQueue = new Queue<KeyValuePair<Type, ITriggerEvent>>();
    Queue<KeyValuePair<Type, ITriggerEvent>> _addQueue = new Queue<KeyValuePair<Type, ITriggerEvent>>();

    public bool ContainsObservingComponent<T>(T component = null) where T : Component
    {
        return _observingComponents.Exists((search) => { return (component) ? component == search : search is T; });
    }

    public TriggerEvent<T> Add<T>(TriggerAction<T> callback, TriggerEventType eventType = TriggerEventType.Enter, bool active = true) where T : Component
    {
        TriggerEvent<T> enterEvent = new TriggerEvent<T>(callback, eventType, active);
        _addQueue.Enqueue(new KeyValuePair<Type, ITriggerEvent>(typeof(T), enterEvent));
        return enterEvent;
    }

    public void Remove<T>(TriggerEvent<T> enterEvent) where T : Component
    {
        _removeQueue.Enqueue(new KeyValuePair<Type, ITriggerEvent>(typeof(T), enterEvent));
    }

    void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CheckQueues();
        TriggerEvent(other, TriggerEventType.Enter);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        CheckQueues();
        TriggerEvent(other, TriggerEventType.Exit);
    }

    void CheckQueues()
    {
        DequeWithAction(_addQueue, Add);
        DequeWithAction(_removeQueue, Remove);
    }

    void DequeWithAction(Queue<KeyValuePair<Type, ITriggerEvent>> queue, Action<KeyValuePair<Type, ITriggerEvent>> action)
    {
        while (queue.Count > 0)
        {
            action(queue.Dequeue());
        }
    }

    void Add(KeyValuePair<Type, ITriggerEvent> addEvent)
    {
        if (!ContainsEvent(addEvent.Key))
        {
            _triggerTypeLists.Add(addEvent.Key, new List<ITriggerEvent>());
        }
        IList list = GetList(addEvent.Key);
        list.Add(addEvent.Value);
    }

    void Remove(KeyValuePair<Type, ITriggerEvent> removeEvent)
    {
        if (ContainsEvent(removeEvent.Key))
        {
            IList list = GetList(removeEvent.Key);
            list.Remove(removeEvent.Value);
            if (list.Count == 0)
            {
                _triggerTypeLists.Remove(removeEvent.Key);
            }
        }
    }

    void TriggerEvent(Collider2D other, TriggerEventType eventType)
    {
        Trigger2D otherTrigger = other.GetComponent<Trigger2D>();
        if (!otherTrigger)
        {
            return;
        }
        InvokeWithComponents(otherTrigger.observingComponents, eventType);
    }

    void InvokeWithComponents(Component[] components, TriggerEventType eventType)
    {
        foreach (Component component in components)
        {
            Type type = component.GetType();
            if (ContainsEvent(type))
            {
                InvokeList(type, eventType, component);
            }
        }
    }

    bool ContainsEvent(Type type)
    {
        return _triggerTypeLists.ContainsKey(type);
    }

    void InvokeList(Type otherType, TriggerEventType evenType, Component otherComponent)
    {
        foreach (ITriggerEvent triggerEvent in GetList(otherType))
        {
            if (triggerEvent.active &&
                triggerEvent.eventType == evenType)
            {
                InvokeItem(triggerEvent, otherComponent);
            }
        }
    }

    IList GetList(Type type)
    {
        IList objectValue;
        _triggerTypeLists.TryGetValue(type, out objectValue);
        return objectValue;
    }

    void InvokeItem(ITriggerEvent triggerEvent, Component other)
    {
        MethodInfo methodInfo = triggerEvent.GetType().GetMethod(_invokeMethodName);
        methodInfo.Invoke(triggerEvent, new object[] { other });
    }
}