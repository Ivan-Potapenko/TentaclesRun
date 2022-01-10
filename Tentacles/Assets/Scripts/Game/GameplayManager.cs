using UnityEngine;
using Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game {

    public class GameplayManager : MonoBehaviour {

        [SerializeField]
        private Player _player;

        [SerializeField]
        private EventListener _updateEventListner;

        [SerializeField]
        private GameObject _lose;

        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private Button _menuButton;

        private void Start() {
            _menuButton.onClick.AddListener(LoadMenu);
            _restartButton.onClick.AddListener(LoadGameplayScene);
        }

        private void LoadMenu() {
            SceneManager.LoadScene("MenuScene");
        }

        private void LoadGameplayScene() {
            SceneManager.LoadScene("GameplayScene");
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
        }
    }
}
