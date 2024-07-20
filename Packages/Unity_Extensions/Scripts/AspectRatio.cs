using UnityEngine;
using System.Collections;

namespace GG.Extensions
{
    /// <summary>
    /// Provides utility methods for calculating aspect ratios.
    /// </summary>
    public static class AspectRatio
    {
        /// <summary>
        /// Calculates the aspect ratio from given width and height integers.
        /// </summary>
        /// <param name="x">The width component of the resolution.</param>
        /// <param name="y">The height component of the resolution.</param>
        /// <param name="debug">If true, logs the calculated aspect ratio to the console.</param>
        /// <returns>A Vector2 representing the aspect ratio (x:y).</returns>
        public static Vector2 GetAspectRatio(int x, int y, bool debug = false)
        {
            return GetAspectRatio(new Vector2(x, y), debug);
        }

        /// <summary>
        /// Calculates the aspect ratio from a Vector2 representing width and height.
        /// </summary>
        /// <param name="xy">A Vector2 where x is width and y is height.</param>
        /// <param name="debug">If true, logs the calculated aspect ratio to the console.</param>
        /// <returns>A Vector2 representing the aspect ratio (x:y).</returns>
        public static Vector2 GetAspectRatio(Vector2 xy, bool debug = false)
        {
            float f = xy.x / xy.y;
            int i = 0;
            while (true)
            {
                i++;
                if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
                    break;
            }

            if (debug)
                Debug.Log($"Aspect ratio is {f * i}:{i} (Resolution: {xy.x}x{xy.y})");
            return new Vector2((float)System.Math.Round(f * i, 2), i);
        }
    }
}