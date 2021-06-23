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
        private NPCMover mover;

        // Parameters
        [SerializeField] private float maxPathLength = 20f;

        // Attributes
        [SerializeField] private float awarenessRadius = 8f;

        // Melee NPC States
        private float walkSpeed;
        private Vector3 initialPosition;
        public readonly NPCState_MeleeIdle IdleState = new NPCState_MeleeIdle();

        // Properties
        public NPCMover Mover { get { return mover; } }
        public float AwarenessRadius { get { return awarenessRadius; } }
        public float WalkSpeed { get { return walkSpeed; } }
        public Vector3 InitialPosition { get { return initialPosition; } }

        protected override void Awake()
        {
            base.Awake();
            npcStats = GetComponent<CharacterStats>();
            mover = new NPCMover(navMeshAgent, maxPathLength);
            // TODO (AI): Setup Targetting layers here
        }

        private void Start()
        {
            walkSpeed = npcStats.GetMoveSpeed();
            initialPosition = transform.position;
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