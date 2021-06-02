namespace Zephyr.Player.Movement
{
    public class PlayerStateMove : PlayerStateBase
    {
        //Cache
        private PlayerMover mover;
        private bool playerCanRotate = true;
        private bool playerCanMove = true;

        public override void EnterState(PlayerController player)
        {
            // Initialize
            mover = player.Mover;
        }

        public override void Update(PlayerController player)
        {
            mover.Move(player, playerCanRotate, playerCanMove, 1f);
        }

        public override void ExitState(PlayerController player)
        {
            // None since all exit states go back to MoveState
        }

    }
}