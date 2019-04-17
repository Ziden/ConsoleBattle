using RovioRPGGame.BattleGame.Characters;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RovioRPGGame.BattleGame.Buffs
{
    public class ActiveBuff
    {
        public BattleCharacter Buffed;
        public IBuff Buff;
        public int RemainingTurns;
        public List<MethodInfo> TriggerMethods = new List<MethodInfo>();

        public bool Trigger<Trigger>()
        {
            if (Buff is Trigger)
            {
                TriggerMethods.ForEach(method =>
                {
                    if (method.DeclaringType == typeof(Trigger))
                    {
                        method.Invoke(Buff, new object[] { Buffed });
                    }
                });
                return true;
            }
            return false;
        }
    }
}
