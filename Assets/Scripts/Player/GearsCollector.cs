using UnityEngine;
using UnityEngine.Events;

public class GearsCollector : MonoBehaviour
{
    private int _currentGearsNumber;

    public event UnityAction<int> GearsNumberChanged;

    public void AddGear()
    {
        _currentGearsNumber++;
        GearsNumberChanged?.Invoke(_currentGearsNumber);
    }
}