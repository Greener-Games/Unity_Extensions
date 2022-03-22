using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace GG.Extensions.Vr
{
    public static class XrExtensions
    {
        public static bool IsHeadsetPresent()
        {
            List<XRDisplaySubsystem> xrDisplaySubsystems = new List<XRDisplaySubsystem>();
            SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
            foreach (XRDisplaySubsystem xrDisplay in xrDisplaySubsystems)
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