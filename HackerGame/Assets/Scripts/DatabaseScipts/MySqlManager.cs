using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;

static class MySqlManager
{
    public static readonly string SERVER_URL = "http://localhost:80/mysqlyoutube";

    public static async Task<bool> RegisterUser(string email, string password, string username)
    {
        string REGISTER_USER_URL = $"{SERVER_URL}/RegisterUser.php";
        return (await SendPostRequest(REGISTER_USER_URL, new Dictionary<string, string>()
        {
            {"email", email},
            {"username", username},
            {"password", password}
        }))
        .success;
    }
    
    public static async Task<(bool success, string username) > LoginUser(string email, string password)
    {
        string LOGIN_USER_URL = $"{SERVER_URL}/Login.php";
        return await SendPostRequest(LOGIN_USER_URL, new Dictionary<string, string>()
        {
            {"email", email},
            {"password", password}
        });
    }

    static async Task<(bool success, string returnMessage)> SendPostRequest(string url, Dictionary<string, string> data)
    {
        using (UnityWebRequest req = UnityWebRequest.Post(url, data))
        {
            // Send the request asynchronously
            var asyncOperation = req.SendWebRequest();

            // Wait until the operation is done
            while (!asyncOperation.isDone)
            {
                await Task.Yield(); // Yield control to Unity to prevent freezing
            }

            // Check for errors
            if (req.result != UnityWebRequest.Result.Success || HasErrorMessage(req.downloadHandler.text))
            {
                return (false, req.downloadHandler.text); // Error occurred 
            }

            return (true, req.downloadHandler.text); // Success
        }
    }

    static bool HasErrorMessage(string msg) => int.TryParse(msg, out var res);
}

public class DatabaseUser
{
    public string Email;
    public string Username;
    public string Password;
}
