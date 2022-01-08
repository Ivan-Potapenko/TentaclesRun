using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private int score = 0;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private float _secondsToWait = 1;

    private void OnEnable() {
        StartCoroutine(CountScore());
    }

    private void OnDisable() {
        StopCoroutine(CountScore());
    }

    private IEnumerator CountScore() {
        while(true) {
            score++;
            _scoreText.text ="Score:" + score.ToString();
            yield return new WaitForSeconds(_secondsToWait);
        }
    }
}
