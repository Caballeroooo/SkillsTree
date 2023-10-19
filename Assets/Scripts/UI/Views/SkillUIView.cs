using UnityEngine;

public class SkillUIView : BaseSkillUIView
{
    [SerializeField] private SkillButtonView _skillButtonView;
    [SerializeField] private SkillButtonDescriptionView _description;
    
    private void OnEnable()
    {
        _skillButtonView.AddListener(OnButtonClicked);
        _description.LearnButtonClicked += OnLearnButtonClicked;
        _description.ForgotButtonClicked += OnForgotButtonClicked;
    }

    private void OnDisable()
    {
        _skillButtonView.RemoveListener(OnButtonClicked);
        _description.LearnButtonClicked -= OnLearnButtonClicked;
        _description.ForgotButtonClicked -= OnForgotButtonClicked;
    }
    
    public override void Initialize(SkillConfig config)
    {
        _description.SetCost(config.Cost.ToString());
        _description.SetName(config.Name);
    }

    public override void ShowDescription()
    {
        _description.Show();
    }

    public override void HideDescription()
    {
        _description.Hide();
    }

    public override void SetTitle(string title)
    {
        _skillButtonView.SetTitle(title);
    }

    public override void SetDefaultState()
    {
        _skillButtonView.SetDefaultState();
    }

    public override void SetDisableState()
    {
        _skillButtonView.SetDisableState();
    }

    public override void SetLearnedState()
    {
        _skillButtonView.SetLearnedState();
    }

    public override void EnableLearnButton()
    {
        _description.EnableLearnButton();
    }

    public override void DisableLearnButton()
    {
        _description.DisableLearnButton();
    }

    public override void EnableForgotButton()
    {
        _description.EnableForgotButton();
    }

    public override void DisableForgotButton()
    {
        _description.DisableForgotButton();
    }
}