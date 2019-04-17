
using NUnit.Framework;
using RovioRPGGame.BattleGame;
using RovioRPGGame.BattleGame.Buffs.BuffList;
using RovioRPGGame.BattleGame.Characters;
using RovioRPGGame.BattleGame.Skills;
using RovioRPGTests.BattleTests;
using System;

namespace RovioRPGTests
{
    [TestFixture]
    public class TestBattles
    {
        BattleCharacter attacker;
        BattleCharacter defender;

        Battle battle;

        [SetUp]
        public void Setup()
        {
            attacker = CharacterFactory.GetCharacter<StubCharacter>();
            defender = CharacterFactory.GetCharacter<StubCharacter>();
            battle = new Battle(new TestInterface(), attacker, defender);
        }

        [Test]
        public void TestBasicDamageDealing()
        {
            battle.DoRound();

            Assert.AreEqual(attacker.Stats[Stat.HP], StubCharacter.INITIAL_HP);
            Assert.Less(defender.Stats[Stat.HP], StubCharacter.INITIAL_HP);

            battle.DoRound(); // attacker becomes defender
            Assert.Less(attacker.Stats[Stat.HP], StubCharacter.INITIAL_HP);
        }

        [Test]
        public void TestDodging()
        {
            defender.Stats[Stat.DODGE] = 100; // 100%

            battle.DoRound();

            Assert.AreEqual(defender.Stats[Stat.HP], StubCharacter.INITIAL_HP);
        }

        [Test]
        public void TestWinnerLooser()
        {
            defender.Stats[Stat.DODGE] = 100;
            defender.Stats[Stat.HP] = 10000;
            attacker.Stats[Stat.HP] = 1;

            var result = battle.RunBattle();

            Assert.AreEqual(defender, result.Winner);
            Assert.AreEqual(attacker, result.Looser);
        }

        [Test]
        public void TestUsingPoison()
        {
            attacker.Skills.Clear();
            attacker.Skills.Add(new Poison());
            attacker.Stats[Stat.ENERGY] = 100;

            battle.DoRound();

            Assert.Less(defender.Stats[Stat.HP], StubCharacter.INITIAL_HP);
            Assert.That(defender.HasBuff(new Poisoned()));

            var beforePoisonTickHP = defender.Stats[Stat.HP];

            battle.DoRound();

            var afterPoisonTickHP = defender.Stats[Stat.HP];

            Assert.Less(afterPoisonTickHP, beforePoisonTickHP);
        }

        [Test]
        public void TestFireballBonusDamage()
        {
            attacker.Skills.Clear();
            attacker.Skills.Add(new Fireball());
            attacker.Stats[Stat.ENERGY] = 100;

            battle.DoRound();

            var firstFireballDamage = StubCharacter.INITIAL_HP - defender.Stats[Stat.HP];

            var fireBonus = 150; // 150% bonus damage
            attacker.Stats[Stat.FIRE_DAMAGE] = fireBonus;

            battle.DoRound();
            battle.DoRound();

            var secondFireballDamage = StubCharacter.INITIAL_HP - firstFireballDamage - defender.Stats[Stat.HP];

            Assert.AreEqual(secondFireballDamage, Math.Round(firstFireballDamage * 2.5));
        }

        [Test]
        public void TestBuffDecayng()
        {
            var buff = new Poisoned();
            attacker.AddBuff(buff);

            for (var x = 0; x < buff.GetDurationInTurns(); x++)
            {
                Assert.That(attacker.HasBuff(buff));
                battle.DoRound();
            }

            Assert.That(!attacker.HasBuff(buff));
        }
    }
}
