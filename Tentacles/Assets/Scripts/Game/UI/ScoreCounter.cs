using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Events;

namespace Game {

    public class ScoreCounter : MonoBehaviour {
        private int score = 0;

        [SerializeField]
        private Text _scoreText;

        [SerializeField]
        private EventListener _playerDeadEventListener;

        [SerializeField]
        private float _secondsToWait = 1;

        private void OnEnable() {
            StartCoroutine(CountScore());
            _playerDeadEventListener.OnEventHappened += OnPlayerDead;
        }

        private void OnDisable() {
            StopCoroutine(CountScore());
            _playerDeadEventListener.OnEventHappened -= OnPlayerDead;
        }

        private void OnPlayerDead() {
            StopCoroutine(CountScore());
        }

        private IEnumerator CountScore() {
            while (true) {
                score++;
                _scoreText.text = "Score:" + score.ToString();
                yield return new WaitForSeconds(_secondsToWait);
            }
        }
    }

}