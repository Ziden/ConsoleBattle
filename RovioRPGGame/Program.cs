using RovioRPGGame.BattleGame;
using RovioRPGGame.BattleGame.Buffs;
using RovioRPGGame.BattleGame.Buffs.BuffList;
using RovioRPGGame.BattleGame.Characters;
using RovioRPGGame.BattleGame.UI;
using System;

namespace RovioRPGGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var attacker = CharacterFactory.GetCharacter<AngryMagebird>();
            var defender = CharacterFactory.GetCharacter<AngryMagebird>();

            attacker.Name = "Angry MageBird";
            defender.Name = "Angry OracleBird";

            if (Formulas.Chance(25))
                attacker.AddBuff(new RingOfFlames());
            if (Formulas.Chance(25))
                attacker.AddBuff(new LuckyPants());
            if (Formulas.Chance(25))
                defender.AddBuff(new RingOfFlames());
            if (Formulas.Chance(25))
                defender.AddBuff(new LuckyPants());

            var ui = new ConsoleInterface();
            ui.ShowMessage("Now for the main event of the night, here comes our first combatant");
            ui.ShowFullStats(attacker);
            ui.ShowMessage("Now, his opponent, the magnificent, the greatest");
            ui.ShowFullStats(defender);

            var battle = new Battle(ui, attacker, defender);
            var result = battle.RunBattle();

            ui.ShowMessage("");
            ui.ShowMessage("----- BATTLE IS OVER ----");
            ui.ShowMessage("WINNER: " + result.Winner.Name);
            ui.ShowHealth(result.Winner);
            ui.ShowMessage("");
            ui.ShowMessage("LOOSER: " + result.Looser.Name);
            ui.ShowHealth(result.Looser);

            Console.ReadKey();
        }
    }
}
