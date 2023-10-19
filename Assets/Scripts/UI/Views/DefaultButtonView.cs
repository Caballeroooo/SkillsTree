using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class DefaultButtonView : MonoBehaviour
{
    private Button _button;

    public event Action Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public virtual void Enable()
    {
        _button.interactable = true;
    }

    public virtual void Disable()
    {
        _button.interactable = false;
    }

    private void OnClick()
    {
        Clicked?.Invoke();
    }
}
