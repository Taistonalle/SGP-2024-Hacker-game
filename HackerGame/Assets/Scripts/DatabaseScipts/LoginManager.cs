using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [Header("Register")]
    [SerializeField] TMP_InputField Reg_Email;
    [SerializeField] TMP_InputField Reg_Password;
    [SerializeField] TMP_InputField Reg_Username;

    [Header("Login")]
    [SerializeField] TMP_InputField Log_Email;
    [SerializeField] TMP_InputField Log_Password;

    public async void OnRegisterPressed()
    {
        if(string.IsNullOrWhiteSpace(Reg_Email.text))
        {
            Debug.LogError("Please enter a valid email");
            return;
        }

        if(await MySqlManager.RegisterUser(Reg_Email.text, Reg_Password.text, Reg_Username.text))
        {
            print("Successfully Registered!");       
        }
        else print("Failed to Register User!");
    }

    public async void OnLoginPressed()
    {
        if(string.IsNullOrWhiteSpace(Log_Email.text))
        {
            Debug.LogError("Please enter a valid email");
            return;
        }
        
        if(string.IsNullOrWhiteSpace(Log_Password.text))
        {
            Debug.LogError("Please enter a valid password");
            return;
        }

        (bool success, string username) = await MySqlManager.LoginUser(Log_Email.text, Log_Password.text);
        if(success) 
        {
            print("Successfully Logged in User: " + username);       
            SendEmailToPlayerDataHandler(Log_Email.text);
        }
        else 
        {
            print("Failed to Log in User!");
            ClearEmailInPlayerDataHandler(); // Call method to clear email
        }
    }

    private void SendEmailToPlayerDataHandler(string email)
    {
        // Find the PlayerDataHandler object in the scene
        PlayerDataHandler playerDataHandler = FindObjectOfType<PlayerDataHandler>();
        if(playerDataHandler != null)
        {
            // Access the PlayerData class and set the email address
            playerDataHandler.currentPlayerData.email = email;
            print("Email sent to PlayerDataHandler: " + email);
        }
        else
        {
            Debug.LogError("PlayerDataHandler not found in the scene!");
        }
    }

    private void ClearEmailInPlayerDataHandler()
    {
        // Find the PlayerDataHandler object in the scene
        PlayerDataHandler playerDataHandler = FindObjectOfType<PlayerDataHandler>();
        if(playerDataHandler != null)
        {
            // Clear the email address
            playerDataHandler.currentPlayerData.email = "";
            print("Email cleared in PlayerDataHandler");
        }
        else
        {
            Debug.LogError("PlayerDataHandler not found in the scene!");
        }
    }
}



////////////////////////////////
///// ORIGINAL SCRIPT BACKUP

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class LoginManager : MonoBehaviour
// {
//     [Header("Register")]
//     [SerializeField] TMP_InputField Reg_Email;
//     [SerializeField] TMP_InputField Reg_Password;
//     [SerializeField] TMP_InputField Reg_Username;

//     [Header("Login")]
//     [SerializeField] TMP_InputField Log_Email;
//     [SerializeField] TMP_InputField Log_Password;

//     public async void OnRegisterPressed()
//     {
//         if(string.IsNullOrWhiteSpace(Reg_Email.text))
//         {
//             Debug.LogError("Please enter a valid email");
//             return;
//         }

//         if(await MySqlManager.RegisterUser(Reg_Email.text, Reg_Password.text, Reg_Username.text))
//         {
//             print("Successfully Registered!");       
//         }
//         else print("Failed to Register User!");
//     }

//     public async void OnLoginPressed()
//     {
//         if(string.IsNullOrWhiteSpace(Log_Email.text))
//         {
//             Debug.LogError("Please enter a valid email");
//             return;
//         }
        
//         if(string.IsNullOrWhiteSpace(Log_Password.text))
//         {
//             Debug.LogError("Please enter a valid password");
//             return;
//         }

//         (bool success, string username) = await MySqlManager.LoginUser(Log_Email.text, Log_Password.text);
//         if(success) 
//         {
//             print("Successfully Logged in User: " + username);       
//         }
//         else 
//         {
//             print("Failed to Log in User!");
//         }
//     }
// }    

