using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zephyr.NPC;
using Zephyr.Util;

namespace Zephyr.AI
{
    public class NPCState_MeleeIdle : NPCStateBase
    {
        // Cache
        NPC_Melee meleeController;

        // Parameters
        private float minIdleTime = 5f; // TODO (AI): Make this a scriptableObject float
        private float maxIdleTime = 10f; // TODO (AI): Make this a scriptableObject float
        private float idleMoveChance = .35f; // TODO (AI): Make this a scriptableObject float
        private float idleWalkSpeedModifier = .3f; // TODO (AI): Make this a scriptableObject float

        // State
        private bool idling;
        public Vector3 here; // TODO: REMOVE DEBUG

        public override void EnterState(NPCController npc)
        {
            // Cache and Initialize
            if (meleeController == null) { meleeController = (NPC_Melee)npc; }
            idling = true;
            npc.StartCoroutine(DoIdleActions(npc));
        }

        public override void Update(NPCController npc)
        {
            // Do update stuff

            // Check if npc has arrived at destination
            if (Vector3.Distance(npc.transform.position, npc.NavAgent.destination) <= npc.NavAgent.stoppingDistance)
            {
                meleeController.Mover.CancelMove();
            }
            // Look for aggro here
        }

        public override void ExitState(NPCController npc)
        {
            // Do exit state stuff
            idling = false;
        }

        private IEnumerator DoIdleActions(NPCController npc)
        {
            npc.NavAgent.isStopped = true;

            while (idling)
            {
                // Get random position if stopped
                if (npc.NavAgent.isStopped)
                {
                    yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
                    Vector3 randomPos = GetRandomPosition(meleeController, meleeController.AwarenessRadius / 2);

                    // Roll for actions
                    if (WillDoAction(idleMoveChance))
                    {
                        // Move
                        MoveToRandomPosition(meleeController, randomPos, meleeController.WalkSpeed);
                    }
                    else if (WillDoAction(idleMoveChance * 1.5f))
                    {
                        // Stationary rotate
                        npc.StartCoroutine(RotateToRandomDirection(meleeController, randomPos, meleeController.WalkSpeed));
                    }
                }
                yield return null;
            }
            yield return null;
        }

        // Move within starting point range
        private void MoveToRandomPosition(NPC_Melee npc, Vector3 randomPos, float walkSpeed)
        {
            // Move npc to position if within maxPathLength
            if (npc.Mover.CanMoveTo(randomPos))
            {
                npc.Mover.MoveTo(randomPos, walkSpeed, idleWalkSpeedModifier);
            }
        }

        // Stationary rotate
        private IEnumerator RotateToRandomDirection(NPC_Melee npc, Vector3 randomPos, float rotateSpeed)
        {
            float modifiedRotateSpeed = rotateSpeed * idleWalkSpeedModifier;
            Vector3 randomDir = (randomPos - npc.transform.position).normalized;
            randomDir.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(randomDir);
            while (Quaternion.Angle(npc.transform.rotation, targetRotation) > .1f && npc.NavAgent.isStopped)
            {
                // Smooth rotate
                npc.transform.rotation = Quaternion.Slerp(
                    npc.transform.rotation, targetRotation, modifiedRotateSpeed * Time.deltaTime);
                // If within 1 degree, snap to target rotation. Prevents jitter at the end
                if (Quaternion.Angle(npc.transform.rotation, targetRotation) <= 1f)
                {
                    npc.transform.rotation = targetRotation;
                    yield break;
                }
                yield return null;
            }
            yield return null;
        }

        private Vector3 GetRandomPosition(NPC_Melee npc, float radius)
        {
            // Find random position on NavMesh
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += npc.InitialPosition;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
                here = finalPosition; // TODO: REMOVE DEBUG
            }
            return finalPosition;
        }

        private bool WillDoAction(float roll)
        {
            return UtilityHelper.RollForProc(roll);
        }

    }
}