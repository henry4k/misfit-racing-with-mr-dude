using System;
using UnityEngine;
using UnityEngine.UI;

public enum FillBar {
	linear,
	exponential,
	easeOut,
	easeIn,
	smoothStep
}

public class Bar : MonoBehaviour {

	[SerializeField]
	Image _amountImage;

	float _targetAmount;
	float _startAmount;
	float _changeTime;
	float _currentLerpTime;
	float _currentLerpPercent;

    [SerializeField]
    FillBar _fillType;

	public void Change(float newPercent, float changeTime) {
		_startAmount = _amountImage.fillAmount;
        _targetAmount = newPercent;
		_changeTime = changeTime;
		_currentLerpTime = 0.0f;
    }

	public void Set(float newPercent) {
		_amountImage.fillAmount = Mathf.Clamp01(newPercent);
		_targetAmount = newPercent;
	}

	void Update() {
        if (_changeTime == 0.0f) return;
		_currentLerpTime += Time.deltaTime;
		_currentLerpTime = Mathf.Clamp(_currentLerpTime, 0.0f, _changeTime);
		_currentLerpPercent = _currentLerpTime / _changeTime;
		_currentLerpPercent = ApplyFillType(_currentLerpPercent, _fillType);
		_amountImage.fillAmount = Mathf.Lerp(_startAmount,
			_targetAmount, _currentLerpPercent);
	}

	float ApplyFillType(float currentLerpPercent, FillBar fillType) {
		float resultPercent = 0.0f;
		switch (_fillType) {
			case FillBar.linear:
				resultPercent = currentLerpPercent;
				break;
			case FillBar.exponential:
				resultPercent = _currentLerpPercent * _currentLerpPercent;
				break;
			case FillBar.easeIn:
				resultPercent = 1.0f - Mathf.Cos(_currentLerpPercent * Mathf.PI * 0.5f);
				break;
			case FillBar.easeOut:
				resultPercent = Mathf.Sin(_currentLerpPercent * Mathf.PI * 0.5f);
				break;
			case FillBar.smoothStep:
				resultPercent = _currentLerpPercent * _currentLerpPercent * (3.0f - 2.0f * _currentLerpPercent);
				break;
		}
		return resultPercent;
	}
}
