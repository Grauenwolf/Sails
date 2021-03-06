using System;
using System.Collections;
using System.Globalization;
using System.Numerics;
using System.Windows.Data;
using Tortuga.Sails.Converters.Internals;

namespace Tortuga.Sails.Converters
{
    /// <summary>
    /// Converts null to false.
    /// Converts 0 to false, other numbers to true.
    /// Throws if the type isn't a number, string, or list
    /// </summary>
    [ValueConversion(typeof(BigInteger), typeof(bool))]
    [ValueConversion(typeof(BigInteger), typeof(bool?))]
    [ValueConversion(typeof(BigInteger?), typeof(bool))]
    [ValueConversion(typeof(BigInteger?), typeof(bool?))]
    [ValueConversion(typeof(byte), typeof(bool))]
    [ValueConversion(typeof(byte), typeof(bool?))]
    [ValueConversion(typeof(byte?), typeof(bool))]
    [ValueConversion(typeof(byte?), typeof(bool?))]
    [ValueConversion(typeof(decimal), typeof(bool))]
    [ValueConversion(typeof(decimal), typeof(bool?))]
    [ValueConversion(typeof(decimal?), typeof(bool))]
    [ValueConversion(typeof(decimal?), typeof(bool?))]
    [ValueConversion(typeof(double), typeof(bool))]
    [ValueConversion(typeof(double), typeof(bool?))]
    [ValueConversion(typeof(double?), typeof(bool))]
    [ValueConversion(typeof(double?), typeof(bool?))]
    [ValueConversion(typeof(ICollection), typeof(bool))]
    [ValueConversion(typeof(ICollection), typeof(bool?))]
    [ValueConversion(typeof(IEnumerable), typeof(bool))]
    [ValueConversion(typeof(IEnumerable), typeof(bool?))]
    [ValueConversion(typeof(short), typeof(bool))]
    [ValueConversion(typeof(short), typeof(bool?))]
    [ValueConversion(typeof(short?), typeof(bool))]
    [ValueConversion(typeof(short?), typeof(bool?))]
    [ValueConversion(typeof(int), typeof(bool))]
    [ValueConversion(typeof(int), typeof(bool?))]
    [ValueConversion(typeof(int?), typeof(bool))]
    [ValueConversion(typeof(int?), typeof(bool?))]
    [ValueConversion(typeof(long), typeof(bool))]
    [ValueConversion(typeof(long), typeof(bool?))]
    [ValueConversion(typeof(long?), typeof(bool))]
    [ValueConversion(typeof(long?), typeof(bool?))]
    [ValueConversion(typeof(sbyte), typeof(bool))]
    [ValueConversion(typeof(sbyte), typeof(bool?))]
    [ValueConversion(typeof(sbyte?), typeof(bool))]
    [ValueConversion(typeof(sbyte?), typeof(bool?))]
    [ValueConversion(typeof(float), typeof(bool))]
    [ValueConversion(typeof(float), typeof(bool?))]
    [ValueConversion(typeof(float?), typeof(bool))]
    [ValueConversion(typeof(float?), typeof(bool?))]
    [ValueConversion(typeof(string), typeof(bool))]
    [ValueConversion(typeof(string), typeof(bool?))]
    [ValueConversion(typeof(ushort), typeof(bool))]
    [ValueConversion(typeof(ushort), typeof(bool?))]
    [ValueConversion(typeof(ushort?), typeof(bool))]
    [ValueConversion(typeof(ushort?), typeof(bool?))]
    [ValueConversion(typeof(uint), typeof(bool))]
    [ValueConversion(typeof(uint), typeof(bool?))]
    [ValueConversion(typeof(uint?), typeof(bool))]
    [ValueConversion(typeof(uint?), typeof(bool?))]
    [ValueConversion(typeof(ulong), typeof(bool))]
    [ValueConversion(typeof(ulong), typeof(bool?))]
    [ValueConversion(typeof(ulong?), typeof(bool))]
    [ValueConversion(typeof(ulong?), typeof(bool?))]
    public partial class NotZeroToTrueConverter : OneWayMarkupValueConverter<NotZeroToTrueConverter>
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">Boolean</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="System.ArgumentException">value is not a number, string, or list - value</exception>

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CheckTargetType(targetType, typeof(bool), typeof(bool?));

            if (value == null)
                return false;

            //Common types
            if (value is int)
                return (int)value != 0;
            if (value is decimal)
                return (decimal)value != 0;
            if (value is float)
                return (float)value != 0;
            if (value is double)
                return (double)value != 0;

            if (value is string)
                return ((string)value).Length != 0;
            if (value is ICollection)
                return ((ICollection)value).Count != 0;
            if (value is IEnumerable)
            {
                var enumerator = ((IEnumerable)value).GetEnumerator();

                if (enumerator.MoveNext()) //count >= 1
                    return true;
                else
                    return false;
            }

            //other numeric types
            if (value is byte)
                return (byte)value != 0;
            if (value is sbyte)
                return (sbyte)value != 0;
            if (value is short)
                return (short)value != 0;
            if (value is ushort)
                return (ushort)value != 0;
            if (value is uint)
                return (uint)value != 0;
            if (value is long)
                return (long)value != 0;
            if (value is ulong)
                return (ulong)value != 0;
            if (value is BigInteger)
                return (BigInteger)value != 0;

            throw new ArgumentException($"{nameof(value)} is not a number, string, or list", nameof(value));
        }
    }
}
