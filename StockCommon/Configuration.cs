﻿using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;


namespace StockCommon
{
    public class Configuration
    {
        public static string SqlConnectStr { get; set; }
        public static string StockList { get; set; }
        public static string StockCSVHeader { get; set; }
        public static string StockItemUrl { get; set; }
        static Configuration()
        {

        }
        public Configuration()
        {
        }
    }

    //-------------------------------------------------------------------------------------------------------

    public static class ConfigLoader
    {
        private const BindingFlags PublicStaticIgnoreCase = BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public;

        private const BindingFlags PublicInstanceIgnoreCase = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;

        public static void Load(string filename, object configurationObject)
        {
            if (!File.Exists(filename) || configurationObject == null)
            {
                return;
            }

            var configEntries = File.ReadAllLines(filename)
                                    .Where(l => !l.StartsWith("#"))
                                    .Select(l => l.Split(new[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries))
                                    .ToArray()
                                    .Where(c => c.Length == 2);

            foreach (var configEntry in configEntries)
            {
                SetEntry(configEntry[0].Trim(), configEntry[1].Trim(), configurationObject);
            }
        }

        private static void SetEntry(string key, string value, object configurationObject)
        {
            if (SetStaticProperty(key, value, configurationObject))
            {
                return;
            }

            SetInstanceProperty(key, value, configurationObject);
        }

        private static bool SetStaticProperty(string key, string value, object configurationObject)
        {
            var property = configurationObject.GetType().GetProperty(key, PublicStaticIgnoreCase);

            if (property == null)
            {
                return false;
            }

            try
            {
                var convertedValue = Convert(value, property.PropertyType);

                property.SetValue(configurationObject, convertedValue, null);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool SetInstanceProperty(string key, string value, object configurationObject)
        {
            var property = configurationObject.GetType().GetProperty(key, PublicInstanceIgnoreCase);

            if (property == null)
            {
                return false;
            }

            try
            {
                var convertedValue = Convert(value, property.PropertyType);

                property.SetValue(configurationObject, convertedValue, null);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static object Convert(string input, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return input;
            }

            var converter = TypeDescriptor.GetConverter(destinationType);

            if (converter == null || !converter.CanConvertFrom(typeof(string)))
            {
                return null;
            }

            try
            {
                return converter.ConvertFrom(input);
            }
            catch (FormatException)
            {
                if (destinationType == typeof(bool) && converter.GetType() == typeof(BooleanConverter) && "on".Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return null;
            }
        }
    }
}