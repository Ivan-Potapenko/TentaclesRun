using UnityEngine;
using Events;

namespace Game {

    public class GameplayManager : MonoBehaviour {

        [SerializeField]
        private Player _player;

        [SerializeField]
        private EventListener _updateEventListner;

        [SerializeField]
        private GameObject _lose;

        private Animator _loseAnimator;

        private void Start() {
            _loseAnimator = _lose.GetComponent<Animator>();
        }

        private void OnEnable() {
            _updateEventListner.OnEventHappened += BehaviourUpdate;
        }

        private void OnDisable() {
            _updateEventListner.OnEventHappened -= BehaviourUpdate;
        }

        private void BehaviourUpdate() {
            if (_player.Animator.GetCurrentAnimatorStateInfo(0).IsName("pepel")) {
                _lose.SetActive(true);
            } else if (_player.Animator.GetCurrentAnimatorStateInfo(0).IsName("ufterFatal")) {
                _lose.SetActive(true);
            }
            if (_loseAnimator.GetCurrentAnimatorStateInfo(0).IsName("loseUfter")) {
                Time.timeScale = 0;
            }
        }
    }
}
