using Zephyr.NPC;
namespace Zephyr.AI
{
    public abstract class NPCStateBase
    {
        public abstract void EnterState(NPCController npc);
        public abstract void Update(NPCController npc);
        public abstract void ExitState(NPCController npc);
    }
}