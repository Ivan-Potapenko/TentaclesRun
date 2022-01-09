using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CthulhuControler : MonoBehaviour {

        [SerializeField]
        private Cthulhu _cthulhu;

        [SerializeField]
        private float _secondsBetwinOpenEye = 10f;

        [SerializeField]
        private float _secondsBetwinCloseEye = 3f;

        private void OnEnable() {
            StartCoroutine(OpenEyeCorutine());
        }

        private void OnDisable() {
            StopCoroutine(OpenEyeCorutine());
        }

        private IEnumerator OpenEyeCorutine() {
            while (true) {
                var randomWaitOffset = Random.Range(-_secondsBetwinOpenEye / 2, _secondsBetwinOpenEye / 5);
                yield return new WaitForSeconds(_secondsBetwinOpenEye + randomWaitOffset);
                _cthulhu.OpenEye();

                yield return new WaitForSeconds(_secondsBetwinCloseEye);
                _cthulhu.CloseEye();
            }
        }
    }
}