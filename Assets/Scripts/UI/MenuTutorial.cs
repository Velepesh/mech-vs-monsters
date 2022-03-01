using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class MenuTutorial : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _menuTutorial;
    [SerializeField] private Image _blackPanel;
    [SerializeField] private RobotBuilder _robotBuilder;
    [SerializeField] private LegButton _legButton;
    [SerializeField] private BodyButton _bodyButton;
    [SerializeField] private LimbShopsHolder _limbShopsHolder;
    [SerializeField] private List<ChooseLimbButton> _otherButtons;

    readonly private string TUTORIAL = "Tutorial";
    private readonly int _tutorialValue = 1;
    private readonly int _withoutTutorialValue = 0;
    private int _tutorial => PlayerPrefs.GetInt(TUTORIAL, 1);

    private void Awake()
    {
        if(_tutorial == _tutorialValue)
            EnableMenuTutorial();
        else
            DisableMenuTutorial();
    }

    private void OnEnable()
    {
        _game.LevelStarted += OnLevelStarted;
        _robotBuilder.LegSelected += OnLegSelected;
        _robotBuilder.BodySelected += OnBodySelected;
        _legButton.Opened += OnOpened;
        _bodyButton.Opened += OnOpened;
    }

    private void OnDisable()
    {
        _game.LevelStarted -= OnLevelStarted;
        _robotBuilder.LegSelected -= OnLegSelected;
        _robotBuilder.BodySelected -= OnBodySelected;
        _legButton.Opened -= OnOpened;
        _bodyButton.Opened -= OnOpened;
    }

    private void OnBodySelected()
    {
        _animator.SetTrigger(AnimatorMenuTutorialController.States.Battle);
        SaveTutorialValue(_withoutTutorialValue);
        UnlockOtherButton();

        DisableAllButtouns();
    }

    private void SaveTutorialValue(int value)
    {
        PlayerPrefs.SetInt(TUTORIAL, value);
    }

    private void OnLegSelected()
    {
        _animator.SetTrigger(AnimatorMenuTutorialController.States.BodyButton);
        UnlockOtherButton();

        _legButton.GetComponent<Button>().interactable = false;
    }

    private void OnLevelStarted()
    {
        DisableMenuTutorial();
    }

    private void EnableMenuTutorial()
    {
        _animator.enabled = true;
        _menuTutorial.SetActive(true);
        _blackPanel.gameObject.SetActive(true);
    }

    private void OnOpened(LimbShop shop)
    {
        _animator.SetTrigger(AnimatorMenuTutorialController.States.SelectLimb);

        BlockOtherButton();
        _limbShopsHolder.TurnOffEmptySpaceButton();
    }

    private void DisableMenuTutorial()
    {
        EnableAllButtouns();
        _menuTutorial.SetActive(false);
        _blackPanel.gameObject.SetActive(false);
        _animator.enabled = false;
        this.enabled = false;
    }

    private void DisableAllButtouns()
    {
        for (int i = 0; i < _otherButtons.Count; i++)
            _otherButtons[i].GetComponent<Button>().interactable = false;

        _legButton.GetComponent<Button>().interactable = false;
        _bodyButton.GetComponent<Button>().interactable = false;
    }

    private void EnableAllButtouns()
    {
        for (int i = 0; i < _otherButtons.Count; i++)
            _otherButtons[i].GetComponent<Button>().interactable = true;

        _legButton.GetComponent<Button>().interactable = true;
        _bodyButton.GetComponent<Button>().interactable = true;
    }

    private void BlockOtherButton()
    {
        _blackPanel.raycastTarget = true;
    }

    private void UnlockOtherButton()
    {
        _blackPanel.raycastTarget = false;
    }
}