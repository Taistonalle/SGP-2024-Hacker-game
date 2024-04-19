using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking; // NEW ADDITION -Teemu H

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

        [Header("Easy Encapsulation data")]
        public List<Easy_Task_Data> task_EE_data = new();
        public int correctAttemptAmount_EE;

        [Header("Easy Abstraction data")]
        public List<Easy_Task_Data> task_EA_data = new();
        public int correctAttemptAmount_EA;

        [Header("Easy Inheritace data")]
        public List<Easy_Task_Data> task_EI_data = new();
        public int correctAttemptAmount_EI;

        [Header("Easy Polymorphism data")]
        public List<Easy_Task_Data> task_EP_data = new();
        public int correctAttemptAmount_EP;

        //Implement medium datasheets later please - Note from Teemu K

        [Header("Hard Inheritance data")]
        public List<Hard_Task_Data> task_HI_data = new();
        public int correctAttemptAmount_HI;

        [Header("Hard Encapsulation data")]
        public List<Hard_Task_Data> task_HE_data = new();
        public int correctAttemptAmount_HE;

        [Header("Hard Abstraction data")]
        public List<Hard_Task_Data> task_HA_data = new();
        public int correctAttemptAmount_HA;

        [Header("Hard Polymorphism data")]
        public List<Hard_Task_Data> task_HP_data = new();
        public int correctAttemptAmount_HP;
    }

    //Data seen from the Unity inspector
    // I changed this from private to public because protection level -Teemu H
    //"Assets\Scripts\DatabaseScipts\LoginManager.cs(66,31): error CS0122: 'PlayerDataHandler.currentPlayerData' is inaccessible due to its protection level"
    public PlayerData currentPlayerData;


    //NEW PART OF SAVE SAVEDATA() -Teemu H
    public void SaveData() {
        string json = JsonUtility.ToJson(currentPlayerData, true);
        string path = Path.Combine(Application.persistentDataPath, "currentPlayerData.json");

        File.WriteAllText(path, json);

        StartCoroutine(SendDataToServer(json));
    }

    public IEnumerator SendDataToServer(string json) {
        UnityWebRequest www = new UnityWebRequest("http://44.211.154.174/mysqlyoutube/PlayerDataHandler.php", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        } else {
            Debug.Log("Data uploaded successfully");
        }
    }
  
    //Save from currentPlayerData to json file

    // ORIGINAL PART OF SAVEDATA(), BEFORE Modifications. -Teemu H
    // public void SaveData() {
    //     string json = JsonUtility.ToJson(currentPlayerData, true);
    //     string path = Path.Combine(Application.persistentDataPath, "currentPlayerData.json");

    //     File.WriteAllText(path, json);
    // }

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

        //Easy EP
        currentPlayerData.task_EP_data = loadedData.task_EP_data;
        currentPlayerData.correctAttemptAmount_EP = loadedData.correctAttemptAmount_EP;

        //Implement medium sheets

        //Hard HI
        currentPlayerData.task_HI_data = loadedData.task_HI_data;
        currentPlayerData.correctAttemptAmount_HI = loadedData.correctAttemptAmount_HI;

        //Hard HE
        currentPlayerData.task_HE_data = loadedData.task_HE_data;
        currentPlayerData.correctAttemptAmount_HE = loadedData.correctAttemptAmount_HE;

        //Hard HA
        currentPlayerData.task_HA_data = loadedData.task_HA_data;
        currentPlayerData.correctAttemptAmount_HA = loadedData.correctAttemptAmount_HA;

        //Hard HP
        currentPlayerData.task_HP_data = loadedData.task_HP_data;
        currentPlayerData.correctAttemptAmount_HP = loadedData.correctAttemptAmount_HP;
    }
}