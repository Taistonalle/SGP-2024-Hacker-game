using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class CheckUserExistence : MonoBehaviour
{
    public TMP_InputField Username_Inputfield; // Assign this in the inspector

    public void CheckUsername()
    {
        StartCoroutine(CheckUsernameCoroutine());
    }

    private IEnumerator CheckUsernameCoroutine()
    {
        string username = Username_Inputfield.text;
        string url = $"http://admin:TOSIHIMMEESALASANA@44.211.154.174:5984/playerdata/{username}";

        UnityWebRequest getReq = UnityWebRequest.Get(url);
        yield return getReq.SendWebRequest();

        if (getReq.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Username doesn't exist");
        }
        else
        {
            Debug.Log("Username exists");
        }
    }
}