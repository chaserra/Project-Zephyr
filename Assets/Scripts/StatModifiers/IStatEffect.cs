namespace Zephyr.Mods
{
    public interface IStatEffect
    {
        void ApplyEffect(ModifierManager modManager);
        void Tick(ModifierManager modManager);
        void RemoveEffect(ModifierManager modManager);
    }
}