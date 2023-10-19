using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class DescriptionAnimationButtonSettings
{
    [SerializeField] private float _startRightOffset = 50f;
    [SerializeField] private float _showDuration = 0.3f;
    [SerializeField] private Ease _fadeEase = Ease.Linear;
    [SerializeField] private Ease _transitionEase = Ease.OutBack;

    public float StartRightOffset => _startRightOffset;
    public float ShowDuration => _showDuration;
    public Ease FadeEase => _fadeEase;
    public Ease TransitionEase => _transitionEase;
}
