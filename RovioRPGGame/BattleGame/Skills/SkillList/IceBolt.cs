

using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.Skills
{
    public class IceBolt : ISkill
    {
        public int GetEnergyCost()
        {
            return 2;
        }

        public string GetFlavourText()
        {
            return "gets a shard of ice from the kitchen freezer and throws on";
        }

        public void UseAbility(BattleCharacter self, BattleCharacter target)
        {
            self.DealDamageTo(target, 10, DamageType.ICE);
        }
    }
}
