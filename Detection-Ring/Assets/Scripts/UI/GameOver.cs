using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _gameOverPanel;

    private Coroutine _displayGameOver;

    private void Awake()
    {
        _health.Died += RunDisplayGameOver;
    }

    private void RunDisplayGameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (_displayGameOver != null)
            StopCoroutine(_displayGameOver);

        _displayGameOver = StartCoroutine(DisplayGameOver());
    }

    public IEnumerator DisplayGameOver()
    {
        yield return new WaitForSeconds(_delay);
        _gameOverPanel.SetActive(true);

        InGameHUDHandler inGameHUD = FindObjectOfType<InGameHUDHandler>();
        if (inGameHUD != null) 
            inGameHUD.SetEnabled(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
