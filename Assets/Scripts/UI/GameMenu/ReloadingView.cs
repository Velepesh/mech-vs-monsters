using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReloadingView : MonoBehaviour
{
    [SerializeField] private RocketLauncher _rocketLauncher;
    [SerializeField] private Image _reloadingImage;

    private void OnEnable()
    {
        _rocketLauncher.Reloaded += OnReloaded;
    }

    private void OnDisable()
    {
        _rocketLauncher.Reloaded -= OnReloaded;
    }

    private void OnReloaded(float duration)
    {
        StartCoroutine(Reload(duration));
    }

    private IEnumerator Reload(float duration)
    {
        float progress = 0;

        _reloadingImage.fillAmount = progress;

        while (progress < duration)
        {
            progress += Time.deltaTime;
            float percent = progress / duration;

            _reloadingImage.fillAmount = percent;

            yield return null;
        }
    }
}