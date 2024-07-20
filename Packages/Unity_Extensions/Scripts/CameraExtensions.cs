using System.Collections.Generic;
using UnityEngine;

namespace GG.Extensions
{
    public static class CameraExtensions
    {
        /// <summary>
        /// Shows layers specified by a bitmask on the camera's culling mask.
        /// </summary>
        /// <param name="cam">The camera to modify.</param>
        /// <param name="layerMask">The layer mask to show.</param>
        public static void LayerCullingShow(this Camera cam, int layerMask)
        {
            cam.cullingMask |= layerMask;
        }

        /// <summary>
        /// Shows a single layer specified by name on the camera's culling mask.
        /// </summary>
        /// <param name="cam">The camera to modify.</param>
        /// <param name="layer">The name of the layer to show.</param>
        public static void LayerCullingShow(this Camera cam, string layer)
        {
            LayerCullingShow(cam, 1 << LayerMask.NameToLayer(layer));
        }

        /// <summary>
        /// Shows multiple layers specified by names on the camera's culling mask.
        /// </summary>
        /// <param name="camera">The camera to modify.</param>
        /// <param name="layerNames">The names of the layers to show.</param>
        public static void LayerCullingShow(this Camera camera, params string[] layerNames)
        {
            foreach (string layerName in layerNames)
            {
                LayerCullingShow(camera, layerName);
            }
        }

        /// <summary>
        /// Hides layers specified by a bitmask from the camera's culling mask.
        /// </summary>
        /// <param name="cam">The camera to modify.</param>
        /// <param name="layerMask">The layer mask to hide.</param>
        public static void LayerCullingHide(this Camera cam, int layerMask)
        {
            cam.cullingMask &= ~layerMask;
        }

        /// <summary>
        /// Hides a single layer specified by name from the camera's culling mask.
        /// </summary>
        /// <param name="cam">The camera to modify.</param>
        /// <param name="layer">The name of the layer to hide.</param>
        public static void LayerCullingHide(this Camera cam, string layer)
        {
            LayerCullingHide(cam, 1 << LayerMask.NameToLayer(layer));
        }

        /// <summary>
        /// Hides multiple layers specified by names from the camera's culling mask.
        /// </summary>
        /// <param name="camera">The camera to modify.</param>
        /// <param name="layerNames">The names of the layers to hide.</param>
        public static void LayerCullingHide(this Camera camera, params string[] layerNames)
        {
            foreach (string layerName in layerNames)
            {
                LayerCullingHide(camera, layerName);
            }
        }

        /// <summary>
        /// Toggles the visibility of layers specified by a bitmask on the camera's culling mask.
        /// </summary>
        /// <param name="cam">The camera to modify.</param>
        /// <param name="layerMask">The layer mask to toggle.</param>
        public static void LayerCullingToggle(this Camera cam, int layerMask)
        {
            cam.cullingMask ^= layerMask;
        }

        /// <summary>
        /// Toggles the visibility of a single layer specified by name on the camera's culling mask.
        /// </summary>
        /// <param name="cam">The camera to modify.</param>
        /// <param name="layer">The name of the layer to toggle.</param>
        public static void LayerCullingToggle(this Camera cam, string layer)
        {
            LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer));
        }

        /// <summary>
        /// Checks if the camera's culling mask includes layers specified by a bitmask.
        /// </summary>
        /// <param name="cam">The camera to check.</param>
        /// <param name="layerMask">The layer mask to check for inclusion.</param>
        /// <returns>True if the layer mask is included in the camera's culling mask; otherwise, false.</returns>
        public static bool LayerCullingIncludes(this Camera cam, int layerMask)
        {
            return (cam.cullingMask & layerMask) > 0;
        }

        /// <summary>
        /// Checks if the camera's culling mask includes a single layer specified by name.
        /// </summary>
        /// <param name="cam">The camera to check.</param>
        /// <param name="layer">The name of the layer to check for inclusion.</param>
        /// <returns>True if the layer is included in the camera's culling mask; otherwise, false.</returns>
        public static bool LayerCullingIncludes(this Camera cam, string layer)
        {
            return LayerCullingIncludes(cam, 1 << LayerMask.NameToLayer(layer));
        }

        /// <summary>
        /// Toggles the visibility of layers specified by a bitmask on the camera's culling mask, with an option to force visibility on or off.
        /// </summary>
        /// <param name="cam">The camera to modify.</param>
        /// <param name="layerMask">The layer mask to toggle.</param>
        /// <param name="isOn">If true, forces the layer(s) to be visible; if false, forces the layer(s) to be hidden.</param>
        public static void LayerCullingToggle(this Camera cam, int layerMask, bool isOn)
        {
            bool included = LayerCullingIncludes(cam, layerMask);
            if (isOn && !included)
            {
                LayerCullingShow(cam, layerMask);
            }
            else if (!isOn && included)
            {
                LayerCullingHide(cam, layerMask);
            }
        }

        /// <summary>
        /// Toggles the visibility of a single layer specified by name on the camera's culling mask, with an option to force visibility on or off.
        /// </summary>
        /// <param name="cam">The camera to modify.</param>
        /// <param name="layer">The name of the layer to toggle.</param>
        /// <param name="isOn">If true, forces the layer to be visible; if false, forces the layer to be hidden.</param>
        public static void LayerCullingToggle(this Camera cam, string layer, bool isOn)
        {
            LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer), isOn);
        }

        /// <summary>
        /// Sets the camera's culling mask to show only the specified layers.
        /// </summary>
        /// <param name="cam">The camera to modify.</param>
        /// <param name="layers">A list of layer names to be made visible.</param>
        public static void SetCullingMask(this Camera cam, List<string> layers)
        {
            cam.cullingMask = 0;
            foreach (string layer in layers)
            {
                cam.LayerCullingShow(layer);
            }
        }

        /// <summary>
        /// Sets the camera's culling mask to show only the specified layer.
        /// This method resets the camera's culling mask before showing the specified layer,
        /// effectively making only the specified layer visible.
        /// </summary>
        /// <param name="cam">The camera to modify.</param>
        /// <param name="layer">The name of the layer to be made visible.</param>
        public static void SetCullingMask(this Camera cam, string layer)
        {
            cam.cullingMask = 0;
            cam.LayerCullingShow(layer);
        }
    }
}