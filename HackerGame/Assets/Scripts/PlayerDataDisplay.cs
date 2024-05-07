using System.Text; // For StringBuilder
using UnityEngine;
using UnityEngine.UI; // Use Text for Unity UI
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class PlayerDataDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI displayText;
    public PlayerDataHandler playerDataHandler; // Reference to PlayerDataHandler
    //[SerializeField] Image notepad; //Teemu K addition
    readonly StringBuilder sb = new(); //Moved this here - Teemu K


    private void Start()
    {
        if (playerDataHandler == null || playerDataHandler.currentPlayerData == null || displayText == null)
        {
            Debug.LogError("PlayerDataHandler or displayText is not set. Please set them in the Inspector.");
            return;
        }

        playerDataHandler = FindObjectOfType<PlayerDataHandler>();
        ShowPlayerData();
    }



    private void ShowPlayerData()
    {
        // Get current player data
        PlayerDataHandler.PlayerData playerData = playerDataHandler.currentPlayerData;

        // Use a StringBuilder for efficient string manipulation
        //StringBuilder sb = new StringBuilder();

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

    public void DestroyWindow(GameObject window) {
        Destroy(window);
    }

    public void StartLoadReport() {
        StartCoroutine(LoadReport());
    }

    IEnumerator LoadReport() {
        //Read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();

        //Create a texture in RGB format the size of the screen
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new(width, height, TextureFormat.RGB24, false);

        /*
        //Custom testing with picture size, works
        float width = notepad.GetComponent<RectTransform>().rect.xMax;
        float height = notepad.GetComponent<RectTransform>().rect.yMax;
        Texture2D tex = new((int)width, (int)height, TextureFormat.RGB24, false);
        */

        //Read the screen contents into the texture
        /*
        //Custom testing with where the shot is taken from screen, did not work
        var noteRect = notepad.GetComponent<RectTransform>().rect;
        tex.ReadPixels(new Rect(100, 20, width, height), 0, 0);
        */
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        
        //Encode the texture in PNG format
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        //Write the returned byte array to a file in desktop
        //File.WriteAllBytes(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Hack the Hacker report - {playerDataHandler.currentPlayerData.userName} {DateTime.Now}.png"), bytes); //Screenshot

        //Text file part
        //File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Hack the Hacker report - {playerDataHandler.currentPlayerData.userName} {DateTime.Now}.txt"), sb.ToString()); //Works

        //----Pdf----
        Document document = new();

        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Hack the Hacker report - {playerDataHandler.currentPlayerData.userName} {DateTime.Now}.pdf");

        // Create a PdfWriter instance to write the document to the specified file
        PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

        // Open the document for writing
        document.Open();

        // Add content to the document
        document.Add(new Paragraph(sb.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 18)));

        // Close the document
        document.Close();
        //----End of pdf---
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