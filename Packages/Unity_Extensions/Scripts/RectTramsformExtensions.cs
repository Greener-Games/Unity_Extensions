using UnityEngine;

namespace GG.Extensions
{
    /// <summary>
    /// Defines preset anchor positions for RectTransforms.
    /// </summary>
    public enum AnchorPresets
    {
        TopLeft,
        TopCenter,
        TopRight,
 
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
 
        BottomLeft,
        BottonCenter,
        BottomRight,
        BottomStretch,
 
        VertStretchLeft,
        VertStretchRight,
        VertStretchCenter,
 
        HorStretchTop,
        HorStretchMiddle,
        HorStretchBottom,
 
        StretchAll
    }
    /// <summary>
    /// Defines preset pivot positions for RectTransforms.
    /// </summary>
    public enum PivotPresets
    {
        TopLeft,
        TopCenter,
        TopRight,
 
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
 
        BottomLeft,
        BottomCenter,
        BottomRight,
    }
    
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Sets the anchor of a RectTransform to a predefined preset.
        /// </summary>
        /// <param name="source">The RectTransform to modify.</param>
        /// <param name="allign">The AnchorPresets value to apply.</param>
        /// <param name="offsetX">Optional X offset from the anchor position.</param>
        /// <param name="offsetY">Optional Y offset from the anchor position.</param>
        public static void SetAnchor(this RectTransform source, AnchorPresets allign, int offsetX=0, int offsetY=0)
        {
            source.anchoredPosition = new Vector3(offsetX, offsetY, 0);
 
            switch (allign)
            {
                case(AnchorPresets.TopLeft):
                {
                    source.anchorMin = new Vector2(0, 1);
                    source.anchorMax = new Vector2(0, 1);
                    break;
                }
                case (AnchorPresets.TopCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 1);
                    source.anchorMax = new Vector2(0.5f, 1);
                    break;
                }
                case (AnchorPresets.TopRight):
                {
                    source.anchorMin = new Vector2(1, 1);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
 
                case (AnchorPresets.MiddleLeft):
                {
                    source.anchorMin = new Vector2(0, 0.5f);
                    source.anchorMax = new Vector2(0, 0.5f);
                    break;
                }
                case (AnchorPresets.MiddleCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0.5f);
                    source.anchorMax = new Vector2(0.5f, 0.5f);
                    break;
                }
                case (AnchorPresets.MiddleRight):
                {
                    source.anchorMin = new Vector2(1, 0.5f);
                    source.anchorMax = new Vector2(1, 0.5f);
                    break;
                }
 
                case (AnchorPresets.BottomLeft):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(0, 0);
                    break;
                }
                case (AnchorPresets.BottonCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0);
                    source.anchorMax = new Vector2(0.5f,0);
                    break;
                }
                case (AnchorPresets.BottomRight):
                {
                    source.anchorMin = new Vector2(1, 0);
                    source.anchorMax = new Vector2(1, 0);
                    break;
                }
 
                case (AnchorPresets.HorStretchTop):
                {
                    source.anchorMin = new Vector2(0, 1);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
                case (AnchorPresets.HorStretchMiddle):
                {
                    source.anchorMin = new Vector2(0, 0.5f);
                    source.anchorMax = new Vector2(1, 0.5f);
                    break;
                }
                case (AnchorPresets.HorStretchBottom):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(1, 0);
                    break;
                }
 
                case (AnchorPresets.VertStretchLeft):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(0, 1);
                    break;
                }
                case (AnchorPresets.VertStretchCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0);
                    source.anchorMax = new Vector2(0.5f, 1);
                    break;
                }
                case (AnchorPresets.VertStretchRight):
                {
                    source.anchorMin = new Vector2(1, 0);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
 
                case (AnchorPresets.StretchAll):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
            }
        }

        /// <summary>
        /// Sets the pivot of a RectTransform to a predefined preset.
        /// </summary>
        /// <param name="source">The RectTransform to modify.</param>
        /// <param name="preset">The PivotPresets value to apply.</param>
        public static void SetPivot(this RectTransform source, PivotPresets preset)
        {
 
            switch (preset)
            {
                case (PivotPresets.TopLeft):
                {
                    source.pivot = new Vector2(0, 1);
                    break;
                }
                case (PivotPresets.TopCenter):
                {
                    source.pivot = new Vector2(0.5f, 1);
                    break;
                }
                case (PivotPresets.TopRight):
                {
                    source.pivot = new Vector2(1, 1);
                    break;
                }
 
                case (PivotPresets.MiddleLeft):
                {
                    source.pivot = new Vector2(0, 0.5f);
                    break;
                }
                case (PivotPresets.MiddleCenter):
                {
                    source.pivot = new Vector2(0.5f, 0.5f);
                    break;
                }
                case (PivotPresets.MiddleRight):
                {
                    source.pivot = new Vector2(1, 0.5f);
                    break;
                }
 
                case (PivotPresets.BottomLeft):
                {
                    source.pivot = new Vector2(0, 0);
                    break;
                }
                case (PivotPresets.BottomCenter):
                {
                    source.pivot = new Vector2(0.5f, 0);
                    break;
                }
                case (PivotPresets.BottomRight):
                {
                    source.pivot = new Vector2(1, 0);
                    break;
                }
            }
        }
        
    }
}