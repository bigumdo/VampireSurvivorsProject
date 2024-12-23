using BGD.Agents;
using BGD.Core.Manager;

namespace BGD.Players
{
    public class PlayerMover : AgentMover
    {
        private Player _player;

        public override void Initialize(Agent agent)
        {
            _player = agent as Player;
            base.Initialize(agent);
        }
        protected override void MoveCharacter()
        {
            int playerDir = _player.PlayerInputSO.InputDirection.x > 0 ? 1 : -1;
            if (CanManualMove)
            {
                if (playerDir == MouseManager.Instance.MouseDir)
                    _rbCompo.velocity = _movement * _moveSpeed * SpeedMultiplier;
                else
                    _rbCompo.velocity = _movement * _moveSpeed * SpeedMultiplier * 0.7f;
            }
            _renderer.FlipController(MouseManager.Instance.MouseDir);
        }
    }
}
