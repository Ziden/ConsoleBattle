
using NUnit.Framework;
using RovioRPGGame.BattleGame;
using RovioRPGGame.BattleGame.Buffs.BuffList;
using RovioRPGGame.BattleGame.Buffs.Triggers;
using RovioRPGGame.BattleGame.Characters;
using RovioRPGGame.BattleGame.Skills;
using System.Collections.Generic;
using static RovioRPGTests.TestBuffTriggers;

namespace RovioRPGTests
{
    [TestFixture]
    public class TestCharacters
    {
        BattleCharacter character;

        [SetUp]
        public void Setup()
        {
            character = new BattleCharacter();
            character.Skills = new List<ISkill>();
            character.Stats = new Stats();
        }

        [Test]
        public void TestUsingRandomSkillWithNoEnergy()
        {
            character.Skills.Add(new Poison());
            character.Skills.Add(new MeleeAttack());
            character.Stats[Stat.ENERGY] = 0;

            for (int x = 0; x < 10; x++)
            {
                var usedSkill = character.UseSkill(character);
                Assert.That(usedSkill is MeleeAttack, 
                    "Character only have energy for melee attacks");
            }
        }

        [Test]
        public void TestDamageMultiplier()
        {
            character.Stats[Stat.FIRE_DAMAGE] = 50;

            Assert.AreEqual(1.5, character.GetDamageMultiplier(DamageType.FIRE));
        }

        [Test]
        public void TestNotAddingDuplicateBuffs()
        {
            character.AddBuff(new Poisoned());
            character.AddBuff(new Poisoned());

            Assert.AreEqual(1, character.ActiveBuffs.Count);
        }

        [Test]
        public void TestAddRemoveBuffTriggering()
        {
            var buff = new TestBuff();

            character.AddBuff(buff);
            Assert.IsTrue(buff.Triggered.Contains(typeof(IBuffApplyTrigger)));

            character.RemoveBuff(buff);
            Assert.IsTrue(buff.Triggered.Contains(typeof(IBuffRemoveTrigger)));
        }

    }
}
