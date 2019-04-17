using RovioRPGGame.BattleGame.Buffs;
using RovioRPGGame.BattleGame.Buffs.Triggers;
using RovioRPGGame.BattleGame.Skills;
using RovioRPGGame.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RovioRPGGame.BattleGame.Characters
{
    public class BattleCharacter : ICloneable
    {
        public string Name;
        public Stats Stats;
        public List<ISkill> Skills;
        public List<ActiveBuff> ActiveBuffs = new List<ActiveBuff>();

        public ISkill UseSkill(BattleCharacter target)
        {
            var skillTouse = Skills
                .Where(s => s.GetEnergyCost() <= Stats[Stat.ENERGY])
                .Random();

            skillTouse.UseAbility(this, target);
            ConsumeEnergy(skillTouse.GetEnergyCost());
            return skillTouse;
        }

        public bool IsAlive()
        {
            return Stats[Stat.HP] > 0;
        }

        public void ConsumeEnergy(int amt)
        {
            var energy = Stats[Stat.ENERGY];
            energy -= amt;
            Stats[Stat.ENERGY] = energy;
        }

        public void DealDamageTo(BattleCharacter target, int amount, DamageType type)
        {
            amount = (int)Math.Round(amount * GetDamageMultiplier(type));
            target.RecieveDamage(amount, type);
        }

        public void RecieveDamage(int damage, DamageType type)
        {
            if (HasDodged(type))
                return;
            var hp = Stats[Stat.HP];
            hp -= damage;
            if (hp < 0) hp = 0;
            Stats[Stat.HP] = hp;
        }

        public void DecayBuffs()
        {
            ActiveBuffs
                .Where(ActiveBuff => ActiveBuff.RemainingTurns == 1)
                .ToList()
                .ForEach(activeBuff => RemoveBuff(activeBuff.Buff));

            ActiveBuffs
                .ToList()
                .ForEach(activeBuff => activeBuff.RemainingTurns--);
        }

        public void RemoveBuff(IBuff buff)
        {
            var presentBuff = ActiveBuffs
                .Where(b => b.Buff.GetType() == buff.GetType())
                .FirstOrDefault();

            if (presentBuff != null)
            {
                presentBuff.Trigger<IBuffRemoveTrigger>();
                ActiveBuffs.Remove(presentBuff);
            }
        }

        public void AddBuff(IBuff buff)
        {
            var alreadyHasBuff = ActiveBuffs.Where(b => b.Buff.GetType() == buff.GetType()).Any();
            if (!alreadyHasBuff)
            {
                var activeBuff = BuffFactory.GetActiveBuff(buff, this);
                activeBuff.Trigger<IBuffApplyTrigger>();
                ActiveBuffs.Add(activeBuff);
            }
        }

        public bool HasBuff(IBuff buff)
        {
            return ActiveBuffs.Where(b => b.Buff.GetType() == buff.GetType()).Any();
        }

        private bool HasDodged(DamageType type)
        {
            if (type == DamageType.PHYSICAL)
            {
                var dodgeChange = Stats[Stat.DODGE];
                return Formulas.Chance(dodgeChange);
            }
            return false;
        }

        public double GetDamageMultiplier(DamageType type)
        {
            string affectedStatName = type.ToString() + "_DAMAGE";
                var value = Stats[affectedStatName];
                if (value == 0)
                    return 1;
                return 1 + (value / 100d);
            return 1;
        }

        public object Clone()
        {
            var cloned = new BattleCharacter();
            cloned.Stats = (Stats)this.Stats.Clone();
            cloned.Name = this.Name;
            cloned.Skills = new List<ISkill>(this.Skills);
            cloned.ActiveBuffs = new List<ActiveBuff>(this.ActiveBuffs);
            return cloned;
        }

    }

}
