
// using System.Collections;
// using UnityEngine;
// using UnityEngine.Networking;

// public class PlayerDataHandler : MonoBehaviour {
//     // ...

//     public IEnumerator SaveData() {
//         string json = JsonUtility.ToJson(currentPlayerData, true);

//         UnityWebRequest www = new UnityWebRequest("http://http://44.211.154.174//PlayerDataHandler.php", "POST");
//         byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
//         www.uploadHandler = new UploadHandlerRaw(bodyRaw);
//         www.downloadHandler = new DownloadHandlerBuffer();
//         www.SetRequestHeader("Content-Type", "application/json");

//         yield return www.SendWebRequest();

//         if (www.result != UnityWebRequest.Result.Success) {
//             Debug.Log(www.error);
//         } else {
//             Debug.Log("Data uploaded successfully");
//         }
//     }
// }

// // In this script, replace `"http://yourwebsite.com/yourscript.php"` with the URL of your PHP script. You can call `SaveData` as a coroutine to send the data to the server:

// // StartCoroutine(SaveData());

// // Please note that these scripts are simple examples and do not include any error handling or security measures such as input validation or prepared statements. You should add these in a production environment.