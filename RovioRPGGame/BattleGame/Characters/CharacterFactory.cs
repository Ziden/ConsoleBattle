
using System;

namespace RovioRPGGame.BattleGame.Characters
{
    public class CharacterFactory
    {
        public static BattleCharacter GetCharacter<Template>() where Template : ICharacter, new()
        {
            var template = Activator.CreateInstance<Template>();
            return new BattleCharacter()
            {
                Stats = template.GetStats(),
                Name = template.GetName(),
                Skills = template.GetSkills(),
            };
        }
    }
}
