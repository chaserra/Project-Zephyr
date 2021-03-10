namespace Zephyr.Mods
{
    public interface IStatEffect
    {
        void ApplyEffect();
        void Tick();
        void RemoveEffect();
    }
}