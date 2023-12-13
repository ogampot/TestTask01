using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem
{
    public void Save(UserData userData)
    {
        string json = JsonUtility.ToJson(userData, true);
        File.WriteAllText(GetPath(), json);
    }

    public UserData Load()
    {
        if (!File.Exists(GetPath()))
        {
            UserData emptyUserData = new UserData();
            Save(emptyUserData);
            return emptyUserData;
        }

        string json = File.ReadAllText(GetPath());
        UserData userData = JsonUtility.FromJson<UserData>(json);

        return userData;
    }

    public string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "userdata.json");
    }
}