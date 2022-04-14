using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField] private Image _aimImage;

    private void Start()
    {
        DisableAim();
    }

    public void MoveAim(Vector3 mousePosition)
    {
        _aimImage.transform.position = mousePosition;
    }

    public void ShowAim()
    {
        _aimImage.gameObject.SetActive(true);
    }

    private void DisableAim()
    {
        _aimImage.gameObject.SetActive(false);
    }
}