using UnityEngine;

namespace GG.Extensions
{
    public static class RendererExtensions
    {
        public static void SetMaterial(this Renderer renderer, int index, Material material)
        {
            Material[] mats = renderer.materials;
            mats[index] = material;
            renderer.materials = mats;
        }
    }
}