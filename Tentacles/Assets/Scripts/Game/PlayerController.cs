using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class PlayerController : MonoBehaviour {

        private enum MoveDirection {
            Left,
            Right,
            Up,
            Down,
        }

        [SerializeField]
        private Player _player;

        [SerializeField]
        private EventListener _updateEventListner;

        private Dictionary<MoveDirection, KeyCode> _control = new Dictionary<MoveDirection, KeyCode> {
            {MoveDirection.Up, KeyCode.W},
            {MoveDirection.Right, KeyCode.D},
            {MoveDirection.Left, KeyCode.A},
            {MoveDirection.Down, KeyCode.S},
        };

        private bool _move = false;

        private MoveDirection _moveDirection;

        private void OnEnable() {
            _updateEventListner.OnEventHappened += BehaviourUpdate;
        }

        private void OnDisable() {
            _updateEventListner.OnEventHappened -= BehaviourUpdate;
        }

        private void BehaviourUpdate() {
            GetInput();
            MovePlayer();
        }

        private void GetInput() {
            CheckKeyDown();
        }

        private void CheckKeyDown() {
            foreach (var button in _control) {
                if (!Input.GetKeyDown(button.Value)) {
                    continue;
                }
                SetMoveDirection(button.Key);
                return;
            }
        }   

        private void SetMoveDirection(MoveDirection moveDirection) {
            _move = true;
            _moveDirection = moveDirection;
        }

        private void MovePlayer() {
            if (!_move) {
                return;
            }
            switch (_moveDirection) {
                case MoveDirection.Left:
                    _player.Move(Vector2.left);
                    break;
                case MoveDirection.Right:
                    _player.Move(Vector2.right);
                    break;
                case MoveDirection.Up:
                    _player.Move(Vector2.up);
                    break;
                case MoveDirection.Down:
                    _player.Move(Vector2.down);
                    break;
            }
        }
    }
}

