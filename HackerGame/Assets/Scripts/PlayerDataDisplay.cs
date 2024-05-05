using System.Text; // For StringBuilder
using UnityEngine;
using UnityEngine.UI; // Use Text for Unity UI
using TMPro;
using System.Collections.Generic;

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
        sb.AppendLine();
        sb.AppendLine($"Email: {playerData.email}");
        sb.AppendLine($"Username: {playerData.userName}");


        // Add task_EE_data
        sb.AppendLine("\nTask EE Data:");
        DisplayTaskData(sb, playerData.task_EE_data);

        // Add task_EA_data
        sb.AppendLine("\nTask EA Data:");
        DisplayTaskData(sb, playerData.task_EA_data);

        // Add task_EI_data
        sb.AppendLine("\nTask EI Data:");
        DisplayTaskData(sb, playerData.task_EI_data);

        // Add task_EP_data
        sb.AppendLine("\nTask EP Data:");
        DisplayTaskData(sb, playerData.task_EP_data);

        // Set the Text component to display the formatted data
        displayText.text = sb.ToString();
    }

    private void DisplayTaskData(StringBuilder sb, List<Easy_Task_Data> taskDataList)
    {
        for (int i = 0; i < taskDataList.Count; i++)
        {
            string attemptData = $"Attempt: {taskDataList[i].attempt}" +
                                $"\nCorrect amount: {taskDataList[i].correctAmount}";

            // In the current task data, loop through its slot data array
            foreach (Slot_Data slot in taskDataList[i].slot_datas)
            {
                attemptData += $"\nSlot name: {slot.slotName}" +
                            $"\nDescription: {slot.description}" +
                            $"\nWas correct: {slot.wasCorrect}";
            }
            sb.AppendLine(attemptData);
            sb.AppendLine(); // Empty line after each attempt
        }
    }

    // private void ShowPlayerData()
    // {
    //     // Get current player data
    //     PlayerDataHandler.PlayerData playerData = playerDataHandler.currentPlayerData;

    //     // Use a StringBuilder for efficient string manipulation
    //     StringBuilder sb = new StringBuilder();

    //     // Add basic information
    //     sb.AppendLine("Player Data:");
    //     sb.AppendLine($"Email: {playerData.email}");
    //     sb.AppendLine($"Username: {playerData.userName}");

    //     // Add easy-level data
    //     sb.AppendLine("\nEasy Level Data:");
    //     sb.AppendLine($"Correct Attempt Amount (Encapsulation): {playerData.correctAttemptAmount_EE}");

    //     // Add task_EE_data
    //     sb.AppendLine("\nTask EE Data:");
    //     /* Old loop
    //     foreach (var taskData in playerData.task_EE_data)
    //     {
    //         sb.AppendLine(taskData.ToString()); // This assumes that you have a suitable ToString method in your Easy_Task_Data class
    //     }
    //     */
    //     //Loop through all the task_EE_datas
    //     for (int i = 0; i < playerData.task_EE_data.Count; i++) {
    //         string attemptData = $"Attempt: {playerData.task_EE_data[i].attempt}" +
    //                              $"\nCorrect amount: {playerData.task_EE_data[i].correctAmount}";
            
    //         //In the current task EE data, loop through it's slot data array
    //         foreach (Slot_Data slot in playerData.task_EE_data[i].slot_datas) {
    //             attemptData += $"\nSlot name: {slot.slotName}" +
    //                            $"\nDescription: {slot.description}" +
    //                            $"\nWas correct: {slot.wasCorrect}";
    //         }
    //         sb.AppendLine(attemptData);
    //         sb.AppendLine(); //Empty line after each attempt
    //     }





    //     // Set the Text component to display the formatted data
    //     displayText.text = sb.ToString();
    // }



    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
}


    //     [Header("Easy Encapsulation data")]
    //     public List<Easy_Task_Data> task_EE_data = new();
    //     public int correctAttemptAmount_EE;

    //     [Header("Easy Abstraction data")]
    //     public List<Easy_Task_Data> task_EA_data = new();
    //     public int correctAttemptAmount_EA;

    //     [Header("Easy Inheritace data")]
    //     public List<Easy_Task_Data> task_EI_data = new();
    //     public int correctAttemptAmount_EI;

    //     [Header("Easy Polymorphism data")]
    //     public List<Easy_Task_Data> task_EP_data = new();
    //     public int correctAttemptAmount_EP;

    //     [Header("Medium Encapsulation data")]
    //     public List<Medium_Task_Data> task_ME_data = new();
    //     public int correctAttemptAmount_ME;

    //     [Header("Medium Abstraction data")]
    //     public List<Medium_Task_Data> task_MA_data = new();
    //     public int correctAttemptAmount_MA;

    //     [Header("Medium Inheritance data")]
    //     public List<Medium_Task_Data> task_MI_data = new();
    //     public int correctAttemptAmount_MI;
       
    //     [Header("Medium Polymorphism data")]
    //     public List<Medium_Task_Data> task_MP_data = new();
    //     public int correctAttemptAmount_MP;

    //     [Header("Hard Encapsulation data")]
    //     public List<Hard_Task_Data> task_HE_data = new();
    //     public int correctAttemptAmount_HE;

    //     [Header("Hard Abstraction data")]
    //     public List<Hard_Task_Data> task_HA_data = new();
    //     public int correctAttemptAmount_HA;
       
    //     [Header("Hard Inheritance data")]
    //     public List<Hard_Task_Data> task_HI_data = new();
    //     public int correctAttemptAmount_HI;

    //     [Header("Hard Polymorphism data")]
    //     public List<Hard_Task_Data> task_HP_data = new();
    //     public int correctAttemptAmount_HP;
    // }