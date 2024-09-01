using UnityEngine;

namespace Utilities
{
    public static class MathUtils
    {
        public static Vector2 Convert(this Vector3 src) => new(src.x, src.z);

        public static Vector3 Convert(this Vector2 src, float y = 0f) => new(src.x, y, src.y);

        public static bool Opposite(this Vector2 src, Vector2 dest) => Opposite(src.x, dest.x) && Opposite(src.y, dest.y);

        private static bool Opposite(this float a, float b)
        {
            if (Mathf.Approximately(a, 0f) || Mathf.Approximately(b, 0f))
                return false;
            
            return a > Mathf.Epsilon ? b < Mathf.Epsilon : b > Mathf.Epsilon;
        }
    }
}