namespace Zephyr.Combat
{
    public class Attack
    {
        private readonly int _damage;
        private readonly bool _isCritical;
        private readonly Skill _skillUsed;

        public Attack(int damage, bool critical, Skill skillUsed)
        {
            _damage = damage;
            _isCritical = critical;
            _skillUsed = skillUsed;
        }

        public int Damage { get { return _damage; } }
        public bool IsCritical { get { return _isCritical; } }
        public Skill SkillUsed { get { return _skillUsed; } }
    }
}