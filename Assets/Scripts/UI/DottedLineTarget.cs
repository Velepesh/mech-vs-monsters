using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLineTarget : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _hand;
    [SerializeField] private Canvas _copyOfMainCanvas;

    private Transform _tagret;

    private bool _isStart = false;

    public void SetTarget(Transform tagret)
    {
        _tagret = tagret;

        _isStart = true;
    }

    private void Update()
    {
        if (_isStart == false)
        {
            Vector2 myPositionOnScreen = _camera.WorldToScreenPoint(_tagret.position);

            float scaleFactor = _copyOfMainCanvas.scaleFactor;
            Vector2 finalPosition = new Vector2(myPositionOnScreen.x / scaleFactor, myPositionOnScreen.y / scaleFactor);
            _hand.anchoredPosition = finalPosition;
        }
    }
}