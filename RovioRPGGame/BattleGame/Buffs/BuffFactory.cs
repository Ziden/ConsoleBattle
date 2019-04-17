
using RovioRPGGame.BattleGame.Buffs;
using RovioRPGGame.BattleGame.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RovioRPGGame.BattleGame.Buffs
{
    public class BuffFactory
    {
        public static ActiveBuff GetActiveBuff(IBuff buff, BattleCharacter buffed)
        {
            var activeBuff = new ActiveBuff()
            {
                Buffed = buffed,
                Buff = buff,
                RemainingTurns = buff.GetDurationInTurns()
            };

            var triggers = GetTriggers(buff);
            triggers.ForEach(trigger =>
            {
                activeBuff.TriggerMethods.Add(GetTriggerMethod(trigger));
            });
            return activeBuff;
        }

        private static List<Type> GetTriggers(IBuff buff)
        {
            return buff.GetType().GetInterfaces()
                .Where(i => i.GetInterfaces().Contains(typeof(IBuffTrigger)))
                .ToList();
        }

        private static MethodInfo GetTriggerMethod(Type trigger)
        {
            return trigger.GetMethods().First();
        } 
    }

 
}
