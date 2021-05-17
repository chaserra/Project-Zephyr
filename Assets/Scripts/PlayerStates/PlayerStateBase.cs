using Zephyr.Player;
public abstract class PlayerStateBase
{
    public abstract void EnterState(PlayerController player);
    public abstract void Update(PlayerController player);
    public abstract void ExitState(PlayerController player);
}