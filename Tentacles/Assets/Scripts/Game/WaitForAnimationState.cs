using UnityEngine;

namespace Game {

    public class WaitForAnimationState : CustomYieldInstruction {

        private Animator _animator;

        private string[] _stateNames;

        public override bool keepWaiting {
            get {
                return CheckAnimationStates();
            }
        }

        private bool CheckAnimationStates() {
            foreach (var stateName in _stateNames) {
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)) {
                    return false;
                }
            }
            return true;
        }

        public WaitForAnimationState(Animator animator, string[] stateNames) {
            _animator = animator;
            _stateNames = stateNames;
        }
    }
}

