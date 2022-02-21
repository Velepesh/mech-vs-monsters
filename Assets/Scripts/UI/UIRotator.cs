using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRotator : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Game _game;
    [SerializeField] private float _time;

    private void OnEnable()
    {
        _game.Fought += OnFought;
        _game.BattleWon += OnBattleWon;
    }

    private void OnDisable()
    {
        _game.Fought -= OnFought;
        _game.BattleWon -= OnBattleWon;
    }

    private void OnBattleWon()
    {
        StartCoroutine(ChangeRotation(Vector3.zero, _time));
    }

    private void OnFought()
    {
       
        StartCoroutine(ChangeRotation(new Vector3(0f, 90f, 0f), _time));
    }

    private IEnumerator ChangeRotation(Vector3 rotation, float duration)
    {
        DisableCanvas();
        yield return new WaitForSeconds(duration);

        _canvas.transform.rotation = Quaternion.Euler(rotation);
        EnableCanvas();
    }

    private void EnableCanvas()
    {
        _canvas.gameObject.SetActive(true);
    }

    private void DisableCanvas()
    {
        _canvas.gameObject.SetActive(false);
    }
}