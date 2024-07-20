#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace GG.Extensions
{
    public static class LayerMaskExtensions
    {
        /// <summary>
        /// Creates a LayerMask from an array of layer names.
        /// </summary>
        /// <param name="layerNames">Array of layer names to include in the mask.</param>
        /// <returns>A LayerMask composed of the specified layers.</returns>
        public static LayerMask CreateLayerMask(params string[] layerNames)
        {
            return NamesToMask(layerNames);
        }

        /// <summary>
        /// Creates a LayerMask from an array of layer numbers.
        /// </summary>
        /// <param name="layerNumbers">Array of layer numbers to include in the mask.</param>
        /// <returns>A LayerMask composed of the specified layers.</returns>
        public static LayerMask CreateLayerMask(params int[] layerNumbers)
        {
            return LayerNumbersToMask(layerNumbers);
        }

        /// <summary>
        /// Converts an array of layer names into a LayerMask.
        /// </summary>
        /// <param name="layerNames">Array of layer names to convert.</param>
        /// <returns>A LayerMask representing the given layer names.</returns>
        public static LayerMask NamesToMask(params string[] layerNames)
        {
            LayerMask ret = 0;
            foreach (string name in layerNames)
            {
                ret |= 1 << LayerMask.NameToLayer(name);
            }

            return ret;
        }

        /// <summary>
        /// Converts an array of layer numbers into a LayerMask.
        /// </summary>
        /// <param name="layerNumbers">Array of layer numbers to convert.</param>
        /// <returns>A LayerMask representing the given layer numbers.</returns>
        public static LayerMask LayerNumbersToMask(params int[] layerNumbers)
        {
            LayerMask ret = 0;
            foreach (int layer in layerNumbers)
            {
                ret |= 1 << layer;
            }

            return ret;
        }

        /// <summary>
        /// Inverts the given LayerMask.
        /// </summary>
        /// <param name="original">The original LayerMask to invert.</param>
        /// <returns>An inverted LayerMask.</returns>
        public static LayerMask Inverse(this LayerMask original)
        {
            return ~original;
        }

        /// <summary>
        /// Adds layers to the given LayerMask.
        /// </summary>
        /// <param name="original">The original LayerMask to add layers to.</param>
        /// <param name="layerNames">Array of layer names to add.</param>
        /// <returns>A LayerMask with the specified layers added.</returns>
        public static LayerMask AddToMask(this LayerMask original, params string[] layerNames)
        {
            return original | NamesToMask(layerNames);
        }

        /// <summary>
        /// Removes layers from the given LayerMask.
        /// </summary>
        /// <param name="original">The original LayerMask to remove layers from.</param>
        /// <param name="layerNames">Array of layer names to remove.</param>
        /// <returns>A LayerMask with the specified layers removed.</returns>
        public static LayerMask RemoveFromMask(this LayerMask original, params string[] layerNames)
        {
            LayerMask invertedOriginal = ~original;
            return ~(invertedOriginal | NamesToMask(layerNames));
        }

        /// <summary>
        /// Converts a LayerMask to an array of layer names.
        /// </summary>
        /// <param name="original">The LayerMask to convert.</param>
        /// <returns>An array of layer names included in the LayerMask.</returns>
        public static string[] MaskToNames(this LayerMask original)
        {
            List<string> output = new List<string>();

            for (int i = 0; i < 32; ++i)
            {
                int shifted = 1 << i;
                if ((original & shifted) == shifted)
                {
                    string layerName = LayerMask.LayerToName(i);
                    if (!string.IsNullOrEmpty(layerName))
                    {
                        output.Add(layerName);
                    }
                }
            }

            return output.ToArray();
        }

        /// <summary>
        /// Converts a LayerMask to a string representation using a default delimiter.
        /// </summary>
        /// <param name="original">The LayerMask to convert.</param>
        /// <returns>A string representation of the LayerMask.</returns>
        public static string MaskToString(this LayerMask original)
        {
            return MaskToString(original, ", ");
        }

        /// <summary>
        /// Converts a LayerMask to a string representation using a specified delimiter.
        /// </summary>
        /// <param name="original">The LayerMask to convert.</param>
        /// <param name="delimiter">The delimiter to use between layer names.</param>
        /// <returns>A string representation of the LayerMask.</returns>
        public static string MaskToString(this LayerMask original, string delimiter)
        {
            return string.Join(delimiter, MaskToNames(original));
        }

        /// <summary>
        /// Moves the GameObject associated with the given Transform to a specified layer, optionally applying the change recursively to all children.
        /// </summary>
        /// <param name="root">The root Transform whose GameObject is to be moved to a new layer.</param>
        /// <param name="layer">The name of the layer to move the GameObject to.</param>
        /// <param name="recursive">Whether to apply the layer change to all child Transforms recursively.</param>
        public static void MoveToLayer(this Transform root, string layer, bool recursive = true)
        {
            MoveToLayer(root, LayerMask.NameToLayer(layer), recursive);
        }

        /// <summary>
        /// Moves the GameObject associated with the given Transform to a specified layer, optionally applying the change recursively to all children.
        /// </summary>
        /// <param name="root">The root Transform whose GameObject is to be moved to a new layer.</param>
        /// <param name="layer">The layer number to move the GameObject to.</param>
        /// <param name="recursive">Whether to apply the layer change to all child Transforms recursively.</param>
        public static void MoveToLayer(this Transform root, int layer, bool recursive = true)
        {
            root.gameObject.layer = layer;

            if (recursive)
            {
                foreach (Transform child in root)
                {
                    MoveToLayer(child, layer);
                }
            }
        }

        /// <summary>
        /// Moves all GameObjects associated with the given Transform and its children of type T to a specified layer.
        /// </summary>
        /// <typeparam name="T">The component type to filter the children by.</typeparam>
        /// <param name="root">The root Transform whose children are to be moved to a new layer.</param>
        /// <param name="layer">The name of the layer to move the GameObjects to.</param>
        public static void MoveToLayer<T>(this Transform root, string layer) where T : Component
        {
            MoveToLayer<T>(root, LayerMask.NameToLayer(layer));
        }

        /// <summary>
        /// Moves all GameObjects associated with the given Transform and its children of type T to a specified layer.
        /// </summary>
        /// <typeparam name="T">The component type to filter the children by.</typeparam>
        /// <param name="root">The root Transform whose children are to be moved to a new layer.</param>
        /// <param name="layerNumber">The layer number to move the GameObjects to.</param>
        public static void MoveToLayer<T>(this Transform root, int layerNumber) where T : Component
        {
            foreach (T trans in root.GetComponentsInChildren<T>(true))
            {
                trans.gameObject.layer = layerNumber;
            }
        }

        /// <summary>
        /// Checks if a LayerMask contains a specific layer number.
        /// </summary>
        /// <param name="mask">The LayerMask to check.</param>
        /// <param name="layer">The layer number to check for.</param>
        /// <returns>True if the LayerMask contains the layer, false otherwise.</returns>
        public static bool ContainsLayer(this LayerMask mask, int layer)
        {
            return ((1 << layer) & mask) > 0;
        }

        /// <summary>
        /// Checks if a LayerMask contains a specific layer by name.
        /// </summary>
        /// <param name="mask">The LayerMask to check.</param>
        /// <param name="layer">The name of the layer to check for.</param>
        public static bool ContainsLayer(this LayerMask mask, string layer)
        {
            return ((1 << LayerMask.NameToLayer(layer)) & mask) > 0;
        }
    }
}