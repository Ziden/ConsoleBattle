using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RovioRPGGame.Extensions;

namespace RovioRPGGame.BattleGame.Characters
{
    public enum Stat
    {
        MAXHP,
        HP,
        ENERGY,
        DODGE,
        FIRE_DAMAGE,
        NONE
    }

    /// <summary>
    /// Simple override of dictionaries to return 0 if not present and support enum or string calls
    /// </summary>
    public class Stats : Dictionary<string, int>, ICloneable, IEqualityComparer<Stats>
    {
        public new int this[string key]
        {
            get
            {
                int t;
                return base.TryGetValue(key, out t) ? t : 0;
            }
            set { base[key] = value; }
        }

        public int this[Stat key]
        {
            get
            {
                if (!ContainsKey(key.ToString()))
                    return 0;
                return this[key.ToString()];
            }
            set { this[key.ToString()] = value; }
        }

        public static List<Stat> GetStatTypes()
        {
            return Enum.GetValues(typeof(Stat)).Cast<Stat>().ToList();
        }

        #region Interfaces Implementation
        public object Clone()
        {
            var clone = new Stats();
            foreach (var attr in this.Keys)
            {
                clone[attr] = this[attr];
            }
            return clone;
        }

        public bool Equals(Stats x, Stats y)
        {
            foreach (var attr in Enum.GetNames(typeof(Stat)))
            {
                if (x[attr] != y[attr])
                    return false;
            }
            return true;
        }

        public int GetHashCode(Stats obj)
        {
            return GetHashCode();
        }
        #endregion
    }
}
