using UnityEngine.InputSystem;

namespace GG.Extensions
{
    /// <summary>
    /// Defines the types of input devices.
    /// </summary>
    public enum DeviceType
    {
        Gamepad,
        Keyboard,
        Mouse,
        Touchscreen,
        Joystick,
        Pen,
        Sensor,
        Unknown
    }

    /// <summary>
    /// Provides extension methods for input-related functionalities.
    /// </summary>
    public static class InputExtensions
    {
        /// <summary>
        /// Determines the type of device from an input action callback context.
        /// </summary>
        /// <param name="context">The callback context from which to determine the device type.</param>
        /// <returns>The determined device type.</returns>
        public static DeviceType GetDeviceType(InputAction.CallbackContext context)
        {
            InputDevice device = context.control.device;

            if (device is Gamepad)
            {
                return DeviceType.Gamepad;
            }
            else if (device is Keyboard)
            {
                return DeviceType.Keyboard;
            }
            else if (device is Mouse)
            {
                return DeviceType.Mouse;
            }
            else if (device is Touchscreen)
            {
                return DeviceType.Touchscreen;
            }
            else if (device is Joystick)
            {
                return DeviceType.Joystick;
            }
            else if (device is Pen)
            {
                return DeviceType.Pen;
            }
            else if (device is Sensor)
            {
                return DeviceType.Sensor;
            }
            else
            {
                return DeviceType.Unknown;
            }
        }

        /// <summary>
        /// Tries to read the value of the specified type from an input action callback context.
        /// </summary>
        /// <typeparam name="T">The type of the value to read.</typeparam>
        /// <param name="context">The callback context from which to read the value.</param>
        /// <param name="value">The output parameter that will contain the read value if successful.</param>
        /// <returns>True if the value was successfully read; otherwise, false.</returns>
        public static bool TryReadValue<T>(this InputAction.CallbackContext context, out T value) where T : struct
        {
            // Initialize the output value to the default
            value = default;

            // Check if the control is not null and the value type is correct
            if (context.control != null && context.control.valueType == typeof(T))
            {
                // Read the value from the context
                value = context.ReadValue<T>();
                return true;
            }

            // If the control is null or the value type does not match, return false
            return false;
        }
    }
}