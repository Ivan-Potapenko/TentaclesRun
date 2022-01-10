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

        private bool _isBraking = false;

        [SerializeField]
        private DifficultyProgressController _difficultyProgressController;

        public void StartBraking() {
            if (!_isBraking) {
                StartCoroutine(StopPlayerMiniGameCoroutine(_difficultyProgressController.DifficultyLevel));
            }
        }

        private void ActivateButtons(int difficultyLevel) {
            switch (difficultyLevel) {
                case 0:
                    _stopButtons[0].gameObject.SetActive(true);
                    _activeButtonsInRound = 1;
                    break;
                case 1:
                    ActivateRandomButtons(3);
                    break;
                case 2:
                    ActivateAllButtons();
                    break;
            }
        }

        private void ActivateAllButtons() {
            _activeButtonsInRound = _stopButtons.Count;
            foreach (var button in _stopButtons) {
                button.gameObject.SetActive(true);
            }
        }

        private void ActivateRandomButtons(int buttonsNumberToActivate) {
            var listToActivation = new List<StopButton>(_stopButtons);
            _activeButtonsInRound = buttonsNumberToActivate;
            for (int i = 0; i < buttonsNumberToActivate; i++) {
                var randomIndex = Random.Range(0, listToActivation.Count - 1);
                listToActivation[randomIndex].gameObject.SetActive(true);
                listToActivation.RemoveAt(randomIndex);
            }
        }

        private IEnumerator StopPlayerMiniGameCoroutine(int difficultyLevel) {
            ActivateButtons(difficultyLevel);
            _isBraking = true;
            var activeButtons = _activeButtonsInRound;
            while (activeButtons > 0) {
                if (OnButtonClick()) {
                    activeButtons--;
                }
                yield return null;
            }
            _activeButtonsInRound = 0;
            _isBraking = false;
        }

        private bool OnButtonClick() {
            if (Input.GetMouseButtonDown(0)) {
                var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(ray, Vector2.zero);
                var gameObject = hit.collider?.gameObject;
                if (gameObject!= null && gameObject.TryGetComponent<StopButton>(out var stopButton)) {
                    stopButton.gameObject.SetActive(false);
                    _player.Stop(1f / _activeButtonsInRound);
                    return true;
                }
            }
            return false;
        }
    }
}
