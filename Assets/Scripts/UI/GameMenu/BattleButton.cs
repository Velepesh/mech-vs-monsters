using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BattleButton : MonoBehaviour
{
    [SerializeField] private RobotBuilder _robotBuilder;

    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        Lock();
        _robotBuilder.BodySelected += OnBodySelected;
    }

    private void OnDisable()
    {
        _robotBuilder.BodySelected -= OnBodySelected;
    }

    private void OnBodySelected()
    {
        Unock();
    }

    private void Lock()
    {
        _button.interactable = false;
    }

    private void Unock()
    {
        _button.interactable = true;
    }
}