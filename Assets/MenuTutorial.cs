using UnityEngine;
using System.Collections;
public class MenuTutorial : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _menuTutorial;
    [SerializeField] private GameObject _blackPanel;
    [SerializeField] private RobotBuilder _robotBuilder;
    [SerializeField] private LegButton _legButton;
    [SerializeField] private BodyButton _bodyButton;
    [SerializeField] private LimbShopsHolder _limbShopsHolder;

    private readonly int _firstLevel = 1;
    private readonly float _delayTime = 0.1f;
    private bool _isBodySelected;

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
    }

    private void OnLegSelected()
    {
        _animator.SetTrigger(AnimatorMenuTutorialController.States.BodyButton);
    }

    private void OnLevelStarted()
    {
        DisableMenuTutorial();
    }
    private void EnableMenuTutorial()
    {
        _menuTutorial.SetActive(true);
        _blackPanel.SetActive(true);
    }

    private void OnOpened(LimbShop shop)
    {
        _animator.SetTrigger(AnimatorMenuTutorialController.States.SelectLimb);

        _limbShopsHolder.TurnOffEmptySpaceButton();
    }

    private void DisableMenuTutorial()
    {
        _menuTutorial.SetActive(false);
        _blackPanel.SetActive(false);
    }
}