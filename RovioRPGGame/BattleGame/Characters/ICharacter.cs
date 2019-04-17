using RovioRPGGame.BattleGame.Skills;
using System.Collections.Generic;

namespace RovioRPGGame.BattleGame.Characters
{
    public interface ICharacter
    {
        List<ISkill> GetSkills();

        Stats GetStats();

        string GetName();
    }
}
