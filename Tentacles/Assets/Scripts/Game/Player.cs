using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {

    public class Player : MonoBehaviour {

        [SerializeField]
        private float _moveSpeed;

        private Rigidbody2D _rigidbody;

        [SerializeField]
        private int _maxMentalLevel = 100;

        private int _mentalLevel;
        public int MentalLevel => _mentalLevel;

        private bool _canMove = true;

        private void Start() {
            _mentalLevel = _maxMentalLevel;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.TryGetComponent<Tablets>(out var tablets)) {
                RecoveryMental(tablets.RecoveryLevel);
                Destroy(tablets.gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.TryGetComponent<LandBorder>(out var landBorder)) {
                Drop();
            }
        }

        private void RecoveryMental(int recoveryLevel) {
            _mentalLevel += recoveryLevel;
            if (_mentalLevel > _maxMentalLevel) {
                _mentalLevel = _maxMentalLevel;
            }
        }

        public void Drop() {
            _canMove = false;
            Debug.Log("Drop");
        }

        public void Move(Vector2 directionVector) {
            if (!_canMove) {
                _rigidbody.isKinematic = true;
                _rigidbody.velocity = Vector2.zero;
                return;
            }
            _rigidbody.velocity = _moveSpeed * directionVector;
        }

    }
}

