using System.Collections.Generic;
using UnityEngine;
using Zephyr.Player.Controls;
using Zephyr.Player.Movement;
using Zephyr.Player.Combat;
using Zephyr.Combat;
using Zephyr.Combat.Mods;
using Zephyr.Stats;

namespace Zephyr.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(InputController))]
    public class PlayerController : MonoBehaviour
    {
        // Cache
        private CharacterController characterController;
        private CharacterStats characterStats;
        private InputController inputController;
        private Animator anim;
        private Camera cam;
        private PlayerMover mover = new PlayerMover();

        // States
        private PlayerStateBase currentState;
        public readonly PlayerStateMove MoveState = new PlayerStateMove();
        public readonly PlayerStateAttack AttackState = new PlayerStateAttack();
        public readonly PlayerStateCharging ChargingState = new PlayerStateCharging();
        private Dictionary<string, Skill> skillWithKeyMap;
        private Skill currentSkill = null;

        // Properties
        #region Properties
        /* **Cache** */
        public CharacterController Controller { get { return characterController; } }
        public InputController Input { get { return inputController; } }
        public Animator Anim { get { return anim; } }
        public Camera Cam { get { return cam; } }
        public PlayerMover Mover { get { return mover; } }
        /* **Parameters** */
        public float MoveSpeed { get { return characterStats.GetMoveSpeed(); } }
        public float TurnSmoothTime { get { return characterStats.GetTurnSpeed(); } }
        /* **States** */
        public PlayerStateBase CurrentState { get { return currentState; } }
        public Skill CurrentSkill { get { return currentSkill; } }
        public Dictionary<string, Skill> SkillWithKeyMap { get { return skillWithKeyMap; } }
        #endregion

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterStats = GetComponent<CharacterStats>();
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
            UseSkill(inputController.SkillButtonPress());

            // Do state Update method
            currentState.Update(this);
        }

        private void UseSkill(Dictionary<string, Skill> skillWithKey)
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

        public void ApplyStatModifiers(Modifier mod)
        {
            mod.ApplyModifier(characterStats);
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