using RovioRPGGame.BattleGame.Buffs;
using RovioRPGGame.BattleGame.Characters;

namespace RovioRPGGame.BattleGame.UI
{
    public interface IUserInterface
    {
        void ShowMessage(string msg);

        void NewRound();

        void ShowStatDifferences(BattleCharacter before, BattleCharacter after);

        void ShowHealth(BattleCharacter character);

        void ShowBuffsChanges(BattleCharacter before, BattleCharacter after);

        void ShowBuffTrigger(IBuff buff, BattleCharacter before, BattleCharacter after);
    }
}
