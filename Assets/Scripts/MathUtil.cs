﻿namespace Erntemaschine
{
    internal static class MathUtil
    {
        public static float Renorm(float v, float min, float max)
        {
            return (v - min) / (max - min);
        }

        public static float NormalizeAngle(float angle)
        {
            // reduce the angle  
            angle = angle % 360;

            // force it to be the positive remainder, so that 0 <= angle < 360  
            angle = (angle + 360) % 360;

            // force into the minimum absolute value residue class, so that -180 < angle <= 180  
            if (angle > 180)
                angle -= 360;

            return angle;
        }
    }
}
