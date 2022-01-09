using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class PlayerStopButtonsController : MonoBehaviour {

        [SerializeField]
        private List<StopButton> _stopButtons;

        [SerializeField]
        private Player _player;

        private int _activeButtonsInRound = 0;

        private bool _isStopping = false;

        private void StartBraking(int difficultyLevel) {
            if (!_isStopping) {
                StartCoroutine(StopPlayerMiniGameCoroutine(difficultyLevel));
            }
        }

        private void ActivateButtons(int difficultyLevel) {
            switch (difficultyLevel) {
                case 0:
                    _stopButtons[0].gameObject.SetActive(true);
                    break;
                case 1:
                    ActivateRandomButtons(3);
                    break;
                case 3:
                    ActivateAllButtons();
                    break;
            }
        }

        private void ActivateAllButtons() {
            foreach(var button in _stopButtons) {
                button.gameObject.SetActive(true);
            }
        }

        private void ActivateRandomButtons(int buttonsNumberToActivate) {
            var listToActivation = new List<StopButton>(_stopButtons);
            for (int i = 0; i < buttonsNumberToActivate; i++) {
                var randomIndex = Random.Range(0, listToActivation.Count - 1);
                listToActivation[randomIndex].gameObject.SetActive(true);
                listToActivation.RemoveAt(randomIndex);
            }
        }

        private IEnumerator StopPlayerMiniGameCoroutine(int difficultyLevel) {
            ActivateButtons(difficultyLevel);
            _isStopping = true;
            var activeButtons = _activeButtonsInRound;
            while (activeButtons > 0) {
                if (OnButtonClick()) {
                    activeButtons--;
                }
                yield return null;
            }
            _isStopping = false;
        }

        private bool OnButtonClick() {
            if (Input.GetMouseButtonDown(0)) {
                var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(ray, Vector2.zero);
                if (hit.collider.gameObject.TryGetComponent<StopButton>(out var stopButton)) {
                    stopButton.gameObject.SetActive(false);
                    _player.Stop(1f / _activeButtonsInRound);
                    return true;
                }
            }
            return false;
        }
    }
}
