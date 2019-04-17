using RovioRPGGame.BattleGame.Buffs.BuffList;
using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.Skills
{
    public class Poison : ISkill
    {
        public int GetEnergyCost()
        {
            return 3;
        }

        public string GetFlavourText()
        {
            return "throws a dirty socks at";
        }

        public void UseAbility(BattleCharacter self, BattleCharacter target)
        {
            self.DealDamageTo(target, 7, DamageType.POISON);
            target.AddBuff(new Poisoned());
        }

    }
}
