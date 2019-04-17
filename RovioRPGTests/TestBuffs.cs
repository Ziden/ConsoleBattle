
using NUnit.Framework;
using RovioRPGGame.BattleGame.Buffs;
using RovioRPGGame.BattleGame.Buffs.BuffList;
using RovioRPGGame.BattleGame.Buffs.Triggers;
using RovioRPGGame.BattleGame.Characters;
using RovioRPGGame.BattleGame.Skills;
using System.Collections.Generic;

namespace RovioRPGTests
{
    [TestFixture]
    public class TestBuffs
    {
        public readonly int STARTING_HP = 100;

        private BattleCharacter character;

        [SetUp]
        public void Setup()
        {
            character = new BattleCharacter();
            character.Skills = new List<ISkill>();
            character.Stats = new Stats();
            character.ActiveBuffs = new List<ActiveBuff>();
            character.Stats[Stat.MAXHP] = STARTING_HP;
            character.Stats[Stat.HP] = STARTING_HP;
        }
      
        [Test]
        public void TestPoisonBuff()
        {
            character.AddBuff(new Poisoned());
            character.ActiveBuffs[0].Trigger<ITurnEndTrigger>();

            Assert.That(character.Stats[Stat.HP] < STARTING_HP);
        }

        [Test]
        // https://www.youtube.com/watch?v=It7107ELQvY
        public void TestRingOfFlames()
        {
            var ringOfFlames = new RingOfFlames();

            character.AddBuff(ringOfFlames);
            Assert.That(character.Stats[Stat.FIRE_DAMAGE] > 0);

            character.RemoveBuff(ringOfFlames);

            Assert.That(character.ActiveBuffs.Count == 0);

            Assert.That(character.Stats[Stat.FIRE_DAMAGE] == 0);
        }

        [Test]
        public void TestBuffDecay()
        {
            var poisoned = new Poisoned();
            var duration = poisoned.GetDurationInTurns();

            character.AddBuff(poisoned);
     
            Assert.That(character.ActiveBuffs.Count == 1);

            for(var x = 0; x <= duration; x++)
            {
                character.DecayBuffs();
            }

            Assert.That(character.ActiveBuffs.Count == 0);
        }
    }
}
