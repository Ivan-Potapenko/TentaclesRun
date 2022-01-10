using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CthulhuController : MonoBehaviour {

        [SerializeField]
        private Cthulhu _cthulhu;

        [SerializeField]
        private float _secondsBetwinOpenEye = 10f;

        [SerializeField]
        private float _secondsBetwinCloseEye = 3f;

        [SerializeField]
        private PlayerStopButtonsController _playerStopButtonsController;

        private void OnEnable() {
            StartCoroutine(OpenEyeCoroutine());
        }

        private void OnDisable() {
            StopCoroutine(OpenEyeCoroutine());
        }

        private IEnumerator OpenEyeCoroutine() {
            while (!_cthulhu.PlayerDie) {
                var randomWaitOffset = Random.Range(-_secondsBetwinOpenEye / 2, _secondsBetwinOpenEye / 5);
                yield return new WaitForSeconds(_secondsBetwinOpenEye + randomWaitOffset);
                _cthulhu.OpenEye();
                _playerStopButtonsController.StartBraking();
                yield return new WaitForSeconds(_secondsBetwinCloseEye);
                _cthulhu.CloseEye();
            }
        }
    }
}