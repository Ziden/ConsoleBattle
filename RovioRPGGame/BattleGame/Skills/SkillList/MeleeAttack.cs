

using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.Skills
{
    public class MeleeAttack : ISkill
    {
        public int GetEnergyCost()
        {
            return 0;
        }

        public string GetFlavourText()
        {
            return "slaps";
        }

        public void UseAbility(BattleCharacter self, BattleCharacter target)
        {
            self.DealDamageTo(target, 3, DamageType.PHYSICAL);
        }
    }
}
