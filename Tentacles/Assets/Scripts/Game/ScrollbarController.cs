using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;

namespace Game {

    public class ScrollbarController : MonoBehaviour {

        [SerializeField]
        private Image _scrollImage;

        [SerializeField]
        private EventListener _eventListener;

        [SerializeField]
        private Player _player;

        private void OnEnable() {
            _eventListener.OnEventHappened += BehaviourUpdate;
        }

        private void OnDisable() {
            _eventListener.OnEventHappened -= BehaviourUpdate;
        }

        private void BehaviourUpdate() {
            _scrollImage.fillAmount = _player.MentalLevel / 100f;
        }
    }
}
