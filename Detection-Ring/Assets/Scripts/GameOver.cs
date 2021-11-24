using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _gameOverPanel;

    private Coroutine _displayGameOver;

    private void Awake()
    {
        _health.OnDeath += RunDisplayGameOver;
    }

    private void RunDisplayGameOver()
    {
        if (_displayGameOver != null)
            StopCoroutine(_displayGameOver);

        _displayGameOver = StartCoroutine(DisplayGameOver());
    }

    public IEnumerator DisplayGameOver()
    {
        yield return new WaitForSeconds(_delay);
        _gameOverPanel.SetActive(true);
    }
}
