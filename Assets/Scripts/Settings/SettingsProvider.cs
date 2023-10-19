using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsProvider", menuName = "Settings/SettingsProvider", order = 0)]
public class SettingsProvider : ScriptableObject
{
    private const string Path = "SettingsProvider";
    
    private static Dictionary<Type, ScriptableObject> _settings;

    [SerializeField] private List<ScriptableObject> _settingsList;
    
    public List<ScriptableObject> SettingsList => _settingsList;

    public static T Get<T>() where T : ScriptableObject
    {
        CheckSettings();

        if (_settings.ContainsKey(typeof(T)))
        {
            return (T)_settings[typeof(T)];
        }

        Debug.LogWarning($"Not found settings of type \"{typeof(T).FullName}\"");
        return null;
    }

    private static void CheckSettings()
    {
        if (_settings != null)
            return;

        var settingsContainer = Resources.Load<SettingsProvider>(Path);
        SetupSettings(settingsContainer);
    }

    private static void SetupSettings(SettingsProvider settingsContainer)
    {
        _settings = settingsContainer.SettingsList.ToDictionary(x => x.GetType(), x => x);
    }
}
