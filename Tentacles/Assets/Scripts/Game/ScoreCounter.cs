using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game {

    public class ScoreCounter : MonoBehaviour {
        private int score = 0;

        [SerializeField]
        private Text _scoreText;

        [SerializeField]
        private Cthulhu _cthulhu;

        [SerializeField]
        private float _secondsToWait = 1;

        private void OnEnable() {
            StartCoroutine(CountScore());
        }

        private void OnDisable() {
            StopCoroutine(CountScore());
        }

        private IEnumerator CountScore() {
            while (!_cthulhu.PlayerDie) {
                score++;
                _scoreText.text = "Score:" + score.ToString();
                yield return new WaitForSeconds(_secondsToWait);
            }
        }
    }

}