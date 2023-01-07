using UnityEngine;

namespace Erntemaschine
{
    internal static class MiscExtensions
    {
        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector2 Xy(this Vector3 vector) => new Vector2(vector.x, vector.y);
    }
}
