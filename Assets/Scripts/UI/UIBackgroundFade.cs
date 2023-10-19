using UnityEngine;
using DG.Tweening;

public class UIBackgroundFade : DefaultButtonView
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _showDuration = 0.2f;
    [SerializeField] private float _endFadeValue = 0.97f;

    private void Start()
    {
        _canvasGroup.alpha = 0f;
        Disable();
    }

    public void Show()
    {
        Enable();
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(_endFadeValue, _showDuration).SetEase(Ease.Linear);
    }

    public void Hide()
    {
        Disable();
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, _showDuration).SetEase(Ease.Linear);
    }

    public override void Enable()
    {
        base.Enable();
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public override void Disable()
    {
        base.Disable();
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}
