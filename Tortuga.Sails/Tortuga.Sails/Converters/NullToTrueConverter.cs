﻿using System;
using System.Globalization;
using System.Windows.Data;
using Tortuga.Sails.Converters.Internals;

namespace Tortuga.Sails.Converters
{
    /// <summary>
    /// Returns True if the value is null, otherwise returns false
    /// </summary>
	[ValueConversion(typeof(object), typeof(bool))]
    public class NullToTrueConverter : OneWayMarkupValueConverter<NullToTrueConverter>
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">Any nullable type</param>
        /// <param name="targetType">Boolean</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns True if the value is null, otherwise returns false</returns>

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CheckTargetType(targetType, typeof(bool), typeof(bool?));

            return value == null;
        }
    }
}
