using System.Collections.Generic;
using UnityEngine;
using Zephyr.Player.Controls;
using Zephyr.Player.Movement;
using Zephyr.Player.Combat;
using Zephyr.Combat;

namespace Zephyr.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(InputController))]
    public class PlayerController : MonoBehaviour
    {
        // Cache
        private CharacterController characterController;
        private InputController inputController;
        private Animator anim;
        private Camera cam;
        private PlayerMover mover = new PlayerMover();

        // Parameters
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float turnSmoothTime = .1f;

        // States
        private PlayerStateBase currentState;
        public readonly PlayerStateMove MoveState = new PlayerStateMove();
        public readonly PlayerStateAttack AttackState = new PlayerStateAttack();
        public readonly PlayerStateCharging ChargingState = new PlayerStateCharging();
        private Dictionary<string, Skill> skillWithKeyMap;
        private Skill currentSkill = null;

        // Properties
        #region Properties
        public CharacterController Controller { get { return characterController; } }
        public InputController Input { get { return inputController; } }
        public Animator Anim { get { return anim; } }
        public Camera Cam { get { return cam; } }
        public PlayerMover Mover { get { return mover; } }

        public float MoveSpeed { get { return moveSpeed; } }
        public float TurnSmoothTime { get { return turnSmoothTime; } }

        public PlayerStateBase CurrentState { get { return currentState; } }
        public Skill CurrentSkill { get { return currentSkill; } }
        public Dictionary<string, Skill> SkillWithKeyMap { get { return skillWithKeyMap; } }
        #endregion

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            inputController = GetComponent<InputController>();
            anim = GetComponent<Animator>();
            cam = Camera.main;
            skillWithKeyMap = new Dictionary<string, Skill>();
        }

        private void Start()
        {
            TransitionState(MoveState);
        }

        private void Update()
        {
            // Detect button press and get mapped skill
            Attack(inputController.SkillButtonPress());

            // Do state Update method
            currentState.Update(this);
        }

        private void Attack(Dictionary<string, Skill> skillWithKey)
        {
            if (skillWithKey != null && skillWithKeyMap.Count < 1)
            {
                // Assign keypress and skill to pass to next state
                foreach (KeyValuePair<string, Skill> keySkill in skillWithKey)
                {
                    string keyPressed = keySkill.Key;
                    currentSkill = keySkill.Value;
                    skillWithKeyMap.Add(keyPressed, currentSkill);
                }

                // Check if skill is a charged attack
                if (currentSkill.skillType == SkillType.Charged)
                {
                    // Pass this attack type to ChargingState
                    TransitionState(ChargingState);
                }
                else
                {
                    // Pass this attack type to AttackState
                    TransitionState(AttackState);
                }
            }
        }

        public void TransitionState(PlayerStateBase state)
        {
            if (currentState != state)
            {
                currentState = state;
                currentState.EnterState(this);
            }
        }

        public void ResetCurrentSkill()
        {
            skillWithKeyMap.Clear();
            currentSkill = null;
        }

    }
}