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
        [Header("User data")]
        public string email;
        public string userName;

        [Header("Easy encapsulation data")]
        public List<Task_Data> task_EE_data = new();
        public int correctAttemptAmount_EE;

        [Header("Easy Abstraction data")]
        public List<Task_Data> task_EA_data = new();
        public int correctAttemptAmount_EA;

        [Header("Easy Inheritace data")]
        public List<Task_Data> task_EI_data = new();
        public int correctAttemptAmount_EI;
    }

    //Data seen from the Unity inspector
    // I changed this from private to public because protection level -teemu h
    //"Assets\Scripts\DatabaseScipts\LoginManager.cs(66,31): error CS0122: 'PlayerDataHandler.currentPlayerData' is inaccessible due to its protection level"
    public PlayerData currentPlayerData;

    
    //Save from currentPlayerData to json file
    public void SaveData() {
        string json = JsonUtility.ToJson(currentPlayerData, true);
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

        //Easy EE
        currentPlayerData.task_EE_data = loadedData.task_EE_data;
        currentPlayerData.correctAttemptAmount_EE = loadedData.correctAttemptAmount_EE;

        //Easy EA
        currentPlayerData.task_EA_data = loadedData.task_EA_data;
        currentPlayerData.correctAttemptAmount_EA = loadedData.correctAttemptAmount_EA;

        //Easy EI
        currentPlayerData.task_EI_data = loadedData.task_EI_data;
        currentPlayerData.correctAttemptAmount_EI = loadedData.correctAttemptAmount_EI;

        //add more later...
    }
}