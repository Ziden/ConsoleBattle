using System.Collections.Generic;
using RovioRPGGame.BattleGame.Characters;
using RovioRPGGame.BattleGame.Skills;

namespace RovioRPGTests.BattleTests
{
    class StubCharacter : ICharacter
    {
        public static readonly int INITIAL_HP = 100;

        public string GetName()
        {
            return "Stub Character";
        }

        public Stats GetStats()
        {
            var stats = new Stats();
            stats[Stat.MAXHP] = INITIAL_HP;
            stats[Stat.HP] = INITIAL_HP;
            stats[Stat.ENERGY] = 0;
            return stats;
        }

        public List<ISkill> GetSkills()
        {
            List<ISkill> skills = new List<ISkill>();
            skills.Add(new MeleeAttack());
            return skills;
        }
    }
}
