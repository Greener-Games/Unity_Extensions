using UnityEngine;

namespace GG.Extensions
{
    /// <summary>
    /// Provides extension methods for the Renderer component.
    /// </summary>
    public static class RendererExtensions
    {
        /// <summary>
        /// Sets a specific material to a renderer at the given index.
        /// </summary>
        /// <param name="renderer">The renderer to modify.</param>
        /// <param name="index">The index at which to set the material.</param>
        /// <param name="material">The new material to set.</param>
        /// <remarks>
        /// This method modifies the materials array of the renderer. It first copies the current materials into a new array,
        /// replaces the material at the specified index, and then sets the modified array back to the renderer.
        /// This is useful for dynamically changing materials of game objects at runtime.
        /// </remarks>
        public static void SetMaterial(this Renderer renderer, int index, Material material)
        {
            Material[] mats = renderer.materials;
            mats[index] = material;
            renderer.materials = mats;
        }
    }
}