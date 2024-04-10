using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class AccountInfoManager : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInputField;
    [SerializeField] TMP_Text accountInfoText;

    public async void OnShowAccountInfoPressed()
    {
        string email = emailInputField.text;

        if (string.IsNullOrEmpty(email))
        {
            accountInfoText.text = "Please enter an email.";
            return; // Exit the method if the email is empty
        }

        // Get account info from the database based on the provided email
        AccountInfo accountInfo = await MySqlManager.GetAccountInfoByEmail(email);
        
        // Display the account info
        if (accountInfo != null)
        {
            accountInfoText.text = "Username: " + accountInfo.username + "\n" +
                                   "Email: " + accountInfo.email;
        }
        else
        {
            accountInfoText.text = "Account not found for the provided email.";
        }
    }
}