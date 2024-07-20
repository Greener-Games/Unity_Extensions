using UnityEngine;

namespace GG.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts a Color32 to a hexadecimal string representation.
        /// </summary>
        /// <param name="color">The Color32 to convert.</param>
        /// <returns>A hexadecimal string representing the color.</returns>
        public static string ColorToHex(this Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }

        /// <summary>
        /// Converts a hexadecimal string to a Color.
        /// </summary>
        /// <param name="hex">The hexadecimal string to convert. Can be prefixed with "0x" or "#".</param>
        /// <returns>A Color represented by the hexadecimal string.</returns>
        /// <remarks>
        /// Assumes the color is fully opaque unless an alpha value is specified in the hex string.
        /// </remarks>
        public static Color HexToColor(string hex)
        {
            hex = hex.Replace("0x", ""); //in case the string is formatted 0xFFFFFF
            hex = hex.Replace("#", ""); //in case the string is formatted #FFFFFF
            byte a = 255; //assume fully visible unless specified in hex
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            //Only use alpha if the string has enough characters
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return new Color32(r, g, b, a);
        }
        
        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color ChangeColorBrightness(this Color color, float correctionFactor)
        {
            float red = (float)color.r;
            float green = (float)color.g;
            float blue = (float)color.b;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return new Color(red/255, green/255, blue/255, color.a);
        }

        #region GameLogic Extensions

        /// <summary>
        /// Returns whether the color is black or almost black.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static bool IsApproximatelyBlack(this Color color)
        {
            return color.r + color.g + color.b <= Mathf.Epsilon;
        }

        /// <summary>
        /// Returns whether the color is white or almost white.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static bool IsApproximatelyWhite(this Color color)
        {
            return color.r + color.g + color.b >= 1 - Mathf.Epsilon;
        }

        /// <summary>
        /// Returns an opaque version of the given color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color Opaque(this Color color)
        {
            return new Color(color.r, color.g, color.b);
        }

        /// <summary>
        /// Returns a new color that is this color inverted.
        /// </summary>
        /// <param name="color">The color to invert.</param>
        /// <returns></returns>
        public static Color Invert(this Color color)
        {
            return new Color(1 - color.r, 1 - color.g, 1 - color.b, color.a);
        }

        /// <summary>
        /// Returns the same color, but with the specified alpha.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="alpha">The alpha.</param>
        /// <returns>Color.</returns>
        public static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        #endregion
    }
}
