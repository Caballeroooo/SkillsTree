using DG.Tweening;
using UnityEngine;

public class DescriptionAnimationButton : DescriptionAnimationVariant
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private DescriptionAnimationButtonSettings _settings;

    private Vector2 _startPosition;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = transform as RectTransform;
    }

    public override void Initialize()
    {
        _startPosition = _rectTransform.anchoredPosition;
        _rectTransform.anchoredPosition = _startPosition + Vector2.right * _settings.StartRightOffset;
        _canvasGroup.alpha = 0f;
    }

    public override Sequence Play()
    {
        var sequence = DOTween.Sequence();
        sequence.Join(_rectTransform.DOAnchorPos(_startPosition, _settings.ShowDuration).SetEase(_settings.TransitionEase));
        sequence.Join(_canvasGroup.DOFade(1, _settings.ShowDuration).SetEase(_settings.FadeEase));
        return sequence;
    }
}