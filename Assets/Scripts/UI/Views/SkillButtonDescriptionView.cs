using System;
using TMPro;
using UnityEngine;

public class SkillButtonDescriptionView : MonoBehaviour
{
    [SerializeField] private DefaultButtonView _learnButton;
    [SerializeField] private DefaultButtonView _forgotButton;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private DescriptionAnimation _animation;

    public event Action LearnButtonClicked;
    public event Action ForgotButtonClicked;

    private void OnEnable()
    {
        _learnButton.Clicked += OnLearnButtonClicked;
        _forgotButton.Clicked += OnForgotButtonClicked;
    }

    private void Start()
    {
        _animation.Hide();
    }

    private void OnDisable()
    {
        _learnButton.Clicked -= OnLearnButtonClicked;
        _forgotButton.Clicked -= OnForgotButtonClicked;
    }

    public void SetCost(string cost)
    {
        _cost.text = string.Format(_cost.text, cost);
    }

    public void SetName(string name)
    {
        _name.text = name;
    }

    public void Show()
    {
        _animation.Show();
        _animation.Play();
    }

    public void Hide()
    {
        _animation.Hide();
    }

    public void EnableLearnButton()
    {
        _learnButton.Enable();
    }

    public void DisableLearnButton()
    {
        _learnButton.Disable();
    }

    public void EnableForgotButton()
    {
        _forgotButton.Enable();
    }

    public void DisableForgotButton()
    {
        _forgotButton.Disable();
    }

    private void OnLearnButtonClicked()
    {
        LearnButtonClicked?.Invoke();
    }

    private void OnForgotButtonClicked()
    {
        ForgotButtonClicked?.Invoke();
    }
}