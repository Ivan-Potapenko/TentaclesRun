using Events;
using UnityEngine;

namespace Game {

    public class Cthulhu : MonoBehaviour {

        private bool _eyeIsOpen = false;
        public bool EyeIsOpen => _eyeIsOpen;

        [SerializeField]
        private Animator _eyeAnimator;

        [SerializeField]
        private Player _player;

        public bool PlayerDie = false;

        [SerializeField]
        private EventListener _updateEventListner;

        private void OnEnable() {
            _updateEventListner.OnEventHappened += BehaviourUpdate;
        }

        private void OnDisable() {
            _updateEventListner.OnEventHappened -= BehaviourUpdate;
        }

        private void BehaviourUpdate() {
            if (_eyeAnimator.GetCurrentAnimatorStateInfo(0).IsName("EyeOpened")) {
                _eyeIsOpen = true;
            } else if (_eyeAnimator.GetCurrentAnimatorStateInfo(0).IsName("EyeCloses")) {
                _eyeIsOpen = false;
                _player.MoveAfterStop();
            }
        }

        public void OpenEye() {
            if(PlayerDie) {
                return;
            }
            _eyeAnimator.SetBool("Open", true);
            _eyeAnimator.SetBool("Close", false);
        }

        public void CloseEye() {
            if (PlayerDie) {
                return;
            }
            _eyeAnimator.SetBool("Open", false);
            _eyeAnimator.SetBool("Close", true);
        }
    }
}