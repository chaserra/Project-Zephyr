using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.AI;

namespace Zephyr.NPC
{
    public abstract class NPCController : MonoBehaviour
    {
        // Cache
        protected Animator anim;

        // State
        protected NPCStateBase _currentState;

        #region Properties
        /* **States** */
        public NPCStateBase CurrentState { get { return _currentState; } }
        #endregion

        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
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