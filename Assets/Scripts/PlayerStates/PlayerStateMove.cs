namespace Zephyr.Player.Movement
{
    public class PlayerStateMove : PlayerStateBase
    {
        //Cache
        private PlayerMover mover;
        private bool playerCanRotate = true;

        public override void EnterState(PlayerController player)
        {
            // Initialize
            mover = player.Mover;
        }

        public override void Update(PlayerController player)
        {
            // TODO (Movement): playerCanRotate should be handled by the Mover script
            mover.Move(player, playerCanRotate);
        }

    }
}