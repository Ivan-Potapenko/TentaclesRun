using Events;
using System.Collections;
using UnityEngine;

namespace Game {

    public class Cthulhu : MonoBehaviour {

        private bool _eyeIsOpen = false;
        public bool EyeIsOpen => _eyeIsOpen;

        [SerializeField]
        private Animator _eyeAnimator;

        [SerializeField]
        private EventDispatcher _cthulhuOpenedEyeEventDispatcher;

        [SerializeField]
        private EventDispatcher _cthulhuClosedEyeEventDispatcher;

        [SerializeField]
        private EventListener _playerDeadEventListener;

        private bool _stopCthulhu = false;
        public bool StopCthulhu => _stopCthulhu;

        private void OnEnable() {
            _playerDeadEventListener.OnEventHappened += PlayerDead;
            StartCoroutine(WaitForAnimationsCoroutine());
        }

        private void OnDisable() {
            _playerDeadEventListener.OnEventHappened -= PlayerDead;
            StopCoroutine(WaitForAnimationsCoroutine());
        }

        private IEnumerator WaitForAnimationsCoroutine() {
            while (!_stopCthulhu) {
                yield return new WaitForAnimationState(_eyeAnimator, new string[] { "EyeOpened" });
                _eyeIsOpen = true;
                _cthulhuOpenedEyeEventDispatcher.Dispatch();

                yield return new WaitForAnimationState(_eyeAnimator, new string[] { "EyeCloses" });
                _eyeIsOpen = false;
                _cthulhuClosedEyeEventDispatcher.Dispatch();
            }
        }

        private void PlayerDead() {
            _stopCthulhu = true;
        }

        public void OpenEye() {
            if (_stopCthulhu) {
                return;
            }
            _eyeAnimator.SetTrigger("Open");
        }

        public void CloseEye() {
            if (_stopCthulhu) {
                return;
            }
            _eyeAnimator.SetTrigger("Close");
        }
    }
}