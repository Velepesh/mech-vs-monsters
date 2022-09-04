using UnityEngine;

public class TapGrenadeTutorialTarget : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _circle;
    [SerializeField] private Canvas _copyOfMainCanvas;

    private Transform _tagret;

    public void SetTarget(Transform tagret)
    {
        _tagret = tagret;
    }

    private void Update()
    {
        if (_tagret != null)
        {
            Vector2 myPositionOnScreen = _camera.WorldToScreenPoint(_tagret.position);

            float scaleFactor = _copyOfMainCanvas.scaleFactor;
            Vector2 finalPosition = new Vector2(myPositionOnScreen.x / scaleFactor, myPositionOnScreen.y / scaleFactor);
            _circle.anchoredPosition = finalPosition;
        }
    }
}