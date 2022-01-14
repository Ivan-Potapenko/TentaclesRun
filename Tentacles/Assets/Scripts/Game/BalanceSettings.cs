using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "new BalanceSettings", menuName = "BalanceSettings")]
    public class BalanceSettings : ScriptableObject {
        [SerializeField]
        private float _timeInSeccondsBetweenMentalDeclines = 0.5f;
        public float TimeInSeccondsBetweenMentalDeclines => _timeInSeccondsBetweenMentalDeclines;

        [SerializeField]
        private float _timeInSeccondsBetweenTabletsSpawns = 5;
        public float TimeInSeccondsBetweenTabletsSpawns => _timeInSeccondsBetweenTabletsSpawns;

        [SerializeField]
        private float _timeInSeccondsBetweenEyeOpening = 10f;
        public float TimeInSeccondsBetweenEyeOpening => _timeInSeccondsBetweenEyeOpening;
    }
}

