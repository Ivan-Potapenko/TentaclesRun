using UnityEngine;
using UnityEngine.UI;
using Events;
using Data;

namespace Game {

    public class ScrollbarController : MonoBehaviour {

        [SerializeField]
        private Image _scrollImage;

        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private PlayerData _playerData;

        private void OnEnable() {
            _eventListener.OnEventHappened += BehaviourUpdate;
        }

        private void OnDisable() {
            _eventListener.OnEventHappened -= BehaviourUpdate;
        }

        private void BehaviourUpdate() {
            UpdateScrollbar();
        }

        private void UpdateScrollbar() {
            if (_playerData.Player != null) {
                _scrollImage.fillAmount = ((float)_playerData.Player.MentalLevel) / _playerData.Player.MaxMentalLevel;
            }
        }
    }
}
