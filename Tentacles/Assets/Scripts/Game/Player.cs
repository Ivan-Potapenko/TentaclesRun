using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {

    public class Player : MonoBehaviour {

        [SerializeField]
        private float _moveSpeed;

        private Rigidbody2D _rigidbody;

        private float _stoppingRatio = 1;

        [SerializeField]
        private Animator _animator;
        public Animator Animator => _animator;

        [SerializeField]
        private int _maxMentalLevel = 100;

        private int _mentalLevel;
        public int MentalLevel => _mentalLevel;

        private bool _canMove = true;

        private void Start() {
            _mentalLevel = _maxMentalLevel;
            _rigidbody = GetComponent<Rigidbody2D>();
            StartCoroutine(GoCrazyCoroutine());
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.TryGetComponent<Tablets>(out var tablets)) {
                RecoveryMental(tablets.RecoveryLevel);
                Destroy(tablets.gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.TryGetComponent<LandBorder>(out var landBorder)) {
                Fall();
            }
        }

        public void RecoveryMental(int recoveryLevel) {
            _mentalLevel += recoveryLevel;
            if (_mentalLevel > _maxMentalLevel) {
                _mentalLevel = _maxMentalLevel;
            }
        }

        private IEnumerator GoCrazyCoroutine() {
            while (true) {
                _mentalLevel--;
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void Fall() {
            _canMove = false;
            _animator.SetBool("fall", true);
            StopCoroutine(GoCrazyCoroutine());
        }

        public void Die() {
            _canMove = false;
            _animator.SetBool("DeathPlayer", true);
            StopCoroutine(GoCrazyCoroutine());
        }

        public void Stop(float stopPercentage) {
            _stoppingRatio -= 1 * stopPercentage;
        }

        public void MoveAfterStop() {
            _stoppingRatio = 1;
        }

        public bool IsWaiting() {
            return _stoppingRatio <= 0;
        }

        public void Move(Vector2 directionVector) {
            if (!_canMove) {
                _rigidbody.isKinematic = true;
                _rigidbody.velocity = Vector2.zero;
                return;
            }
            _rigidbody.velocity = _moveSpeed * directionVector * _stoppingRatio;
        }

    }
}

