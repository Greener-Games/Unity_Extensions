using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace GG.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the description assigned to the enum value throguh system.componentmodel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T value)
        {
            CheckIsEnum<T>(false);
            string name = Enum.GetName(typeof(T), value);
            if (name != null)
            {
                FieldInfo field = typeof(T).GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                        Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the description assigned to the enum value throguh system.componentmodel,
        /// Use this when passing a generic object though thats an enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionFromObject(this object o)
        {
            if (!o.GetType().IsEnum)
            {
                throw new ArgumentException(string.Format("Type '{0}' is not an enum", o.GetType()));
            }

            string name = Enum.GetName(o.GetType(), o);
            if (name != null)
            {
                FieldInfo field = o.GetType().GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                        Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieves descriptions for all enum values of a specified type.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>An enumerable of descriptions for each enum value.</returns>
        public static IEnumerable<string> GetDescriptions<T>()
        {
            List<string> descs = new List<string>();
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                descs.Add(GetDescription(item));
            }

            return descs;
        }

        /// <summary>
        /// Finds an enum value by its description.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="description">The description to search for.</param>
        /// <returns>The enum value associated with the given description, or the default value if not found.</returns>
        /// <exception cref="ArgumentException">Thrown if T is not an enum type.</exception>
        public static T GetEnumValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException();
            FieldInfo[] fields = type.GetFields();
            var field = fields
                .SelectMany(f => f.GetCustomAttributes(
                    typeof(DescriptionAttribute), false), (
                    f, a) => new { Field = f, Att = a }).SingleOrDefault(a => ((DescriptionAttribute)a.Att)
                    .Description == description);
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }

        /// <summary>
        /// Validates that a type is an enum and optionally checks for the Flags attribute.
        /// </summary>
        /// <typeparam name="T">The type to check.</typeparam>
        /// <param name="withFlags">Whether to check for the Flags attribute.</param>
        /// <exception cref="ArgumentException">Thrown if T is not an enum or doesn't have the Flags attribute when required.</exception>
        private static void CheckIsEnum<T>(bool withFlags)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException(string.Format("Type '{0}' is not an enum", typeof(T).FullName));
            if (withFlags && !Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
                throw new ArgumentException(string.Format("Type '{0}' doesn't have the 'Flags' attribute",
                    typeof(T).FullName));
        }

        /// <summary>
        /// Checks if a specific flag is set in an enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <param name="flag">The flag to check for.</param>
        /// <returns>True if the flag is set, false otherwise.</returns>
        public static bool IsFlagSet<T>(this T value, T flag) where T : struct
        {
            CheckIsEnum<T>(true);
            long lValue = Convert.ToInt64(value);
            long lFlag = Convert.ToInt64(flag);
            return (lValue & lFlag) != 0;
        }

        /// <summary>
        /// Retrieves all flags that are set in an enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <returns>An enumerable of all set flags.</returns>
        public static IEnumerable<T> GetFlags<T>(this T value) where T : struct
        {
            CheckIsEnum<T>(true);
            foreach (T flag in Enum.GetValues(typeof(T)).Cast<T>())
            {
                if (value.IsFlagSet(flag))
                    yield return flag;
            }
        }

        public static T SetFlags<T>(this T value, T flags, bool on) where T : struct
        {
            CheckIsEnum<T>(true);
            long lValue = Convert.ToInt64(value);
            long lFlag = Convert.ToInt64(flags);
            if (on)
            {
                lValue |= lFlag;
            }
            else
            {
                lValue &= (~lFlag);
            }

            return (T)Enum.ToObject(typeof(T), lValue);
        }

        /// <summary>
        /// Joins the specified flags with the current value, setting the specified flags to true.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The current enum value.</param>
        /// <param name="flags">The flags to join with the current value.</param>
        /// <returns>The result of joining the specified flags with the current value.</returns>
        public static T JoinFlags<T>(this T value, T flags) where T : struct => value.SetFlags<T>(flags, true);

        /// <summary>
        /// Sets the specified flags on the current value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The current enum value.</param>
        /// <param name="flags">The flags to set.</param>
        /// <returns>The result of setting the specified flags on the current value.</returns>
        public static T SetFlags<T>(this T value, T flags) where T : struct
        {
            return value.SetFlags(flags, true);
        }

        /// <summary>
        /// Clears the specified flags from the current value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The current enum value.</param>
        /// <param name="flags">The flags to clear.</param>
        /// <returns>The result of clearing the specified flags from the current value.</returns>
        public static T ClearFlags<T>(this T value, T flags) where T : struct
        {
            return value.SetFlags(flags, false);
        }

        /// <summary>
        /// Combines multiple enum flags into a single value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="flags">The collection of enum flags to combine.</param>
        /// <returns>The combined enum value.</returns>
        public static T CombineFlags<T>(this IEnumerable<T> flags) where T : struct
        {
            CheckIsEnum<T>(true);
            long lValue = 0;
            foreach (T flag in flags)
            {
                long lFlag = Convert.ToInt64(flag);
                lValue |= lFlag;
            }

            return (T)Enum.ToObject(typeof(T), lValue);
        }

        /// <summary>
        /// Cycles through the values of an enum, moving to the next value, and wrapping back to the first value after the last.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="enumerable">The current enum value.</param>
        /// <returns>The next enum value, or the first enum value if the current value is the last.</returns>
        public static T CycleEnum<T>(this T enumerable) where T : struct, IConvertible
        {
            int enumLength = Enum.GetValues(typeof(T)).Length;

            int val = (int)(IConvertible)enumerable;
            val++;

            if (val == enumLength)
            {
                val = 0;
            }

            T returnVal = (T)(IConvertible)val;

            return returnVal;
        }

        /// <summary>
        /// Converts the enum values to a list of their names.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>A list of the names of the enum values.</returns>
        public static List<string> AsList<T>() where T : struct, Enum
        {
            return Enum.GetNames(typeof(T)).ToList();
        }
    }
}