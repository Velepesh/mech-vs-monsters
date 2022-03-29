using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private GameObject _playerAnime;
    [SerializeField] private PlayerMover _playerMove;
    [SerializeField] private GameObject _camera;
    [SerializeField] private bool _isPlay;
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private GameObject[] _icons;

    private Animator _animator;

    private void Start()
    {
        _animator = _playerAnime.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isPlay)
        {
            _isPlay = false;
            _camera.SetActive(false);
            StartCoroutine(StartMove());
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(StartUpg());
        }
    }

    private IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1.5f);

        _animator.SetTrigger("Move");
        _playerMove.enabled = true;
    }

    private IEnumerator StartUpg()
    {
        yield return new WaitForSeconds(0.6f);

        foreach (GameObject weap in _weapons)
        {
            weap.gameObject.SetActive(true);
        }
        foreach (GameObject icon in _icons)
        {
            icon.gameObject.SetActive(false);
        }
    }
}
