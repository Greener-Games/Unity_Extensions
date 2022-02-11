using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace GG.Extensions
{
    public static class XrExtensions
    {
        public static bool IsHeadsetPresent()
        {
            var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
            SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
            foreach (var xrDisplay in xrDisplaySubsystems)
            {
                if (xrDisplay.running)
                {
                    return true;
                }
            }
            return false;
        }
    }
}