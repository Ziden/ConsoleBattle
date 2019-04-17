using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.Buffs.Triggers
{
    public interface IBuffRemoveTrigger : IBuffTrigger
    {
        void OnBuffRemoved(BattleCharacter affected);
    }
}
