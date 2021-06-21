using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.AI;
using Zephyr.Combat;
using Zephyr.Stats;

namespace Zephyr.NPC
{
    [RequireComponent(typeof (CharacterStats))]
    public class NPC_Melee : NPCController, ICombatant
    {
        // Cache
        private CharacterStats npcStats;

        // Melee NPC States
        public readonly NPCState_MeleeIdle IdleState = new NPCState_MeleeIdle();

        protected override void Awake()
        {
            base.Awake();
            npcStats = GetComponent<CharacterStats>();
            // TODO (AI): Setup Targetting layers here
        }

        private void Start()
        {
            TransitionState(IdleState);
        }

        #region Interface Methods
        public void HitTarget(GameObject target)
        {
            // TODO (AI): Hit enemies logic
        }

        public void Stunned(bool isStunned)
        {
            // TODO (AI): Stunned logic
        }
        #endregion

    }
}