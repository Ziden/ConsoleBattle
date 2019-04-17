using System;
using System.Collections.Generic;
using System.Text;

namespace RovioRPGGame.BattleGame
{
    public static class Formulas
    {
        public static Random random = new Random();

        public static bool Chance(int chancePct)
        {
            return random.Next(100) < chancePct;
        }
    }
}
