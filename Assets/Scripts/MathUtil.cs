namespace Erntemaschine
{
    internal static class MathUtil
    {
        public static float Renorm(float v, float min, float max)
        {
            return (v - min) / (max - min);
        }
    }
}
