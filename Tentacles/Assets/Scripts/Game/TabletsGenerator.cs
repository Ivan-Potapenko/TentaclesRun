using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class TabletsGenerator : MonoBehaviour {

        [SerializeField]
        private float _timeBetwinSpawnTablets = 5f;

        [SerializeField]
        private GameObject _tablets;

        [SerializeReference]
        private bool _showGizmos;

        [SerializeField]
        private List<SpawnZone> _spawnZones;

        [Serializable]
        private struct SpawnZone {
            public Vector2 center;
            public float radius;
        }


        private void OnDrawGizmos() {
            if (!_showGizmos) {
                return;
            }

            Gizmos.color = Color.yellow;
            foreach (var zone in _spawnZones) {
                Gizmos.DrawSphere(zone.center, zone.radius);
            }
        }

        private void OnEnable() {
            StartCoroutine(SpawnTabletsCoroutine());
        }

        private void OnDisable() {
            StopCoroutine(SpawnTabletsCoroutine());
        }

        private IEnumerator SpawnTabletsCoroutine() {
            while (true) {
                var randomWaitOffset = UnityEngine.Random.Range(-_timeBetwinSpawnTablets / 2, _timeBetwinSpawnTablets / 2);
                yield return new WaitForSeconds(_timeBetwinSpawnTablets + randomWaitOffset);
                var randomSpawnZone = _spawnZones[UnityEngine.Random.Range(0, _spawnZones.Count)];
                var theta = UnityEngine.Random.Range(0, (float)(2 * Math.PI * randomSpawnZone.radius));
                var r = UnityEngine.Random.Range(0, randomSpawnZone.radius);
                var x = (float)(r * Math.Cos(theta) + randomSpawnZone.center.x);
                var y = (float)(r * Math.Sin(theta) + randomSpawnZone.center.y);

                Instantiate(_tablets, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}

