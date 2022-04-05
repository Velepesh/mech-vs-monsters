using UnityEngine;
using UnityEngine.UI;

public class HeadBuyTutorial : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RobotBuilder _robotBuilder;
    [SerializeField] private HeadButton _headButton;
    [SerializeField] private GameObject _menuTutorial;
    [SerializeField] private Image _blackPanel;

    private void OnEnable()
    {
        _robotBuilder.HeadSelected += OnHeadSelected;
    }

    private void OnDisable()
    {
        _robotBuilder.HeadSelected -= OnHeadSelected;
    }

    private void Start()
    {
        if (_robotBuilder.IsHeadSelected == false)
            StartTutorial();
    }

    private void StartTutorial()
    {
        _animator.enabled = true;
        _menuTutorial.SetActive(true);
        _blackPanel.gameObject.SetActive(true);
        _animator.SetTrigger(AnimatorMenuTutorialController.States.SelectLimb);
        Debug.Log("Totor");
        BlockOtherButton();
    }

    private void BlockOtherButton()
    {
        _blackPanel.raycastTarget = true;
    }

    private void OnHeadSelected()
    {
        _animator.SetTrigger(AnimatorMenuTutorialController.States.Battle);

        _headButton.GetComponent<Button>().interactable = false;
    }
}
