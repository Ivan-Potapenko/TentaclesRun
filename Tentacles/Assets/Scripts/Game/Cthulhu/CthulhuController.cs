using System.Collections;
using UnityEngine;
using Events;

namespace Game {

    public class CthulhuController : MonoBehaviour {

        [SerializeField]
        private Cthulhu _cthulhu;

        [SerializeField]
        private float _secondsBetwinOpenEye = 10f;

        [SerializeField]
        private float _secondsBetwinCloseEye = 5f;

        [SerializeField]
        private EventDispatcher _startBrakingEventDispatcher;

        private void OnEnable() {
            StartCoroutine(OpenEyeCoroutine());
        }

        private void OnDisable() {
            StopCoroutine(OpenEyeCoroutine());
        }

        private IEnumerator OpenEyeCoroutine() {
            while (!_cthulhu.StopCthulhu) {
                var randomWaitOffset = Random.Range(-_secondsBetwinOpenEye / 2, _secondsBetwinOpenEye / 5);
                yield return new WaitForSeconds(_secondsBetwinOpenEye + randomWaitOffset);
                _cthulhu.OpenEye();
                _startBrakingEventDispatcher.Dispatch();
                yield return new WaitForSeconds(_secondsBetwinCloseEye);
                _cthulhu.CloseEye();
            }
        }
    }
}