using System;

public class PlayerPointsModel
{
    private int _currentPointsCount;

    public event Action Changed;

    public int CurrentPointsCount => _currentPointsCount;
    
    public void Add(int count)
    {
        _currentPointsCount += count;
        Changed?.Invoke();
    }

    public void Remove(int count)
    {
        _currentPointsCount -= count;
        Changed?.Invoke();
    }
}
