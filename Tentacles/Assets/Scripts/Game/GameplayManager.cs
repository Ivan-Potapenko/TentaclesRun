using UnityEngine;
using Events;

namespace Game {

    public class GameplayManager : MonoBehaviour {

        [SerializeField]
        private Player _player;

        [SerializeField]
        private EventListener _updateEventListner;

        private void OnEnable() {
            _updateEventListner.OnEventHappened += BehaviourUpdate;
        }

        private void OnDisable() {
            _updateEventListner.OnEventHappened -= BehaviourUpdate;
        }

        private void BehaviourUpdate() {
            if (_player.Animator.GetCurrentAnimatorStateInfo(0).IsName("pepel")) {

            } else if (_player.Animator.GetCurrentAnimatorStateInfo(0).IsName("ufterFatal")) {

            }
        }
    }
}
