namespace Zephyr.Player.Movement
{
    public class PlayerStateStunned : PlayerStateBase
    {
        public override void EnterState(PlayerController player)
        {
            // Reset all triggers
            ResetAnimTriggers(player);

            // Set trigger "Stunned" to escape from current animation
            player.Anim.SetTrigger("Stunned");
        }

        public override void Update(PlayerController player)
        {
            // Stunned. Character can't do anything while stunned.
        }

        public override void ExitState(PlayerController player)
        {
            // Reset all triggers
            ResetAnimTriggers(player);
        }

        private static void ResetAnimTriggers(PlayerController player)
        {
            foreach (var trigger in player.Anim.parameters)
            {
                player.Anim.ResetTrigger(trigger.name);
            }
        }

    }
}