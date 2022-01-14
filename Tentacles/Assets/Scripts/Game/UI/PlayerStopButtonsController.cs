using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Data;

namespace Game {

    public class PlayerStopButtonsController : MonoBehaviour {

        [SerializeField]
        private List<StopButton> _stopButtons;

        [SerializeField]
        private PlayerData _playerData;

        [SerializeField]
        private EventListener _playerDeadEventListener;

        [SerializeField]
        private EventListener _startBrakingEventListener;

        private int _activeButtonsInRound = 0;

        private bool _isBraking = false;

        public int DifficultyLevel => 3 - (((((int)_playerData.Player.MentalLevel) / 30) + 1) > 3 ? 3 :
            ((((int)_playerData.Player.MentalLevel) / 30) + 1));

        private void OnEnable() {
            _playerDeadEventListener.OnEventHappened += OnPlayerDead;
            _startBrakingEventListener.OnEventHappened += StartBraking;
        }

        private void OnDisable() {
            _playerDeadEventListener.OnEventHappened -= OnPlayerDead;
            _startBrakingEventListener.OnEventHappened -= StartBraking;
        }

        private void OnPlayerDead() {
            gameObject.SetActive(false);
        }

        public void StartBraking() {
            if (!_isBraking) {
                StartCoroutine(StopPlayerMiniGameCoroutine(DifficultyLevel));
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

        public void DisativateButtons() {
            _activeButtonsInRound = _stopButtons.Count;
            foreach (var button in _stopButtons) {
                button.gameObject.SetActive(false);
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
                if (CheckOnButton(activeButtons)) {
                    activeButtons--;
                }
                yield return null;
            }
            _activeButtonsInRound = 0;
            _isBraking = false;
        }

        private bool CheckOnButton(int activeButtons) {
            if (PlatformController.IsInEditor) {
                return OnButtonClick(activeButtons);
            }
            if (PlatformController.IsAndroidPlatform) {
                return OnTouchClick(activeButtons);
            }
            return false;
        }

        private bool OnButtonClick(int activeButtons) {
            if (Input.GetMouseButtonDown(0)) {
                var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(ray, Vector2.zero);
                var gameObject = hit.collider?.gameObject;
                if (gameObject != null && gameObject.TryGetComponent<StopButton>(out var stopButton)) {
                    stopButton.gameObject.SetActive(false);
                    _playerData.Player.Stop(1f / _activeButtonsInRound, activeButtons == 1);
                    return true;
                }
            }
            return false;
        }

        private bool OnTouchClick(int activeButtons) {
            if (Input.touchCount > 0) {
                var touches = Input.touches;
                for (int i = 0; i < touches.Length; i++) {
                    if (touches[i].phase != TouchPhase.Began) {
                        continue;
                    }
                    var ray = Camera.main.ScreenToWorldPoint(touches[i].position);
                    var hit = Physics2D.Raycast(ray, Vector2.zero);
                    var gameObject = hit.collider?.gameObject;
                    if (gameObject != null && gameObject.TryGetComponent<StopButton>(out var stopButton)) {
                        stopButton.gameObject.SetActive(false);

                        _playerData.Player.Stop(1f / _activeButtonsInRound, activeButtons == 1);
                        return true;
                    }
                }

            }
            return false;
        }
    }
}
