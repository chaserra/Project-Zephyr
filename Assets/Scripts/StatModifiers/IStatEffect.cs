namespace Zephyr.Mods
{
    public interface IStatEffect
    {
        void ApplyEffect(Modifier mod);
        void Tick(Modifier mod);
        void RemoveEffect(Modifier mod);
    }
}