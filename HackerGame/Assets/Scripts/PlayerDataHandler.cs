using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerDataHandler : MonoBehaviour {
    /* Documentation used for this script
     * 
     * Application.persistentDataPath: https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Application-persistentDataPath.html
     * File.WriteAllText: https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-8.0
     * Path.Combine: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.combine?view=net-8.0
     */

    [System.Serializable]
    public class PlayerData {
        [Header("Database related")]
        public string _rev; // This is for the CouchDB -Teemu H. EDIT: Moved this here, looks nicer in inspector this way :) -Teemu K

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

        [Header("Medium Encapsulation data")]
        public List<Medium_Task_Data> task_ME_data = new();
        public int correctAttemptAmount_ME;

        [Header("Medium Abstraction data")]
        public List<Medium_Task_Data> task_MA_data = new();
        public int correctAttemptAmount_MA;

        [Header("Medium Inheritance data")]
        public List<Medium_Task_Data> task_MI_data = new();
        public int correctAttemptAmount_MI;
       
        [Header("Medium Polymorphism data")]
        public List<Medium_Task_Data> task_MP_data = new();
        public int correctAttemptAmount_MP;

        [Header("Hard Encapsulation data")]
        public List<Hard_Task_Data> task_HE_data = new();
        public int correctAttemptAmount_HE;

        [Header("Hard Abstraction data")]
        public List<Hard_Task_Data> task_HA_data = new();
        public int correctAttemptAmount_HA;
       
        [Header("Hard Inheritance data")]
        public List<Hard_Task_Data> task_HI_data = new();
        public int correctAttemptAmount_HI;

        [Header("Hard Polymorphism data")]
        public List<Hard_Task_Data> task_HP_data = new();
        public int correctAttemptAmount_HP;
    }

    //Data seen from the Unity inspector
    public PlayerData currentPlayerData; //I changed this from private to public because protection level -Teemu H

    private void Awake() {
        try {
            //LoadData(); //Placeholder
        } catch {
            Debug.Log("No JSON data yet. Continuing as normal");
        }
    }

    // Login things for usernamecheck - Teemu h
    [Header("Login things for usernamecheck")]
     public TMP_InputField Username_Inputfield;
     public TMP_InputField Email_Inputfield;

    [Header("Register fields")]
    [SerializeField] TMP_InputField reqUsername_Inputfield;
    [SerializeField] TMP_InputField reqEmail_Inputfield;

    public void CheckUsername()
    {
        StartCoroutine(CheckUsernameCoroutine());
    }

    public void CheckUserData() {
        StartCoroutine(CheckUserDataRoutine());
    }

    private IEnumerator CheckUsernameCoroutine()
    {
        string username = Username_Inputfield.text;
        string email = Email_Inputfield.text; // Get email from input field
        string url = $"http://admin:hackergame2024!@44.211.154.174:5984/playerdata/{username}";

        UnityWebRequest getReq = UnityWebRequest.Get(url);
        yield return getReq.SendWebRequest();

        if (getReq.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Username doesn't exist");
        }
        else
        {
            Debug.Log("Username exists");
            currentPlayerData.userName = username; // Save username to PlayerData
            currentPlayerData.email = email; // Save email to PlayerData
            SaveData(username); // Save PlayerData
            SceneManager.LoadScene("Teemu H Progression"); // Load new scene
        }
    }

    //----User checking Teemu K Edition----
    private IEnumerator CheckUserDataRoutine() {
        string username = Username_Inputfield.text;
        string email = Email_Inputfield.text;
        string url = $"http://admin:hackergame2024!@44.211.154.174:5984/playerdata/{username}";

        UnityWebRequest getReq = UnityWebRequest.Get(url);
        yield return getReq.SendWebRequest();

        //First check that Username_Inputfield.text is not empty or null and stop the routine if it is
        if (Username_Inputfield.text == "") yield break;

        if (getReq.result == UnityWebRequest.Result.Success) { //Address found by username
            Debug.Log("Username found");

            //From CouchDB load the data
            string json = getReq.downloadHandler.text;
            PlayerData jsonData = JsonUtility.FromJson<PlayerData>(json);

            if (jsonData.email == email) {
                Debug.Log("Email matches with current user data. Continuing");
                //Set jsonData to PlayerData
                currentPlayerData = jsonData;
                SceneManager.LoadScene("Teemu K");
            }
            else Debug.Log("Email is not correct for the username!");
        }

        else if (getReq.result != UnityWebRequest.Result.Success) Debug.Log(getReq.error);
        getReq.Dispose();
    }
    //---End of checking----

    //Save data to the json file and send it to the server
    public void SaveData(string username) {
        string json = JsonUtility.ToJson(currentPlayerData, true);
        string path = Path.Combine(Application.persistentDataPath, $"currentPlayerData_{username}.json");

        File.WriteAllText(path, json);

        // Read the file content.
        string fileContent = File.ReadAllText(path);

        StartCoroutine(SendDataToServer(fileContent, username));
    }
    public IEnumerator SendDataToServer(string fileContent, string username) {
        // Parse the file content back into a PlayerData object.
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(fileContent);

        //string username = Username_Inputfield.text; Commented this, because it's missing after scene is changed

        // The URL should include the document ID/Player at the end.
        string url = $"http://admin:hackergame2024!@44.211.154.174:5984/playerdata/{username}"; //Note to everyone: make sure the password is not pushed to git! 

        // Send a GET request to the document's URL to retrieve the current _rev value.
        UnityWebRequest getReq = UnityWebRequest.Get(url);
        yield return getReq.SendWebRequest();

        // Convert the PlayerData object back into a JSON string.
        string jsonWithRev;

        if (getReq.result != UnityWebRequest.Result.Success) {
            // If the GET request fails, assume the document doesn't exist and create a new one.
            jsonWithRev = JsonUtility.ToJson(playerData);
        } else {
            // If the GET request succeeds, update the existing document.
            // Parse the response body.
            PlayerData responseData = JsonUtility.FromJson<PlayerData>(getReq.downloadHandler.text);

            // Add the _rev field to the PlayerData object.
            playerData._rev = responseData._rev;

            // Convert the PlayerData object back into a JSON string.
            jsonWithRev = JsonUtility.ToJson(playerData);
        }

        // Send a PUT request to create or update the document.
        UnityWebRequest putReq = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonWithRev);
        putReq.uploadHandler = new UploadHandlerRaw(bodyRaw);
        putReq.downloadHandler = new DownloadHandlerBuffer();
        putReq.SetRequestHeader("Content-Type", "application/json");

        yield return putReq.SendWebRequest();

        if (putReq.result != UnityWebRequest.Result.Success) {
            Debug.Log(putReq.error);
        } else {
            Debug.Log("Data uploaded successfully");
        }
    }

    public void RegisterNewUser() {
        StartCoroutine(RegisterNewUserRoutine(reqUsername_Inputfield.text, reqEmail_Inputfield.text));
    }

    private IEnumerator RegisterNewUserRoutine(string userName, string email) {
        string baseUrl = $"http://admin:hackergame2024!@44.211.154.174:5984";
        string newDocUrl = $"{baseUrl}/playerdata/{userName}";

        //Create a new empty json for database document with username and email filled
        PlayerData newData = new();
        newData.userName = userName;
        newData.email = email;

        //Store the username and email to current data
        currentPlayerData = newData;

        //Create new empty document in the database
        UnityWebRequest request = UnityWebRequest.Put(newDocUrl, "{}");
        
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.Log($"Error creating a new user document: {request.error}\nResponse code: {request.responseCode}");
            request.Dispose();
            currentPlayerData = new();
            yield break; //Get out the routine if error happens, for example username already exists
        } 
        else Debug.Log($"{userName} document created succesfully!");
       
        request.Dispose();

        //This is a workaround solution for saving the registered data.
        SaveData(userName);

        //Empty the currentPlayerdata field after
        currentPlayerData = new();
    }

    // MYSQL DATABASE CONNECTION -Teemu H
    // public IEnumerator SendDataToServer(string json) {
    //     UnityWebRequest www = new UnityWebRequest("http://44.211.154.174/mysqlyoutube/PlayerDataHandler.php", "POST");
    //     byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
    //     www.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //     www.downloadHandler = new DownloadHandlerBuffer();
    //     www.SetRequestHeader("Content-Type", "application/json");

    //     yield return www.SendWebRequest();

    //     if (www.result != UnityWebRequest.Result.Success) {
    //         Debug.Log(www.error);
    //     } else {
    //         Debug.Log("Data uploaded successfully");
    //     }
    // }
  
    //Save from currentPlayerData to json file

    // ORIGINAL PART OF SAVEDATA(), BEFORE Modifications. -Teemu H
    // public void SaveData() {
    //     string json = JsonUtility.ToJson(currentPlayerData, true);
    //     string path = Path.Combine(Application.persistentDataPath, "currentPlayerData.json");

    //     File.WriteAllText(path, json);
    // }

    //Load data from the json file

    // This is for the future. -Teemu H

    // public void LoadData() {
    //     UnityWebRequest www = UnityWebRequest.Get("http://your-couchdb-server/PlayerData/" + currentPlayerData.email);
    //     yield return www.SendWebRequest();

    //     if (www.result != UnityWebRequest.Result.Success) {
    //         Debug.Log(www.error);
    //     } else {
    //         string json = www.downloadHandler.text;
    //         PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);
    //         // ... copy data from loadedData to currentPlayerData ...
    //     }
    // }

    //----This is for testing purposes mainly----
    public void LocalSaveData() {
        string json = JsonUtility.ToJson(currentPlayerData, true);
        string path = Path.Combine(Application.persistentDataPath, "currentPlayerData.json");

        File.WriteAllText(path, json);
    }
    //---End of testing purpose functions----

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

        //Medium ME
        currentPlayerData.task_ME_data = loadedData.task_ME_data;
        currentPlayerData.correctAttemptAmount_ME = loadedData.correctAttemptAmount_ME;
        
        //Medium MA
        currentPlayerData.task_MA_data = loadedData.task_MA_data;
        currentPlayerData.correctAttemptAmount_MA = loadedData.correctAttemptAmount_MA;
        
        //Medium MI
        currentPlayerData.task_MI_data = loadedData.task_MI_data;
        currentPlayerData.correctAttemptAmount_MI = loadedData.correctAttemptAmount_MI;
        
        //Medium MP
        currentPlayerData.task_MP_data = loadedData.task_MP_data;
        currentPlayerData.correctAttemptAmount_MP = loadedData.correctAttemptAmount_MP;

        //Hard HE
        currentPlayerData.task_HE_data = loadedData.task_HE_data;
        currentPlayerData.correctAttemptAmount_HE = loadedData.correctAttemptAmount_HE;

        //Hard HA
        currentPlayerData.task_HA_data = loadedData.task_HA_data;
        currentPlayerData.correctAttemptAmount_HA = loadedData.correctAttemptAmount_HA;

        //Hard HI
        currentPlayerData.task_HI_data = loadedData.task_HI_data;
        currentPlayerData.correctAttemptAmount_HI = loadedData.correctAttemptAmount_HI;

        //Hard HP
        currentPlayerData.task_HP_data = loadedData.task_HP_data;
        currentPlayerData.correctAttemptAmount_HP = loadedData.correctAttemptAmount_HP;
    }
}


