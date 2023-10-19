using System;
using UnityEngine;

public class BaseSkillUIView : MonoBehaviour
{
    [SerializeField] private SkillType _type;

    public event Action<SkillType> Clicked;
    public event Action LearnButtonClicked;
    public event Action ForgotButtonClicked;
    
    public SkillType Type => _type;

    public virtual void Initialize(SkillConfig config)
    {
    }

    public virtual void ShowDescription()
    {
    }

    public virtual void HideDescription()
    {
    }

    public virtual void SetTitle(string title)
    {
    }
    
    public virtual void SetDefaultState()
    {
    }

    public virtual void SetDisableState()
    {
    }

    public virtual void SetLearnedState()
    {
    }

    public virtual void EnableLearnButton()
    {
    }

    public virtual void DisableLearnButton()
    {
    }

    public virtual void EnableForgotButton()
    {
    }

    public virtual void DisableForgotButton()
    {
    }

    protected void OnLearnButtonClicked()
    {
        LearnButtonClicked?.Invoke();
    }

    protected void OnForgotButtonClicked()
    {
        ForgotButtonClicked?.Invoke();
    }

    protected void OnButtonClicked()
    {
        Clicked?.Invoke(Type);
    }
}