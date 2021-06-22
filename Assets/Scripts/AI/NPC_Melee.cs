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

        // Attributes
        [SerializeField] private float awarenessRadius = 4f;

        // Melee NPC States
        public readonly NPCState_MeleeIdle IdleState = new NPCState_MeleeIdle();

        // Properties
        public float AwarenessRadius { get { return awarenessRadius; } }

        protected override void Awake()
        {
            base.Awake();
            npcStats = GetComponent<CharacterStats>();
            // TODO (AI): Setup Targetting layers here
        }

        private void Start()
        {
            NavAgent.speed = npcStats.GetMoveSpeed();
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