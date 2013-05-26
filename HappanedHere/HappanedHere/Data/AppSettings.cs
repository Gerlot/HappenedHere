using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace HappanedHere.DataStore
{
    public class AppSettings
    {
        IsolatedStorageSettings settings;

        // Key names of settings
        const string UseLocationSettingKeyName = "UseLocationSetting";

        // Default values of settings
        const bool UseLocationSettingDefault = true;

        public AppSettings()
        {
            // Get the settings for this application.
            settings = IsolatedStorageSettings.ApplicationSettings;
        }

        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (settings.Contains(Key))
            {
                // If the value has changed
                if (settings[Key] != value)
                {
                    // Store the new value
                    settings[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                settings.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (settings.Contains(Key))
            {
                value = (T)settings[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }
            return value;
        }

        public void Save()
        {
            settings.Save();
        }

        // Properties for settings
        public bool UseLocationSetting
        {
            get { return GetValueOrDefault<bool>(UseLocationSettingKeyName, UseLocationSettingDefault); }
            set
            {
                if (AddOrUpdateValue(UseLocationSettingKeyName, value))
                {
                    Save();
                }
            }
        }
    }
}
