#if Interaction_Toolkit
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace GG.Extensions.Vr
{
    public static class InteractionLayerMaskExtensions
    {
        /// <summary>
        /// Creates a layer mask from the specified layer names.
        /// </summary>
        /// <param name="layerNames">The layer names to include in the mask.</param>
        /// <returns>An InteractionLayerMask representing the specified layers.</returns>
        public static InteractionLayerMask CreateLayerMask(params string[] layerNames)
        {
            return NamesToMask(layerNames);
        }

        /// <summary>
        /// Creates a layer mask from the specified layer numbers.
        /// </summary>
        /// <param name="layerNumbers">The layer numbers to include in the mask.</param>
        /// <returns>An InteractionLayerMask representing the specified layers.</returns>
        public static InteractionLayerMask CreateLayerMask(params int[] layerNumbers)
        {
            return LayerNumbersToMask(layerNumbers);
        }

        /// <summary>
        /// Converts an array of layer names into an InteractionLayerMask.
        /// </summary>
        /// <param name="layerNames">The layer names to convert.</param>
        /// <returns>An InteractionLayerMask representing the specified layers.</returns>
        public static InteractionLayerMask NamesToMask(params string[] layerNames)
        {
            InteractionLayerMask ret = 0;
            foreach (string name in layerNames)
            {
                ret |= 1 << InteractionLayerMask.NameToLayer(name);
            }

            return ret;
        }

        /// <summary>
        /// Converts an array of layer numbers into an InteractionLayerMask.
        /// </summary>
        /// <param name="layerNumbers">The layer numbers to convert.</param>
        /// <returns>An InteractionLayerMask representing the specified layers.</returns>
        public static InteractionLayerMask LayerNumbersToMask(params int[] layerNumbers)
        {
            InteractionLayerMask ret = 0;
            foreach (int layer in layerNumbers)
            {
                ret |= 1 << layer;
            }

            return ret;
        }

        /// <summary>
        /// Inverts the specified InteractionLayerMask.
        /// </summary>
        /// <param name="original">The original InteractionLayerMask to invert.</param>
        /// <returns>An inverted InteractionLayerMask.</returns>
        public static InteractionLayerMask Inverse(this InteractionLayerMask original)
        {
            return ~original;
        }

        /// <summary>
        /// Adds layers to the specified InteractionLayerMask.
        /// </summary>
        /// <param name="original">The original InteractionLayerMask.</param>
        /// <param name="layerNames">The layer names to add.</param>
        /// <returns>An InteractionLayerMask with the specified layers added.</returns>
        public static InteractionLayerMask AddToMask(this InteractionLayerMask original, params string[] layerNames)
        {
            return original | NamesToMask(layerNames);
        }

        /// <summary>
        /// Removes layers from the specified InteractionLayerMask.
        /// </summary>
        /// <param name="original">The original InteractionLayerMask.</param>
        /// <param name="layerNames">The layer names to remove.</param>
        /// <returns>An InteractionLayerMask with the specified layers removed.</returns>
        public static InteractionLayerMask RemoveFromMask(this InteractionLayerMask original,
            params string[] layerNames)
        {
            InteractionLayerMask invertedOriginal = ~original;
            return ~(invertedOriginal | NamesToMask(layerNames));
        }

        /// <summary>
        /// Converts an InteractionLayerMask to an array of layer names.
        /// </summary>
        /// <param name="original">The InteractionLayerMask to convert.</param>
        /// <returns>An array of layer names represented by the InteractionLayerMask.</returns>
        public static string[] MaskToNames(this InteractionLayerMask original)
        {
            List<string> output = new List<string>();

            for (int i = 0; i < 32; ++i)
            {
                int shifted = 1 << i;
                if ((original & shifted) == shifted)
                {
                    string layerName = InteractionLayerMask.LayerToName(i);
                    if (!string.IsNullOrEmpty(layerName))
                    {
                        output.Add(layerName);
                    }
                }
            }

            return output.ToArray();
        }

        /// <summary>
        /// Converts an InteractionLayerMask to a string representation, using a specified delimiter.
        /// </summary>
        /// <param name="original">The InteractionLayerMask to convert.</param>
        /// <param name="delimiter">The delimiter to use between layer names.</param>
        /// <returns>A string representation of the InteractionLayerMask.</returns>
        public static string MaskToString(this InteractionLayerMask original, string delimiter)
        {
            return string.Join(delimiter, MaskToNames(original));
        }

        /// <summary>
        /// Converts an InteractionLayerMask to a string representation with a default delimiter.
        /// </summary>
        /// <param name="original">The InteractionLayerMask to convert.</param>
        /// <returns>A string representation of the InteractionLayerMask, separated by ", ".</returns>
        public static string MaskToString(this InteractionLayerMask original)
        {
            return MaskToString(original, ", ");
        }

        /// <summary>
        /// Moves the GameObject associated with the specified Transform to a new layer, optionally applying the change recursively to all children.
        /// </summary>
        /// <param name="root">The root Transform whose GameObject's layer will be changed.</param>
        /// <param name="layer">The name of the layer to move the GameObject to.</param>
        /// <param name="recursive">Whether to apply the layer change to all child GameObjects recursively.</param>
        public static void MoveToLayer(this Transform root, string layer, bool recursive = true)
        {
            MoveToLayer(root, InteractionLayerMask.NameToLayer(layer), recursive);
        }

        /// <summary>
        /// Moves the GameObject associated with the specified Transform to a new layer, optionally applying the change recursively to all children.
        /// </summary>
        /// <param name="root">The root Transform whose GameObject's layer will be changed.</param>
        /// <param name="layer">The layer number to move the GameObject to.</param>
        /// <param name="recursive">Whether to apply the layer change to all child GameObjects recursively.</param>
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
        /// Moves all GameObjects of a specific component type within the children of the specified Transform to a new layer.
        /// </summary>
        /// <typeparam name="T">The component type to filter the GameObjects by.</typeparam>
        /// <param name="root">The root Transform to search within.</param>
        /// <param name="layer">The name of the layer to move the GameObjects to.</param>
        public static void MoveToLayer<T>(this Transform root, string layer) where T : Component
        {
            MoveToLayer<T>(root, InteractionLayerMask.NameToLayer(layer));
        }

        /// <summary>
        /// Moves all GameObjects of a specific component type within the children of the specified Transform to a new layer.
        /// </summary>
        /// <typeparam name="T">The component type to filter the GameObjects by.</typeparam>
        /// <param name="root">The root Transform to search within.</param>
        /// <param name="layerNumber">The layer number to move the GameObjects to.</param>
        public static void MoveToLayer<T>(this Transform root, int layerNumber) where T : Component
        {
            foreach (T trans in root.GetComponentsInChildren<T>(true))
            {
                trans.gameObject.layer = layerNumber;
            }
        }

        /// <summary>
        /// Checks if the specified InteractionLayerMask contains a specific layer number.
        /// </summary>
        /// <param name="mask">The InteractionLayerMask to check.</param>
        /// <param name="layer">The layer number to check for.</param>
        /// <returns>True if the mask contains the layer, otherwise false.</returns>
        public static bool ContainsLayer(this InteractionLayerMask mask, int layer)
        {
            return ((1 << layer) & mask) > 0;
        }

        /// <summary>
        /// Checks if the specified InteractionLayerMask contains a specific layer name.
        /// </summary>
        /// <param name="mask">The InteractionLayerMask to check.</param>
        /// <param name="layer">The name of the layer to check for.</param>
        /// <returns>True if the mask contains the layer, otherwise false.</returns>
        public static bool ContainsLayer(this InteractionLayerMask mask, string layer)
        {
            return ((1 << InteractionLayerMask.NameToLayer(layer)) & mask) > 0;
        }
    }
}
#endif