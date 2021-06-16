using System.Collections.Generic;
using UnityEngine;
using Zephyr.Player.Controls;
using Zephyr.Player.Movement;
using Zephyr.Player.Combat;
using Zephyr.Combat;
using Zephyr.Stats;

namespace Zephyr.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(SpellCaster))]
    public class PlayerController : MonoBehaviour, ICombatant
    {
        #region Cache and Attributes
        // Cache
        private CharacterController characterController;
        private CharacterStats characterStats;
        private Animator anim;
        private Camera cam;
        private SpellCaster spellCaster;
        private PlayerMover mover = new PlayerMover();
        // Attributes
        [SerializeField] private InputController inputController;
        #endregion

        #region States
        // States
        private PlayerStateBase _currentState;
        public readonly PlayerStateMove MoveState = new PlayerStateMove();
        public readonly PlayerStateAttack AttackState = new PlayerStateAttack();
        public readonly PlayerStateCharging ChargingState = new PlayerStateCharging();
        public readonly PlayerStateCasting CastingState = new PlayerStateCasting();
        public readonly PlayerStateChannelling ChannellingState = new PlayerStateChannelling();
        public readonly PlayerStateStunned StunnedState = new PlayerStateStunned();
        private Dictionary<string, Skill> skillWithKeyMap = new Dictionary<string, Skill>();
        private Skill _currentSkill = null;
        private bool _isStunned = false;
        #endregion

        #region Properties
        /* **Cache** */
        public CharacterController Controller { get { return characterController; } }
        public InputController Input { get { return inputController; } }
        public Animator Anim { get { return anim; } }
        public Camera Cam { get { return cam; } }
        public PlayerMover Mover { get { return mover; } }
        public SpellCaster SpellCaster { get { return spellCaster; } }
        /* **Parameters** */
        public int GetMaxHealth { get { return characterStats.GetMaxHealth(); } }
        public int GetCurrentHealth { get { return characterStats.GetHealthPoints(); } }
        public float GetMoveSpeed { get { return characterStats.GetMoveSpeed(); } }
        public int GetDamage { get { return characterStats.GetDamage(); } }
        public float GetTurnSmoothTime { get { return characterStats.GetTurnSpeed(); } }
        /* **States** */
        public PlayerStateBase CurrentState { get { return _currentState; } }
        public Skill CurrentSkill { get { return _currentSkill; } }
        public Dictionary<string, Skill> SkillWithKeyMap { get { return skillWithKeyMap; } }
        public bool IsStunned { get { return IsStunned; } }
        #endregion

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterStats = GetComponent<CharacterStats>();
            anim = GetComponent<Animator>();
            spellCaster = GetComponent<SpellCaster>();
            cam = Camera.main;
        }

        private void Start()
        {
            TransitionState(MoveState);
        }

        private void Update()
        {
            if (_isStunned) { return; }

            // Detect button press and get mapped skill
            UseSkill(inputController.SkillButtonPress());

            // Do state Update method
            _currentState.Update(this);
        }

        private void UseSkill(Dictionary<string, Skill> skillWithKey)
        {
            if (skillWithKey != null && skillWithKeyMap.Count < 1)
            {
                // Assign keypress and skill to pass to next state
                foreach (KeyValuePair<string, Skill> keySkill in skillWithKey)
                {
                    string keyPressed = keySkill.Key;
                    _currentSkill = keySkill.Value;
                    skillWithKeyMap.Add(keyPressed, _currentSkill);
                }

                // Check skill type
                switch (_currentSkill.skillType)
                {
                    // If Charged skill
                    case SkillType.Charged:
                        TransitionState(ChargingState);
                        break;
                    // If Channelled skill
                    case SkillType.Channelled:
                        TransitionState(ChannellingState);
                        break;
                    // If Casted skill
                    case SkillType.Casting:
                        TransitionState(CastingState);
                        break;
                    // If Instant skill
                    default:
                        TransitionState(AttackState);
                        break;
                }
            }
        }

        public void TransitionState(PlayerStateBase state)
        {
            if (_currentState != state)
            {
                _currentState = state;
                _currentState.EnterState(this);
            }
        }

        public void ResetCurrentSkill()
        {
            skillWithKeyMap.Clear();
            _currentSkill = null;
        }

        #region Interface Methods
        // Hit other Combatants
        public void HitTarget(GameObject attackTarget)
        {
            // TODO (HIT): make hurtboxes only active on skill use
            //if (_currentSkill == null || _currentSkill is Skill_Self || _currentSkill is Skill_Projectile) { return; }
            if (!(_currentSkill is Skill_Melee)) { return; } // Ignore if skill is not melee
            if (_currentState == CastingState || _currentState == ChargingState || _currentState == ChannellingState) { return; }
            _currentSkill.ApplySkill(gameObject, attackTarget);
        }

        // Toggle Stunned State
        public void Stunned(bool isStunned)
        {
            // Set stun flag
            _isStunned = isStunned;

            // Exit current state (Exit State resets all of state's values internally)
            _currentState.ExitState(this);

            if (_isStunned) 
            {
                TransitionState(StunnedState);
            }
            else
            {
                TransitionState(MoveState);
            }
        }
        #endregion

    }
}