using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class DifficultyProgressController : MonoBehaviour {

        [SerializeField]
        private Player _player;

        public int DifficultyLevel => 3 - (((((int)_player.MentalLevel)/30) +1) > 3?  3 : ((((int)_player.MentalLevel) / 30) + 1));
    }
}

