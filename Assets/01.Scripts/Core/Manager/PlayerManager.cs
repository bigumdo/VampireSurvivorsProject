using BGD.Players;
using UnityEngine;

namespace BGD.Core.Manager
{
    [CreateAssetMenu(menuName ="SO/PlayerManager")]
    public class PlayerManager : ScriptableObject
    {
        private Player _player;
        public Player Player
        {
            get
            {
                if(_player == null )
                {
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                    Debug.Assert(_player != null, "Player°¡ ¾ø¾î");
                }
                return _player;
            }
            set { _player = value; }
        }
        public Transform PlayerTrm => _player.transform;
    }
}
