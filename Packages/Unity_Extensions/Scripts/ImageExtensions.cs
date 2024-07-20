using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GG.Extensions
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Generates a Texture2D object from a base64 encoded string.
        /// </summary>
        /// <param name="base64">The base64 encoded string representing the image data.</param>
        /// <param name="name">Optional name to assign to the texture. Defaults to an empty string.</param>
        /// <returns>A new Texture2D object created from the base64 string.</returns>
        /// <remarks>
        /// The texture is created with a default size of 16x16 pixels, ARGB32 format, no mipmaps, and bilinear filtering.
        /// The texture is also marked with HideFlags.HideAndDontSave to avoid it being saved with the scene.
        /// </remarks>
        public static Texture2D CreateTextureFromBase64(string base64, string name = "")
        {
            byte[] data = Convert.FromBase64String(base64);
            Texture2D tex = new Texture2D(16, 16, TextureFormat.ARGB32, false, true) {hideFlags = HideFlags.HideAndDontSave, name = name, filterMode = FilterMode.Bilinear};
            tex.LoadImage(data);
            return tex;
        }

        /// <summary>
        /// Creates a Sprite from a base64 encoded string.
        /// </summary>
        /// <param name="base64">The base64 encoded string representing the image data.</param>
        /// <returns>A new Sprite created from the base64 string.</returns>
        /// <remarks>
        /// This method first converts the base64 string into a Texture2D object, then creates a sprite from the entire texture.
        /// The sprite's pivot is set to the center (0.5, 0.5) and pixels per unit to 100.
        /// </remarks>
        public static Sprite CreateSpriteFromBase64(string base64)
        {
            Texture2D tex = CreateTextureFromBase64(base64);
            return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        /// <summary>
        /// Converts a Texture2D object to a base64 encoded string.
        /// </summary>
        /// <param name="texture2D">The Texture2D object to convert.</param>
        /// <returns>A base64 encoded string representing the image data of the Texture2D.</returns>
        /// <remarks>
        /// The texture is first encoded to PNG format before being converted to a base64 string.
        /// </remarks>
        public static string ToBase64Image(Texture2D texture2D)
        {
            byte[] imageData = texture2D.EncodeToPNG();
            return Convert.ToBase64String(imageData);
        }

        /// <summary>
        /// Converts an image file to a base64 encoded string.
        /// </summary>
        /// <param name="path">The file path of the image to convert.</param>
        /// <returns>A base64 encoded string representing the image data.</returns>
        /// <remarks>
        /// The image file is read as a byte array before being converted to a base64 string.
        /// </remarks>
        public static string ToBase64Image(string path)
        {
            byte[] asBytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(asBytes);
        }

        /// <summary>
        /// Loads an image file as a byte array.
        /// </summary>
        /// <param name="path">The file path of the image to load.</param>
        /// <returns>A byte array containing the image data.</returns>
        /// <remarks>
        /// This method directly reads the file content into a byte array without any conversion.
        /// </remarks>
        public static byte[] LoadImageAsBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}