using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace GG.Extensions.Vr
{
    public static class InteractionLayerMaskExtensions
    {
        public static InteractionLayerMask CreateLayerMask(params string[] layerNames)
        {
            return NamesToMask(layerNames);
        }

        public static InteractionLayerMask CreateLayerMask(params int[] layerNumbers)
        {
            return LayerNumbersToMask(layerNumbers);
        }

        public static InteractionLayerMask NamesToMask(params string[] layerNames)
        {
            InteractionLayerMask ret = 0;
            foreach (string name in layerNames)
            {
                ret |= 1 << InteractionLayerMask.NameToLayer(name);
            }

            return ret;
        }

        public static InteractionLayerMask LayerNumbersToMask(params int[] layerNumbers)
        {
            InteractionLayerMask ret = 0;
            foreach (int layer in layerNumbers)
            {
                ret |= 1 << layer;
            }

            return ret;
        }

        public static InteractionLayerMask Inverse(this InteractionLayerMask original)
        {
            return ~original;
        }

        public static InteractionLayerMask AddToMask(this InteractionLayerMask original, params string[] layerNames)
        {
            return original | NamesToMask(layerNames);
        }

        public static InteractionLayerMask RemoveFromMask(this InteractionLayerMask original, params string[] layerNames)
        {
            InteractionLayerMask invertedOriginal = ~original;
            return ~(invertedOriginal | NamesToMask(layerNames));
        }

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

        public static string MaskToString(this InteractionLayerMask original)
        {
            return MaskToString(original, ", ");
        }

        public static string MaskToString(this InteractionLayerMask original, string delimiter)
        {
            return string.Join(delimiter, MaskToNames(original));
        }

        public static void MoveToLayer(this Transform root, string layer, bool recursive = true)
        {
            MoveToLayer(root, InteractionLayerMask.NameToLayer(layer), recursive);
        }

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
        
        public static void MoveToLayer<T>(this Transform root, string layer) where T : Component
        {
            MoveToLayer<T>(root, InteractionLayerMask.NameToLayer(layer));
        }
        
        public static void MoveToLayer<T>(this Transform root, int layerNumber) where T : Component
        {
            foreach (T trans in root.GetComponentsInChildren<T>(true))
            {
                trans.gameObject.layer = layerNumber;
            }
        }

        public static bool ContainsLayer(this InteractionLayerMask mask, int layer)
        {
            return ((1 << layer) & mask) > 0;
        }

        public static bool ContainsLayer(this InteractionLayerMask mask, string layer)
        {
            return ((1 << InteractionLayerMask.NameToLayer(layer)) & mask) > 0;
        }
    }
}