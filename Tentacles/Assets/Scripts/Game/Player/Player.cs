using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Data;

namespace Game {

    public class Player : MonoBehaviour {

        public enum PlayerState {
            Moving,
            Stands,
            DeathProcess,
            Dead,
        }

        [SerializeField]
        private PlayerState _currentPlayerState;
        public PlayerState CurrentPlayerState => _currentPlayerState;

        [SerializeField]
        private float _moveSpeed;

        private Rigidbody2D _rigidbody;

        public float _stoppingRatio = 1;

        [SerializeField]
        private Animator _animator;
        public Animator Animator => _animator;

        [SerializeField]
        private BalanceSettings _balanceSettings;

        [SerializeField]
        private int _maxMentalLevel = 100;
        public int MaxMentalLevel => _maxMentalLevel;

        [SerializeField]
        private EventDispatcher _deathEventDispatcher;

        [SerializeField]
        private EventListener _cthulhuOpenedEyeEventListener;

        [SerializeField]
        private PlayerData _playerData;

        [SerializeField]
        private EventListener _cthulhuClosedEyeEventListener;

        public bool IsDead => _currentPlayerState == PlayerState.Dead;
        public bool CanMove => _currentPlayerState != PlayerState.DeathProcess && _currentPlayerState != PlayerState.Dead;

        private int _mentalLevel;
        public int MentalLevel => _mentalLevel;

        private void Awake() {
            _mentalLevel = _maxMentalLevel;
            _playerData.SetPlayer(this);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable() {
            StartCoroutine(GoCrazyCoroutine());
            StartCoroutine(WaitForDeadCoroutine());
            _cthulhuOpenedEyeEventListener.OnEventHappened += BurnDown;
            _cthulhuClosedEyeEventListener.OnEventHappened += MoveAfterStop;
        }

        private void OnDisable() {
            StopCoroutine(GoCrazyCoroutine());
            StopCoroutine(WaitForDeadCoroutine());
            _cthulhuOpenedEyeEventListener.OnEventHappened -= BurnDown;
            _cthulhuClosedEyeEventListener.OnEventHappened -= MoveAfterStop;
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

        private void RecoveryMental(int recoveryLevel) {
            _mentalLevel += recoveryLevel;
            if (_mentalLevel > _maxMentalLevel) {
                _mentalLevel = _maxMentalLevel;
            }
        }

        private IEnumerator WaitForDeadCoroutine() {
            yield return new WaitForAnimationState(_animator, new string[] { "Fell", "Died" });
            _currentPlayerState = PlayerState.Dead;
            _deathEventDispatcher.Dispatch();
        }

        private IEnumerator GoCrazyCoroutine() {
            while (CanMove) {
                _mentalLevel--;
                if (_mentalLevel <= 0) {
                    BurnDown();
                }
                yield return new WaitForSeconds(_balanceSettings.TimeInSeccondsBetweenMentalDeclines);
            }
        }

        public void Fall() {
            SetDeath(deathAnimationTrigger: "Fall");
        }

        public void BurnDown() {
            if (_mentalLevel <= 0 || _currentPlayerState == PlayerState.Moving) {
                SetDeath(deathAnimationTrigger: "BurnDown");
            }
        }

        private void SetDeath(string deathAnimationTrigger) {
            if (!CanMove) {
                return;
            }
            _currentPlayerState = PlayerState.DeathProcess;
            _animator.SetTrigger(deathAnimationTrigger);
            StopCoroutine(GoCrazyCoroutine());
        }

        public void Stop(float stopPercentage, bool fullStop = false) {
            if (fullStop) {
                _currentPlayerState = PlayerState.Stands;
                _stoppingRatio = 0;
            } else {
                _stoppingRatio -= 1 * stopPercentage;
            }
        }

        public void MoveAfterStop() {
            _currentPlayerState = PlayerState.Moving;
            _stoppingRatio = 1;
        }

        public void Move(Vector2 directionVector) {
            if (!CanMove) {
                _rigidbody.isKinematic = true;
                _rigidbody.velocity = Vector2.zero;
                return;
            }
            _rigidbody.velocity = _moveSpeed * directionVector * _stoppingRatio;
        }

    }
}

