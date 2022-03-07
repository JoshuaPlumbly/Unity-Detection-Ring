using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleate : MonoBehaviour
{
    [SerializeField] float _delay;
    [SerializeField] GameObject _levelCompleatePanel;

    private Coroutine _displayLevelCompleateScreen;

    private void RunDisplayGameOver()
    {
        if (_levelCompleatePanel != null)
            StopCoroutine(_displayLevelCompleateScreen);

        _displayLevelCompleateScreen = StartCoroutine(DisplayLevelCompleate());
    }

    public IEnumerator DisplayLevelCompleate()
    {
        yield return new WaitForSeconds(_delay);
        _levelCompleatePanel.SetActive(true);
    }
}
