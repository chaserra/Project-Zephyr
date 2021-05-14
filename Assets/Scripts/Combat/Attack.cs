using UnityEngine;

namespace Zephyr.Combat
{
    public class Attack
    {
        private readonly int _damage;
        private readonly bool _isCritical;
        private readonly Skill _skillUsed;
        private readonly Color _color = Color.red;

        // Just damage
        public Attack(int damage)
        {
            _damage = damage;
        }

        // Used for attacks with specific text colors
        public Attack(int damage, Color newColor)
        {
            _damage = damage;
            _color = newColor;
        }

        // Attack and don't proc mods
        public Attack(int damage, bool critical)
        {
            _damage = damage;
            _isCritical = critical;
        }

        // Attack and proc mods
        public Attack(int damage, bool critical, Skill skillUsed)
        {
            _damage = damage;
            _isCritical = critical;
            _skillUsed = skillUsed; // For passing mods
        }

        public int Damage { get { return _damage; } }
        public bool IsCritical { get { return _isCritical; } }
        public Skill SkillUsed { get { return _skillUsed; } }
        public Color TextColor { get { return _color; } }
    }
}