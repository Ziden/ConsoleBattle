using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.Buffs.Triggers
{
    public interface ITurnEndTrigger : IBuffTrigger
    {
        void OnTurnEnded(BattleCharacter affected);
    }
}
