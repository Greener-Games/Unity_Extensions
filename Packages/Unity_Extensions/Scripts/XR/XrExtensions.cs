using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace GG.Extensions.Vr
{
    /// <summary>
    /// Provides extension methods for XR (Extended Reality) functionalities.
    /// </summary>
    public static class XrExtensions
    {
        /// <summary>
        /// Checks if an XR headset is currently present and running.
        /// </summary>
        /// <returns>True if an XR headset is present and running; otherwise, false.</returns>
        public static bool IsHeadsetPresent()
        {
            // Create a list to hold XR display subsystems
            List<XRDisplaySubsystem> xrDisplaySubsystems = new List<XRDisplaySubsystem>();
            // Populate the list with the current XR display subsystem instances
            SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
            // Iterate through each XR display subsystem
            foreach (XRDisplaySubsystem xrDisplay in xrDisplaySubsystems)
            {
                // Check if the XR display subsystem is running
                if (xrDisplay.running)
                {
                    // If running, return true indicating a headset is present
                    return true;
                }
            }
            // If no running XR display subsystem is found, return false
            return false;
        }
    }
}