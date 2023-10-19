using DG.Tweening;
using TMPro;
using UnityEngine;

public class DescriptionAnimationText : DescriptionAnimationVariant
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private DescriptionAnimationTextSettings _settings;

    public override void Initialize()
    {
        _text.alpha = 0f;
        _text.rectTransform.localScale = Vector3.zero;
    }

    public override Sequence Play()
    {
        var sequence = DOTween.Sequence();
        sequence.Join(_text.DOFade(_settings.FadeEndValue, _settings.ShowDuration).SetEase(_settings.FadeEase));
        sequence.Join(_text.rectTransform.DOScale(Vector3.one, _settings.ShowDuration).SetEase(_settings.ScalingEase));
        return sequence;
    }
}
