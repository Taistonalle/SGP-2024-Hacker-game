using System.Text; // For StringBuilder
using UnityEngine;
using UnityEngine.UI; // Use Text for Unity UI
using TMPro;

public class PlayerDataDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI displayText;
    public PlayerDataHandler playerDataHandler; // Reference to PlayerDataHandler

    private void Start()
    {
        if (playerDataHandler == null || playerDataHandler.currentPlayerData == null || displayText == null)
        {
            Debug.LogError("PlayerDataHandler or displayText is not set. Please set them in the Inspector.");
            return;
        }

        ShowPlayerData();
    }

    private void ShowPlayerData()
    {
        // Get current player data
        PlayerDataHandler.PlayerData playerData = playerDataHandler.currentPlayerData;

        // Use a StringBuilder for efficient string manipulation
        StringBuilder sb = new StringBuilder();

        // Add basic information
        sb.AppendLine("Player Data:");
        sb.AppendLine($"Email: {playerData.email}");
        sb.AppendLine($"Username: {playerData.userName}");

        // Add easy-level data
        sb.AppendLine("\nEasy Level Data:");
        sb.AppendLine($"Correct Attempt Amount (Encapsulation): {playerData.correctAttemptAmount_EE}");
        sb.AppendLine($"Correct Attempt Amount (Abstraction): {playerData.correctAttemptAmount_EA}");
        sb.AppendLine($"Correct Attempt Amount (Inheritance): {playerData.correctAttemptAmount_EI}");
        sb.AppendLine($"Correct Attempt Amount (Polymorphism): {playerData.correctAttemptAmount_EP}");

        // Add medium-level data
        sb.AppendLine("\nMedium Level Data:");
        sb.AppendLine($"Correct Attempt Amount (Encapsulation): {playerData.correctAttemptAmount_ME}");
        sb.AppendLine($"Correct Attempt Amount (Abstraction): {playerData.correctAttemptAmount_MA}");
        sb.AppendLine($"Correct Attempt Amount (Inheritance): {playerData.correctAttemptAmount_MI}");
        sb.AppendLine($"Correct Attempt Amount (Polymorphism): {playerData.correctAttemptAmount_MP}");

        // Add hard-level data
        sb.AppendLine("\nHard Level Data:");
        sb.AppendLine($"Correct Attempt Amount (Encapsulation): {playerData.correctAttemptAmount_HE}");
        sb.AppendLine($"Correct Attempt Amount (Abstraction): {playerData.correctAttemptAmount_HA}");
        sb.AppendLine($"Correct Attempt Amount (Inheritance): {playerData.correctAttemptAmount_HI}");
        sb.AppendLine($"Correct Attempt Amount (Polymorphism): {playerData.correctAttemptAmount_HP}");

        // Set the Text component to display the formatted data
        displayText.text = sb.ToString();
    }



    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
}



//Other tricks i have tried. -Teemu H

//     private void ShowPlayerData()
//     {
//         var playerData = playerDataHandler.currentPlayerData;

//         var sb = new StringBuilder();

//         // Display basic information
//         sb.AppendLine("Player Data:");
//         sb.AppendLine($"Email: {playerData.email}");
//         sb.AppendLine($"Username: {playerData.userName}");

//         // Add details for each field in PlayerData
//         AppendObjectDetails(sb, playerData);

//         // Set the text with the accumulated data
//         displayText.text = sb.ToString();
//     }

//     private void AppendObjectDetails(StringBuilder sb, object obj)
//     {
//         // Use reflection to iterate through fields and properties
//         var type = obj.GetType();

//         sb.AppendLine($"\n{type.Name} Details:");

//         // Display public properties
//         foreach (PropertyInfo prop in type.GetProperties())
//         {
//             var value = prop.GetValue(obj);
//             sb.AppendLine($"{prop.Name}: {FormatValue(value)}");
//         }

//         // Display public fields
//         foreach (FieldInfo field in type.GetFields())
//         {
//             var value = field.GetValue(obj);
//             sb.AppendLine($"{field.Name}: {FormatValue(value)}");
//         }
//     }

//     private string FormatValue(object value)
//     {
//         if (value == null)
//         {
//             return "null";
//         }
//         else if (value is IEnumerable<object> collection)
//         {
//             return $"[{string.Join(", ", collection)}]";
//         }
//         else
//         {
//             return value.ToString();
//         }
//     }
// }


////////

//     private void ShowPlayerData()
//     {
//         var playerData = playerDataHandler.currentPlayerData;

//         var sb = new StringBuilder();
//         sb.AppendLine("Player Data:");
//         sb.AppendLine($"Email: {playerData.email}");
//         sb.AppendLine($"Username: {playerData.userName}");

//         // Include the count of each task list for context
//         sb.AppendLine("\nEasy Level Data:");
//         sb.AppendLine($"Correct Attempt Amount (Encapsulation): {playerData.correctAttemptAmount_EE}");
//         sb.AppendLine($"Tasks (Encapsulation): {playerData.task_EE_data.Count}");
//         AppendTasksData(sb, "Easy Encapsulation", playerData.task_EE_data);

//         sb.AppendLine($"Correct Attempt Amount (Abstraction): {playerData.correctAttemptAmount_EA}");
//         sb.AppendLine($"Tasks (Abstraction): {playerData.task_EA_data.Count}");
//         AppendTasksData(sb, "Easy Abstraction", playerData.task_EA_data);

//         sb.AppendLine($"Correct Attempt Amount (Inheritance): {playerData.correctAttemptAmount_EI}");
//         sb.AppendLine($"Tasks (Inheritance): {playerData.task_EI_data.Count}");
//         AppendTasksData(sb, "Easy Inheritance", playerData.task_EI_data);

//         sb.AppendLine($"Correct Attempt Amount (Polymorphism): {playerData.correctAttemptAmount_EP}");
//         sb.AppendLine($"Tasks (Polymorphism): {playerData.task_EP_data.Count}");
//         AppendTasksData(sb, "Easy Polymorphism", playerData.task_EP_data);

//         // Set the Text component to display the formatted data
//         displayText.text = sb.ToString();
//     }

//     private void AppendTasksData(StringBuilder sb, string sectionName, List<Easy_Task_Data> taskData)
//     {
//         sb.AppendLine($"\n{sectionName} Tasks:");
//         foreach (var task in taskData)
//         {
//             sb.AppendLine($"- {task.TaskName}: {task.Description}"); // Adjust according to the properties in Easy_Task_Data
//         }
//     }

//     public void CloseWindow(GameObject window)
//     {
//         window.SetActive(false);
//     }
// }

