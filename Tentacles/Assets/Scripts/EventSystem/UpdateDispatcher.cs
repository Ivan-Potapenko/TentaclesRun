using UnityEngine;

namespace Events {

    public class UpdateDispatcher : MonoBehaviour {

        [SerializeField]
        private EventDispatcher _updateEventDispatcher;

        private void Update() {
            _updateEventDispatcher.Dispatch();
        }
    }
}
