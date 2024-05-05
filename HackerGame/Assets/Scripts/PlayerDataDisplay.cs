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

        // Add task_EE_data
        sb.AppendLine("\nTask EE Data:");
        /* Old loop
        foreach (var taskData in playerData.task_EE_data)
        {
            sb.AppendLine(taskData.ToString()); // This assumes that you have a suitable ToString method in your Easy_Task_Data class
        }
        */
        //Loop through all the task_EE_datas
        for (int i = 0; i < playerData.task_EE_data.Count; i++) {
            string attemptData = $"Attempt: {playerData.task_EE_data[i].attempt}" +
                                 $"\nCorrect amount: {playerData.task_EE_data[i].correctAmount}";
            
            //In the current task EE data, loop through it's slot data array
            foreach (Slot_Data slot in playerData.task_EE_data[i].slot_datas) {
                attemptData += $"\nSlot name: {slot.slotName}" +
                               $"\nDescription: {slot.description}" +
                               $"\nWas correct: {slot.wasCorrect}";
            }
            sb.AppendLine(attemptData);
            sb.AppendLine(); //Empty line after each attempt
        }
        //sb.AppendLine(attemptData);

        // Add easy-level data

        // sb.AppendLine($"Correct Attempt Amount (Encapsulation): {playerData.correctAttemptAmount_EE}");
        // sb.AppendLine($"Correct Attempt Amount (Abstraction): {playerData.correctAttemptAmount_EA}");
        // sb.AppendLine($"Correct Attempt Amount (Inheritance): {playerData.correctAttemptAmount_EI}");
        // sb.AppendLine($"Correct Attempt Amount (Polymorphism): {playerData.correctAttemptAmount_EP}");

        // Add medium-level data
        // sb.AppendLine("\nMedium Level Data:");
        // sb.AppendLine($"Correct Attempt Amount (Encapsulation): {playerData.correctAttemptAmount_ME}");
        // sb.AppendLine($"Correct Attempt Amount (Abstraction): {playerData.correctAttemptAmount_MA}");
        // sb.AppendLine($"Correct Attempt Amount (Inheritance): {playerData.correctAttemptAmount_MI}");
        // sb.AppendLine($"Correct Attempt Amount (Polymorphism): {playerData.correctAttemptAmount_MP}");

        // Add hard-level data
        // sb.AppendLine("\nHard Level Data:");
        // sb.AppendLine($"Correct Attempt Amount (Encapsulation): {playerData.correctAttemptAmount_HE}");
        // sb.AppendLine($"Correct Attempt Amount (Abstraction): {playerData.correctAttemptAmount_HA}");
        // sb.AppendLine($"Correct Attempt Amount (Inheritance): {playerData.correctAttemptAmount_HI}");
        // sb.AppendLine($"Correct Attempt Amount (Polymorphism): {playerData.correctAttemptAmount_HP}");

        // Set the Text component to display the formatted data
        displayText.text = sb.ToString();
    }



    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
}