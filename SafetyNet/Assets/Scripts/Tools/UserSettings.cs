using UnityEngine;
using System.Collections;

public class UserSettings
{
    private class PrefKeys
    {
        public static string Username = "username";
    }

    #region Username

    public static void SetUsername(string username)
    {
        PlayerPrefs.SetString(PrefKeys.Username, username);
    }

    public static string GetUsername()
    {
        return PlayerPrefs.GetString(PrefKeys.Username, "Default");
    }

    #endregion

    public static void SaveUserSettings()
    {
        PlayerPrefs.Save();
    }
}
