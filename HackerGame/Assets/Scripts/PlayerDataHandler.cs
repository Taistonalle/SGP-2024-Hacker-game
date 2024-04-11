using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour {
    /* Documentation used for this script
     * 
     * Application.persistentDataPath: https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Application-persistentDataPath.html
     * File.WriteAllText: https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-8.0
     * Path.Combine: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.combine?view=net-8.0
     */

    [System.Serializable]
    public class PlayerData {
        public string email;
        public string userName;
        public bool beatEA; //Example what task could be, in this case EA = Easy Abstraction
            
    }

    //Data seen from the Unity inspector
    [SerializeField] PlayerData currentPlayerData;

    void Start() {
        //SaveData();
        LoadData();
    }


    //Save from currentPlayerData to json file
    public void SaveData() {
        string json = JsonUtility.ToJson(currentPlayerData);
        string path = Path.Combine(Application.persistentDataPath, "currentPlayerData.json");

        File.WriteAllText(path, json);
    }

    //Load data from the json file
    public void LoadData() {
        string path = Path.Combine(Application.persistentDataPath, "currentPlayerData.json");
        string json = File.ReadAllText(path);

        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

        currentPlayerData.email = loadedData.email;
        currentPlayerData.userName = loadedData.userName;
        currentPlayerData.beatEA = loadedData.beatEA;
    }
}
