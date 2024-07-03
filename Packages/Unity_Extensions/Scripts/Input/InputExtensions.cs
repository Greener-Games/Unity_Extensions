using UnityEngine.InputSystem;

namespace GG.Extensions
{
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

    public static class InputExtensions
    {
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