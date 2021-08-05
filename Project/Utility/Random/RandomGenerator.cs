using System;
using System.Numerics;

namespace Project.Utility.Random
{
    public static class RandomGenerator
    {
        public static float RandomNumber(float minRange, float maxRange)
        {
            var rand = new System.Random(Guid.NewGuid().GetHashCode());
            var val = (float)(rand.NextDouble() * (maxRange - minRange) + minRange);
            return val;
        }
        
        public static int RandomNumber(int minRange, int maxRange)
        {
            var rand = new System.Random(Guid.NewGuid().GetHashCode());
            var val = rand.Next(minRange, maxRange);
            return val;
        }
    }
}