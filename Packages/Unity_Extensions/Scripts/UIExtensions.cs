using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GG.Extensions
{
    public static class UiExtensions
    {
        #region ToggleGroups

        public static Toggle GetActive(this ToggleGroup aGroup)
        {
            return aGroup.ActiveToggles().FirstOrDefault();
        }

        #endregion

        static Slider.SliderEvent emptySliderEvent = new Slider.SliderEvent();
        public static void SetValue(this Slider instance, float value)
        {
            var originalEvent = instance.onValueChanged;
            instance.onValueChanged = emptySliderEvent;
            instance.value = value;
            instance.onValueChanged = originalEvent;
        }

        static Toggle.ToggleEvent emptyToggleEvent = new Toggle.ToggleEvent();
        public static void SetValue(this Toggle instance, bool value)
        {
            var originalEvent = instance.onValueChanged;
            instance.onValueChanged = emptyToggleEvent;
            instance.isOn = value;
            instance.onValueChanged = originalEvent;
        }

        static InputField.OnChangeEvent emptyInputFieldEvent = new InputField.OnChangeEvent();
        public static void SetValue(this InputField instance, string value)
        {
            var originalEvent = instance.onValueChanged;
            instance.onValueChanged = emptyInputFieldEvent;
            instance.text = value;
            instance.onValueChanged = originalEvent;
        }
    }
}
