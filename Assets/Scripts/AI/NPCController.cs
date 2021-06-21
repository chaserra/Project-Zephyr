using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.AI;

namespace Zephyr.NPC
{
    public class NPCController : MonoBehaviour
    {
        private Animator anim;

        private NPCStateBase _currentState;
        private readonly NPCStateIdle IdleState = new NPCStateIdle();

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            TransitionState(IdleState);
        }

        private void Update()
        {
            _currentState.Update(this);
        }

        public void TransitionState(NPCStateBase state)
        {
            if (_currentState != state)
            {
                _currentState = state;
                _currentState.EnterState(this);
            }
        }

    }
}