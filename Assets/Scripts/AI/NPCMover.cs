using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Zephyr.AI
{
    public class NPCMover
    {
        private NavMeshAgent _navMeshAgent;
        private float _maxPathLength;

        // Constructor
        public NPCMover(NavMeshAgent navMeshAgent, float maxPathLength)
        {
            _navMeshAgent = navMeshAgent;
            _maxPathLength = maxPathLength;
        }

        public void MoveTo(Vector3 destination, float speed, float speedModifier)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.speed = speed * Mathf.Clamp01(speedModifier);
            _navMeshAgent.isStopped = false;
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(_navMeshAgent.transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) { return false; } // No path found
            if (path.status != NavMeshPathStatus.PathComplete) { return false; } // Path is disconnected
            if (GetPathLength(path) > _maxPathLength) { return false; } // Path goes outside max distance
            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) { return total; }
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;
        }

        public void CancelMove()
        {
            if (_navMeshAgent.isStopped) { return; }
            _navMeshAgent.isStopped = true;
        }
    }
}