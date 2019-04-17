using RovioRPGGame.BattleGame.Buffs;
using RovioRPGGame.BattleGame.Characters;
using System;
using System.Linq;

namespace RovioRPGGame.BattleGame.UI
{
    public class ConsoleInterface : IUserInterface
    {
        public void ShowMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public void NewRound()
        {
            Console.WriteLine("");
            Console.WriteLine(" -- Starting new Round -- ");
        }

        public void ShowFullStats(BattleCharacter character)
        {
            Console.WriteLine(character.Name);
            Console.WriteLine("Stats");
            character.Stats.Keys.ToList().ForEach(stat => Console.WriteLine($"- {stat}: {character.Stats[stat]}"));
            Console.WriteLine("Skills");
            character.Skills.ForEach(skill => Console.WriteLine($"- {skill.GetType().Name}"));
            Console.WriteLine("Buffs");
            if (character.ActiveBuffs.Count == 0)
                Console.WriteLine("- No Buffs :(");
            else
                character.ActiveBuffs.ForEach(activeBuff => Console.WriteLine($"- {activeBuff.Buff.GetType().Name}"));
           
        }

        public void ShowBuffsChanges(BattleCharacter before, BattleCharacter after)
        {
            var beforeBuffs = before.ActiveBuffs.Select(activeBuff => activeBuff.Buff.GetType().Name).ToList();
            var afterBuffs = after.ActiveBuffs.Select(activeBuff => activeBuff.Buff.GetType().Name).ToList();

            beforeBuffs.Except(afterBuffs)
                .ToList()
                .ForEach(removedBuff =>
                {
                    Console.WriteLine($"{before.Name} is no longer {removedBuff}");
                });

            afterBuffs
                .Except(beforeBuffs)
                .ToList()
                .ForEach(addedBuff =>
                {
                    Console.WriteLine($"{before.Name} is now {addedBuff}");
                });
        }

        public void ShowBuffTrigger(IBuff buff, BattleCharacter before, BattleCharacter after)
        {
            Console.WriteLine($"{before.Name} feels the effect of {buff.GetType().Name}");
            ShowStatDifferences(before, after);
        }


        public void ShowStatDifferences(BattleCharacter before, BattleCharacter after)
        {
            foreach(var attr in Stats.GetStatTypes())
            {
                var oldValue = before.Stats[attr];
                var newValue = after.Stats[attr];
                var difference = newValue - oldValue;
                if(difference != 0)
                {
                    Console.WriteLine($"{before.Name} {attr.ToString()} {difference}");
                }
            }
        }

        public void ShowHealth(BattleCharacter character)
        {
            var hp = character.Stats[Stat.HP];
            var maxHp = character.Stats[Stat.MAXHP];
            Console.WriteLine($"{character.Name} HP: {hp}/{maxHp}");
        }
    }
}
