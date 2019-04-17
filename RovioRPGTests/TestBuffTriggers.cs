
using NUnit.Framework;
using RovioRPGGame.BattleGame.Buffs;
using RovioRPGGame.BattleGame.Buffs.Triggers;
using RovioRPGGame.BattleGame.Characters;
using RovioRPGGame.BattleGame.Skills;
using System;
using System.Collections.Generic;

namespace RovioRPGTests
{
    [TestFixture]
    public class TestBuffTriggers
    {
    
        private BattleCharacter character;
        private ActiveBuff activeBuff;

        [SetUp]
        public void Setup()
        {
            character = new BattleCharacter();
            character.Skills = new List<ISkill>();
            character.Stats = new Stats();
            character.ActiveBuffs = new List<ActiveBuff>();
            activeBuff = BuffFactory.GetActiveBuff(new TestBuff(), character);
        }

        public class TestBuff : IBuff, IBuffApplyTrigger, IBuffRemoveTrigger, ITurnEndTrigger
        {
            public List<Type> Triggered = new List<Type>();

            public int GetDurationInTurns()
            {
                return -1;
            }

            public void OnBuffApplied(BattleCharacter affected)
            {
                Triggered.Add(typeof(IBuffApplyTrigger));
            }

            public void OnBuffRemoved(BattleCharacter affected)
            {
                Triggered.Add(typeof(IBuffRemoveTrigger));
            }

            public void OnTurnEnded(BattleCharacter affected)
            {
                Triggered.Add(typeof(ITurnEndTrigger));
            }
        }

        [Test]
        public void TestTurnEndTrigger()
        {
            activeBuff.Trigger<ITurnEndTrigger>();
            var testBuff = (TestBuff)activeBuff.Buff;

            Assert.IsTrue(testBuff.Triggered.Contains(typeof(ITurnEndTrigger)));
        }

        [Test]
        public void TestBuffApplyTrigger()
        {
            activeBuff.Trigger<IBuffApplyTrigger>();
            var testBuff = (TestBuff)activeBuff.Buff;

            Assert.IsTrue(testBuff.Triggered.Contains(typeof(IBuffApplyTrigger)));
        }

        [Test]
        public void TestBuffRemoveTrigger()
        {
            activeBuff.Trigger<IBuffRemoveTrigger>();
            var testBuff = (TestBuff)activeBuff.Buff;

            Assert.IsTrue(testBuff.Triggered.Contains(typeof(IBuffRemoveTrigger)));
        }

    }
}
