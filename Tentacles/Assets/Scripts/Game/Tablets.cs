using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class Tablets : MonoBehaviour {

        [SerializeField]
        private int _recoveryLevel;
        public int RecoveryLevel => _recoveryLevel;
    }
}

