using System.Collections;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private float _upDistance;
    [SerializeField] private float _duration;

    private void OnValidate()
    {
        _upDistance = Mathf.Clamp(_upDistance, 0f, float.MaxValue);
        _duration = Mathf.Clamp(_duration, 0f, float.MaxValue);
    }

    private void OnEnable()
    {
        _game.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _game.LevelStarted -= OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        StartCoroutine(Move(_duration));
    }

    private IEnumerator Move(float duration)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + (transform.up * _upDistance);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}