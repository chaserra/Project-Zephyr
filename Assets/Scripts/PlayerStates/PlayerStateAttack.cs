using System.Collections;
using UnityEngine;

namespace Zephyr.Player.Combat
{
    public class PlayerStateAttack : PlayerStateBase
    {
        public override void EnterState(PlayerController player)
        {
            player.StartCoroutine(TransitionState(player));
        }

        public override void Update(PlayerController player)
        {
            // Add mover script here? For skills that can be spammed while moving
        }

        IEnumerator TransitionState(PlayerController player)
        {
            // TODO (Attacks): Possibly change this to animation based instead of a coroutine
            player.CurrentSkill.Initialize(player.Anim);
            yield return new WaitForSeconds(.5f); // Better change this to wait for animation to end
            player.ResetCurrentSkill();
            player.TransitionState(player.MoveState);
        }

    }
}