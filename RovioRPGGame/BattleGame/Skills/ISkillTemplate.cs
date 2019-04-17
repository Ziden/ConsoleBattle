using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.Skills
{
    public interface ISkill
    {
        int GetEnergyCost();

        void UseAbility(BattleCharacter self, BattleCharacter target);

        string GetFlavourText();
    }
}
