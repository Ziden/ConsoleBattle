using RovioRPGGame.BattleGame.Skills;
using System.Collections.Generic;

namespace RovioRPGGame.BattleGame.Characters
{
    public class AngryMagebird : ICharacter
    {
        public string GetName()
        {
            return "Angry MageBird";
        }

        public Stats GetStats()
        {
            var stats = new Stats();
            stats[Stat.MAXHP] = 100;
            stats[Stat.HP] = 100;
            stats[Stat.ENERGY] = 0;
            return stats;
        }

        public List<ISkill> GetSkills()
        {
            List<ISkill> skills = new List<ISkill>();
            skills.Add(new Fireball());
            skills.Add(new IceBolt());
            skills.Add(new MeleeAttack());
            skills.Add(new Poison());
            return skills;
        }
    }
}
