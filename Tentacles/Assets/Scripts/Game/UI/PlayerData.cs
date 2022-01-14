using UnityEngine;
using Game;

namespace Data {

    [CreateAssetMenu(fileName = "new PlayerData", menuName = "PlayerData")]
    public class PlayerData : ScriptableObject {

        private Player _player;
        public Player Player {
            get {
                if (_player != null) {
                    return _player;
                }
                Debug.LogWarning("Player is null");
                return null;
            }
        }

        public void SetPlayer(Player player) {
            _player = player;
        }
    }
}
