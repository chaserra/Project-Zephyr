using System.Collections;
using UnityEngine;
using Zephyr.Player.Movement;
using Zephyr.Combat;

namespace Zephyr.Player.Combat
{
    public class PlayerStateAttack : PlayerStateBase
    {
        // Cache
        private PlayerMover mover;
        private Skill skill;

        public override void EnterState(PlayerController player)
        {
            mover = player.Mover;
            skill = player.CurrentSkill;
            player.StartCoroutine(TransitionState(player));
        }

        public override void Update(PlayerController player)
        {
            // Move if skill allows movement
            mover.Move(player, skill.userCanRotate, skill.userCanMove, skill.moveSpeedMultiplier);
        }

        IEnumerator TransitionState(PlayerController player)
        {
            // TODO (Attacks): Possibly change this to animation based instead of a coroutine
            skill.Initialize(player.Anim);
            // TODO (Combat): Create an attack, then transfer to target
            yield return new WaitForSeconds(.5f); // Better change this to wait for animation to end
            player.ResetCurrentSkill();
            player.TransitionState(player.MoveState);
        }

    }
}