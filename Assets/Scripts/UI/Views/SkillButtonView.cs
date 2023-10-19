using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private ButtonAnimation _buttonAnimation;
    [SerializeField] private Color _enableColor;
    [SerializeField] private Color _disableColor;
    [SerializeField] private Color _learnedColor;

    public void AddListener(Action action)
    {
        _button.onClick.AddListener(() => action?.Invoke());
    }

    public void RemoveListener(Action action)
    {
        _button.onClick.RemoveListener(() => action?.Invoke());
    }

    public void SetTitle(string title)
    {
        _title.text = title;
    }

    public void SetDefaultState()
    {
        _image.color = _enableColor;
        EnableButton();
    }

    public void SetDisableState()
    {
        _image.color = _disableColor;
        DisableButton();
    }

    public void SetLearnedState()
    {
        _image.color = _learnedColor;
        EnableButton();
    }

    private void EnableButton()
    {
        _buttonAnimation.Enable();
        _button.interactable = true;
    }

    private void DisableButton()
    {
        _buttonAnimation.Disable();
        _button.interactable = false;
    }
}
