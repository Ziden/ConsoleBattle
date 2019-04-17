using RovioRPGGame.BattleGame.Buffs.Triggers;
using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.Buffs.BuffList
{
    public class LuckyPants : IBuff, IBuffApplyTrigger, IBuffRemoveTrigger
    {
        public int GetDurationInTurns()
        {
            return -1;
        }

        public void OnBuffApplied(BattleCharacter affected)
        {
            var dodge = affected.Stats[Stat.DODGE];
            dodge += 75;
            affected.Stats[Stat.DODGE] = dodge;
        }

        public void OnBuffRemoved(BattleCharacter affected)
        {
            var dodge = affected.Stats[Stat.DODGE];
            dodge -= 75;
            affected.Stats[Stat.DODGE] = dodge;
        }
    }
}
