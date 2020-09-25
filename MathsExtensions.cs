using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GG.Extensions
{
	public static class MathsExtensions
	{
		#region Constants

		public static readonly float Sqrt3 = Mathf.Sqrt(3);

		#endregion

		#region Static Methods

		/// <summary>
		/// Linearly interpolates between two values between 0 and 1 if values wrap around from 1 back to 0.
		/// </summary>
		/// <remarks>This is useful, for example, in lerping between angles.</remarks>
		/// <example>
		/// <code>float angleInRad1 = 1;
		/// float angleInRad2 = 5;
		/// float revolution = Mathf.PI * 2;
		/// float interpolation = WLerp(angleInRad1 / revolution, angleInRad2 / revolution, 0.5f);
		/// 
		/// //interpolation == (5 + 1 + Mathf.PI * 2)/2 = 3 + Mathf.PI
		/// </code>
		/// </example>
		public static float Wlerp01(float v1, float v2, float t)
		{
			if (Mathf.Abs(v1 - v2) <= 0.5f)
			{
				return Mathf.Lerp(v1, v2, t);
			}
			else if (v1 <= v2)
			{
				return Frac(Mathf.Lerp(v1 + 1, v2, t));
			}
			else
			{
				return Frac(Mathf.Lerp(v1, v2 + 1, t));
			}
		}

		/// <summary>
		/// Tests whether the given value lies in the range [0, 1).
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <returns><c>true</c> if the given value is equal or greater than 0 and smaller than 1, <c>false</c> otherwise.</returns>
		public static bool InRange01(float value)
		{
			return InRange(value, 0, 1);
		}

		/// <summary>
		/// Tests whether the given value lies in the half-open interval specified by its endpoints, that is, whether the value
		/// lies in the interval <c>[closedLeft, openRight)</c>.
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <param name="closedLeft">The left end of the interval.</param>
		/// <param name="openRight">The right end of the interval.</param>
		/// <returns><c>true</c> if the given value is equal or greater than <c>closedLeft</c> and smaller than <c>openRight</c>, <c>false</c> otherwise.</returns>
		public static bool InRange(float value, float closedLeft, float openRight)
		{
			return value >= closedLeft && value < openRight;
		}

		/// <summary>
		/// Mod operator that also works for negative m.
		/// </summary>
		/// <param name="m">The m.</param>
		/// <param name="n">The n.</param>
		/// <returns>System.Int32.</returns>
		public static int FloorMod(int m, int n)
		{
			if (m >= 0)
			{
				return m % n;
			}

			return (m - 2 * m * n) % n;
		}

		/// <summary>
		/// Mod operator that also works for negative m.
		/// </summary>
		/// <param name="m">The m.</param>
		/// <param name="n">The n.</param>
		/// <returns>System.Int32.</returns>
		public static float FloorMod(float m, float n)
		{
			if (m >= 0)
			{
				return m % n;
			}

			return (m % n) + n;
		}

		/// <summary>
		/// Floor division that also work for negative m.
		/// </summary>
		/// <param name="m">The m.</param>
		/// <param name="n">The n.</param>
		/// <returns>System.Int32.</returns>
		public static int FloorDiv(int m, int n)
		{
			if (m >= 0)
			{
				return m / n;
			}

			int t = m / n;

			if (t * n == m)
			{
				return t;
			}

			return t - 1;
		}

		/// <summary>
		/// Returns the fractional part of a floating point number.
		/// </summary>
		/// <param name="x">The number to get the fractional part of.</param>
		/// <returns>The fractional part of the given number.</returns>
		/// <remarks>The result is always the number minus the number's floor.</remarks>
		public static float Frac(float x)
		{
			return x - Mathf.Floor(x);
		}

		/// <summary>
		/// Returns the sign function evaluated at the given value.
		/// </summary>
		/// <returns>1 if the given value is positive, -1 if it is negative, and 0 if it is 0.</returns>
		public static int Sign(float x)
		{
			if (x > 0) return 1;
			if (x < 0) return -1;

			return 0;
		}

		/// <summary>
		/// Returns the sign function evaluated at the given value.
		/// </summary>
		/// <returns>1 if the given value is positive, -1 if it is negative, and 0 if it is 0.</returns>
		public static int Sign(int p)
		{
			if (p > 0) return 1;
			if (p < 0) return -1;

			return 0;
		}

		#endregion

		#region Obsolete

		[Obsolete("Use FloorDiv instead")]
		public static int Div(int m, int n)
		{
			return FloorDiv(m, n);
		}

		[Obsolete("Use FloorMod instead")]
		public static int Mod(int m, int n)
		{
			return FloorMod(m, n);
		}

		[Obsolete("Use FloorMod instead")]
		public static float Mod(float m, float n)
		{
			return FloorMod(m, n);
		}

		/// <summary>
		/// Returns the highest integer equal to the given float.
		/// </summary>
		[Obsolete("Use Mathf.FloorToInt")]
		public static int FloorToInt(float x)
		{
			return Mathf.FloorToInt(x);
		}

		[Obsolete("Use Frac instead.")]
		public static float Wrap01(float value)
		{
			int n = Mathf.FloorToInt(value);
			float result = value - n;

			return result;
		}

		#endregion
	}
}
