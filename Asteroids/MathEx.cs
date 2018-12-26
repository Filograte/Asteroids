using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public static class MathEx
    {
        public static float Min(params float[] values)
        {
            return values.Min();
        }

        public static float Max(params float[] values)
        {
            return values.Max();
        }
    }
}
