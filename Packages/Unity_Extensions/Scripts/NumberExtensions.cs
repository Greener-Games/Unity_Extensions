using System;
using UnityEngine;

namespace GG.Extensions
{
    /// <summary>
    /// Provides extension methods for numeric types to perform various rounding operations and checks.
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// Defines the rounding mode to be used.
        /// </summary>
        public enum RoundMode
        {
            /// <summary>
            /// Round to the nearest multiple.
            /// </summary>
            UpDown,
            /// <summary>
            /// Always round up to the nearest multiple.
            /// </summary>
            Up,
            /// <summary>
            /// Always round down to the nearest multiple.
            /// </summary>
            Down
        }

        /// <summary>
        /// Rounds an integer to the nearest multiple of a specified number, with an optional rounding mode.
        /// </summary>
        /// <param name="x">The integer to round.</param>
        /// <param name="multiple">The multiple to round to.</param>
        /// <param name="rm">The rounding mode to use. Defaults to UpDown.</param>
        /// <returns>The rounded integer.</returns>
        public static int NearestMultipleOf(this int x, int multiple, RoundMode rm = RoundMode.UpDown)
        {
            return Mathf.RoundToInt(((float)x).NearestMultipleOf((float)multiple, rm));
        }

        /// <summary>
        /// Rounds a float to the nearest multiple of a specified number, with an optional rounding mode.
        /// </summary>
        /// <param name="x">The float to round.</param>
        /// <param name="multiple">The multiple to round to.</param>
        /// <param name="rm">The rounding mode to use. Defaults to UpDown.</param>
        /// <returns>The rounded float.</returns>
        public static float NearestMultipleOf(this float x, float multiple, RoundMode rm = RoundMode.UpDown)
        {
            float mod = x % multiple;
            float midPoint = multiple / 2.0f;

            if (rm == RoundMode.UpDown)
            {
                if (mod > midPoint)
                {
                    return x + (multiple - mod);
                }
                else
                {
                    return x - mod;
                }
            }
            else if (rm == RoundMode.Up)
            {
                return x + (multiple - mod);
            }
            else // (rm == RoundMode.Down)
            {
                return x - mod;
            }
        }

        /// <summary>
        /// Checks if a float value falls within a specified range, inclusive of the range boundaries.
        /// </summary>
        /// <param name="numberToCheck">The float value to check.</param>
        /// <param name="bottom">The lower boundary of the range.</param>
        /// <param name="top">The upper boundary of the range.</param>
        /// <returns>True if the value falls within the range, false otherwise.</returns>
        public static bool FallsBetween(this float numberToCheck, float bottom, float top)
        {
            return (numberToCheck >= bottom && numberToCheck <= top);
        }

        /// <summary>
        /// Rounds a float value to a specified number of decimal places.
        /// </summary>
        /// <param name="value">The float value to round.</param>
        /// <param name="digits">The number of decimal places to round to.</param>
        /// <returns>The rounded float value.</returns>
        public static float Round(this float value, int digits)
        {
            return (float)Math.Round(value, digits);
        }
    }
}