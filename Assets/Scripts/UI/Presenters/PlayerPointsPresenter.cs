using TMPro;
using UnityEngine;

public class PlayerPointsPresenter : MonoBehaviour
{
    [SerializeField] private DefaultButtonView _earnPointsButtonView;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private int _addedValue = 1;
    
    private PlayerPointsModel _playerPointsModel;

    public void Initialize(PlayerPointsModel playerPointsModel)
    {
        _playerPointsModel = playerPointsModel;
        _playerPointsModel.Changed += OnPlayerPointsChanged;
        _earnPointsButtonView.Clicked += OnEarnPointsButtonClicked;
    }

    public void Uninitialize()
    {
        _playerPointsModel.Changed -= OnPlayerPointsChanged;
        _earnPointsButtonView.Clicked -= OnEarnPointsButtonClicked;
        _playerPointsModel = null;
    }

    private void OnEarnPointsButtonClicked()
    {
        _playerPointsModel.Add(_addedValue);
    }

    private void OnPlayerPointsChanged()
    {
        SetTitle(_playerPointsModel.CurrentPointsCount.ToString());
    }

    private void SetTitle(string title)
    {
        _title.text = title;
    }
}
