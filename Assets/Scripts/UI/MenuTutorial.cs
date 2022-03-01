using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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

    private readonly int _firstLevel = 1;
    private readonly float _delayTime = 0.01f;
    private bool _isBodySelected;
    private bool _isTutorial;

    private void Awake()
    {
        DisableMenuTutorial();
    }
    private void Start()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return new WaitForSeconds(_delayTime);

        if (_game.CurrentLevel == _firstLevel && _isBodySelected == false)
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
        _isBodySelected = true;
        UnlockOtherButton();

        _bodyButton.GetComponent<Button>().interactable = false;

        for (int i = 0; i < _otherButtons.Count; i++)
            _otherButtons[i].GetComponent<Button>().interactable = false;
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
        for (int i = 0; i < _otherButtons.Count; i++)
            _otherButtons[i].GetComponent<Button>().interactable = true;

        _legButton.GetComponent<Button>().interactable = true;
        _menuTutorial.SetActive(false);
        _blackPanel.gameObject.SetActive(false);
        //this.enabled = false;
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