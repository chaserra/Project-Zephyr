using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zephyr.NPC;

namespace Zephyr.AI
{
    public class NPCState_MeleeIdle : NPCStateBase
    {
        // Parameters
        private float minIdleTime = 5f; // TODO (AI): Make this a scriptableObject float
        private float maxIdleTime = 10f; // TODO (AI): Make this a scriptableObject float

        // State
        private bool idling;

        public override void EnterState(NPCController npc)
        {
            // Cache and Initialize
            idling = true;
            npc.StartCoroutine(MoveToRandomPosition(npc));
        }

        public override void Update(NPCController npc)
        {
            // Do update stuff

            // Check if npc has arrived at destination
            if (Vector3.Distance(npc.transform.position, npc.NavAgent.destination) <= npc.NavAgent.stoppingDistance)
            {
                npc.NavAgent.isStopped = true;
            }
            // Look for aggro here
        }

        public override void ExitState(NPCController npc)
        {
            // Do exit state stuff
            idling = false;
        }

        private IEnumerator MoveToRandomPosition(NPCController npc)
        {
            NPC_Melee meleeNPC = (NPC_Melee)npc;
            npc.NavAgent.isStopped = true;

            while (idling)
            {
                // Get random position if stopped
                if (npc.NavAgent.isStopped)
                {
                    npc.NavAgent.SetDestination(RandomNavmeshLocation(npc, meleeNPC.AwarenessRadius));
                    npc.NavAgent.isStopped = false;
                    yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
                }
                yield return null;
            }
            yield return null;
        }

        private Vector3 RandomNavmeshLocation(NPCController npc, float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += npc.transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }

    }
}