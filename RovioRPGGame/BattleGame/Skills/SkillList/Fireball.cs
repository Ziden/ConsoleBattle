using RovioRPGGame.BattleGame.Characters;
using System;

namespace RovioRPGGame.BattleGame.Skills
{
    public class Fireball : ISkill
    {
        public int GetEnergyCost()
        {
            return 1;
        }

        public string GetFlavourText()
        {
            return "gets a ligher and a deodorant and blasts fire on";
        }

        public void UseAbility(BattleCharacter self, BattleCharacter target)
        {
            self.DealDamageTo(target, 5, DamageType.FIRE);
        }
    }
}
