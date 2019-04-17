using RovioRPGGame.BattleGame.Buffs.Triggers;
using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.Buffs.BuffList
{
    public class RingOfFlames : IBuff, IBuffApplyTrigger, IBuffRemoveTrigger
    {
        public int GetDurationInTurns()
        {
            return -1;
        }

        public void OnBuffApplied(BattleCharacter affected)
        {
            var fireDmg = affected.Stats[Stat.FIRE_DAMAGE];
            fireDmg += 100;
            affected.Stats[Stat.FIRE_DAMAGE] = fireDmg;
        }

        public void OnBuffRemoved(BattleCharacter affected)
        {
            var fireDmg = affected.Stats[Stat.FIRE_DAMAGE];
            fireDmg -= 100;
            affected.Stats[Stat.FIRE_DAMAGE] = fireDmg;
        }
    }
}
