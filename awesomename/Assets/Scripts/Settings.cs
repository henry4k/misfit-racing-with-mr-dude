using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    public float blub;

    [Space(20)]
    [Header("Movement Controls")]
    public KeyCode A;
    public KeyCode D;
    public KeyCode W;
    public KeyCode S;
    public KeyCode Enter;
    public KeyCode Esc;
    public KeyCode Leave;

    [Space(20)]
    public float playerMovementSpeed = 3.5f;
    public float playerNpcTalkingDistance = 1.4f;
}