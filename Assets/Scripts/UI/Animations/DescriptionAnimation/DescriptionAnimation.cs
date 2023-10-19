using UnityEngine;
using DG.Tweening;

public class DescriptionAnimation : MonoBehaviour
{
    [SerializeField] private DescriptionAnimationVariant[] _animationVariants;
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Play()
    {
        var sequence = DOTween.Sequence();
        foreach (var animationVariant in _animationVariants)
        {
            animationVariant.Initialize();
            if (animationVariant.SequenceType == DescriptionAnimationSequenceType.Append)
            {
                sequence.Append(animationVariant.Play());
            }
            else
            {
                sequence.Join(animationVariant.Play().SetDelay(animationVariant.JoinDelayDuration));
            }
        }
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
    }
}
