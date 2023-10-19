using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class DescriptionAnimationTextSettings
{
    [SerializeField] private float _fadeEndValue = 1f;
    [SerializeField] private float _showDuration = 0.3f;
    [SerializeField] private Ease _fadeEase = Ease.Linear;
    [SerializeField] private Ease _scalingEase = Ease.OutBack;

    public float FadeEndValue => _fadeEndValue;
    public float ShowDuration => _showDuration;
    public Ease FadeEase => _fadeEase;
    public Ease ScalingEase => _scalingEase;
}
