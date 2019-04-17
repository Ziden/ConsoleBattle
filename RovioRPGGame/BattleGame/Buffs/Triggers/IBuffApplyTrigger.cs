using RovioRPGGame.BattleGame.Buffs.Triggers;
using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.Buffs.Triggers
{
    public interface IBuffApplyTrigger : IBuffTrigger
    {
        void OnBuffApplied(BattleCharacter affected);
    }
}
