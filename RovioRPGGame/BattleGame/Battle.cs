using RovioRPGGame.BattleGame.Buffs;
using RovioRPGGame.BattleGame.Buffs.Triggers;
using RovioRPGGame.BattleGame.Characters;
using RovioRPGGame.BattleGame.UI;
using System;
using System.Linq;

namespace RovioRPGGame.BattleGame
{
    public class Battle
    {
        private IUserInterface ui;

        public BattleCharacter Attacker;
        public BattleCharacter Defender;

        public Battle(IUserInterface ui, BattleCharacter attacker, BattleCharacter defender)
        {
            this.ui = ui;
            this.Attacker = attacker;
            this.Defender = defender;
        }

        public BattleResult RunBattle()
        {
            ui.ShowMessage($"The battle between {Attacker.Name} and {Defender.Name} is about to start");
            while (Attacker.IsAlive())
                DoRound();

            return new BattleResult()
            {
                Looser = Attacker,
                Winner = Defender
            };  
        }

        public void DoRound()
        {
            ui.NewRound();
            ui.ShowMessage($"{Attacker.Name} Turn");

            RegenEnergy(Attacker);

            AttackerPerformAction();

            ApplyBuffTrigger<ITurnEndTrigger>(Attacker);
            DecayBuffs(Attacker);
            ui.ShowHealth(Defender);

            (Attacker, Defender) = (Defender, Attacker);
        }

        public void AttackerPerformAction()
        {
            var defenderBeforeSkill = (BattleCharacter)Defender.Clone();

            var usedSkill = Attacker.UseSkill(Defender);
            ui.ShowMessage($"{Attacker.Name} {usedSkill.GetFlavourText()} {Defender.Name}");

            ui.ShowBuffsChanges(defenderBeforeSkill, Defender);
            ui.ShowStatDifferences(defenderBeforeSkill, Defender);
        }

        public void DecayBuffs(BattleCharacter character)
        {
            var characterBeforeBuffs = (BattleCharacter)character.Clone();
            character.DecayBuffs();
            ui.ShowBuffsChanges(characterBeforeBuffs, character);
        }

        public void ApplyBuffTrigger<TriggerType>(BattleCharacter attacker)
        {
            foreach (var activeBuff in attacker.ActiveBuffs)
            {
                var attackerBeforeTarget = (BattleCharacter)attacker.Clone();
                if(activeBuff.Trigger<TriggerType>())
                    ui.ShowBuffTrigger(activeBuff.Buff, attackerBeforeTarget, attacker);
            }
        }

        public void RegenEnergy(BattleCharacter character)
        {
            var energy = character.Stats[Stat.ENERGY];
            energy += BattleConfig.ENERGY_PER_TURN;
            character.Stats[Stat.ENERGY] = energy;
        }

    }
}
