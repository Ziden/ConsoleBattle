using RovioRPGGame.BattleGame.Buffs.Triggers;
using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.Buffs.BuffList
{
    public class Poisoned : IBuff, ITurnEndTrigger
    {
        public int GetDurationInTurns()
        {
            return 1;
        }

        public void OnTurnEnded(BattleCharacter affected)
        {
            affected.RecieveDamage(4, DamageType.POISON);
        }
    }
}
