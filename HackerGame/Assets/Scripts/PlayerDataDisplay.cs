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
    string mainTitle, userNameAndEmail;
    string EETitle, EEData, EATitle, EAData, EITitle, EIData, EPTitle, EPData;
    string METitle, MEData, MATitle, MAData, MITitle, MIData, MPTitle, MPData;
    string HETitle, HEData, HATitle, HAData, HITitle, HIData, HPTitle, HPData;

    int task_EE_Attempts, task_EA_Attempts, task_EI_Attempts, task_EP_Attempts;
    int task_ME_Attempts, task_MA_Attempts, task_MI_Attempts, task_MP_Attempts;
    int task_HE_Attempts, task_HA_Attempts, task_HI_Attempts, task_HP_Attempts;
    int allAttempts;

    private void Start()
    {
        

        if (playerDataHandler == null || playerDataHandler.currentPlayerData == null || displayText == null)
        {
            Debug.LogError("PlayerDataHandler or displayText is not set. Please set them in the Inspector.");
            return;
        }

        playerDataHandler = FindObjectOfType<PlayerDataHandler>();

        //Define strings
        mainTitle = "Player data";
        userNameAndEmail = $"\nEmail: {playerDataHandler.currentPlayerData.email}\nUsername: {playerDataHandler.currentPlayerData.userName}";
        EETitle = "\nTask EE Data:";
        EATitle = "\nTask EA Data:";
        EITitle = "\nTask EI Data:";
        EPTitle = "\nTask EP Data:";
        METitle = "\nTask ME Data:";
        MATitle = "\nTask MA Data:";
        MITitle = "\nTask MI Data:";
        MPTitle = "\nTask MP Data:";
        HETitle = "\nTask HE Data:";
        HATitle = "\nTask HA Data:";
        HITitle = "\nTask HI Data:";
        HPTitle = "\nTask HP Data:";

        //Define attempts
        task_EE_Attempts = playerDataHandler.currentPlayerData.task_EE_data.Count;
        task_EA_Attempts = playerDataHandler.currentPlayerData.task_EA_data.Count;
        task_EI_Attempts = playerDataHandler.currentPlayerData.task_EI_data.Count;
        task_EP_Attempts = playerDataHandler.currentPlayerData.task_EP_data.Count;

        task_ME_Attempts = playerDataHandler.currentPlayerData.task_ME_data.Count;
        task_MA_Attempts = playerDataHandler.currentPlayerData.task_MA_data.Count;
        task_MI_Attempts = playerDataHandler.currentPlayerData.task_MI_data.Count;
        task_MP_Attempts = playerDataHandler.currentPlayerData.task_MP_data.Count;

        task_HE_Attempts = playerDataHandler.currentPlayerData.task_HE_data.Count;
        task_HA_Attempts = playerDataHandler.currentPlayerData.task_HA_data.Count;
        task_HI_Attempts = playerDataHandler.currentPlayerData.task_HI_data.Count;
        task_HP_Attempts = playerDataHandler.currentPlayerData.task_HP_data.Count;

        allAttempts = task_EE_Attempts + task_EA_Attempts + task_EI_Attempts + task_EP_Attempts +
                          task_ME_Attempts + task_MA_Attempts + task_MI_Attempts + task_MP_Attempts +
                          task_HE_Attempts + task_HA_Attempts + task_HI_Attempts + task_HP_Attempts;

        ShowPlayerData();
    }



    private void ShowPlayerData()
    {
        // Get current player data
        PlayerDataHandler.PlayerData playerData = playerDataHandler.currentPlayerData;

        // Use a StringBuilder for efficient string manipulation
        //StringBuilder sb = new StringBuilder();

        //Add player information
        sb.AppendLine(mainTitle);
        sb.AppendLine(userNameAndEmail);
        sb.AppendLine($"Task EE attempts: {task_EE_Attempts}");
        sb.AppendLine($"Task EA attempts: {task_EA_Attempts}");
        sb.AppendLine($"Task EI attempts: {task_EI_Attempts}");
        sb.AppendLine($"Task EP attempts: {task_EP_Attempts}");
        sb.AppendLine($"Task ME attempts: {task_ME_Attempts}");
        sb.AppendLine($"Task MA attempts: {task_MA_Attempts}");
        sb.AppendLine($"Task MI attempts: {task_MI_Attempts}");
        sb.AppendLine($"Task MP attempts: {task_MP_Attempts}");
        sb.AppendLine($"Task HE attempts: {task_HE_Attempts}");
        sb.AppendLine($"Task HA attempts: {task_HA_Attempts}");
        sb.AppendLine($"Task HI attempts: {task_HI_Attempts}");
        sb.AppendLine($"Task HP attempts: {task_HP_Attempts}");
        sb.AppendLine($"All attempts: {allAttempts}");

        //Add Task EE data
        sb.AppendLine(EETitle);
        DisplayTaskData(sb, playerData.task_EE_data, 1);

        //Add Task EA data
        sb.AppendLine(EATitle);
        DisplayTaskData(sb, playerData.task_EA_data, 2);

        //Add Task EI data
        sb.AppendLine(EITitle);
        DisplayTaskData(sb, playerData.task_EI_data, 3);

        //Add Task EP data
        sb.AppendLine(EPTitle);
        DisplayTaskData(sb, playerData.task_EP_data, 4);

        //Add Task ME data
        sb.AppendLine(METitle);
        DisplayTaskData(sb, playerData.task_ME_data, 1);

        //Add Task MA data
        sb.AppendLine(MATitle);
        DisplayTaskData(sb, playerData.task_MA_data, 2);

        //Add Task MI data
        sb.AppendLine(MITitle);
        DisplayTaskData(sb, playerData.task_MI_data, 3);

        //Add Task MP data
        sb.AppendLine(MPTitle);
        DisplayTaskData(sb, playerData.task_MP_data, 4);

        /* Old data information
        // Add basic information
        sb.AppendLine("Player Data:");
        sb.AppendLine();
        sb.AppendLine($"Email: {playerData.email}");
        sb.AppendLine($"Username: {playerData.userName}");
        */

        /* Old data adding
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

        // Add task_ME_data
        sb.AppendLine("\nTask ME Data:");
        DisplayTaskData(sb, playerData.task_ME_data);

        // Add task_MA_data
        sb.AppendLine("\nTask MA Data:");
        DisplayTaskData(sb, playerData.task_MA_data);

        // Add task_MI_data
        sb.AppendLine("\nTask MI Data:");
        DisplayTaskData(sb, playerData.task_MI_data);

        // Add task_MP_data
        sb.AppendLine("\nTask MP Data:");
        DisplayTaskData(sb, playerData.task_MP_data);
        */
        // Set the Text component to display the formatted data
        displayText.text = sb.ToString();
    }

    private void DisplayTaskData(StringBuilder sb, List<Easy_Task_Data> taskDataList, int dataNum)
    {
        for (int i = 0; i < taskDataList.Count; i++)
        {
            string attemptData = $"\nAttempt: {taskDataList[i].attempt}" +
                          $"\nCorrect amount: {taskDataList[i].correctAmount}";

            // In the current task data, loop through its slot data array
            foreach (Slot_Data slot in taskDataList[i].slot_datas)
            {
                attemptData += $"\nSlot name: {slot.slotName}" +
                               $"\nDescription: {slot.description}" +
                               $"\nWas correct: {slot.wasCorrect}\n";
            }
            sb.AppendLine(attemptData);
            //sb.AppendLine(); // Empty line after each attempt

            switch (dataNum) {
                case 1:
                EEData += attemptData;
                break;

                case 2:
                EAData += attemptData;
                break;

                case 3:
                EIData += attemptData;
                break;

                case 4:
                EPData += attemptData;
                break;
            }
        }
    }

    private void DisplayTaskData(StringBuilder sb, List<Medium_Task_Data> taskDataList, int dataNum) {
        for (int i = 0; i < taskDataList.Count;i++) {
            string attemptData = $"\nAttempt: {taskDataList[i].attempt}" +
                                 $"\nCorrect amount: {taskDataList[i].correctAmount}";

            foreach (QuestionSelectionData question in taskDataList[i].questionData) {
                attemptData += $"\nQuestion: {question.question}" +
                               $"\nWhat was selected: {question.whatWasSelected}" +
                               $"\nWas correct: {question.wasCorrect}\n";
            }
            sb.AppendLine(attemptData);

            switch (dataNum) {
                case 1:
                MEData += attemptData;
                break;

                case 2:
                MAData += attemptData;
                break;

                case 3:
                MIData += attemptData;
                break;

                case 4:
                MPData += attemptData;
                break;
            }
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
        //int width = Screen.width;
        //int height = Screen.height;
        //Texture2D tex = new(width, height, TextureFormat.RGB24, false);

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
        //tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //tex.Apply();
        
        //Encode the texture in PNG format
        //byte[] bytes = tex.EncodeToPNG();
        //Destroy(tex);

        //Write the returned byte array to a file in desktop
        //File.WriteAllBytes(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Hack the Hacker report - {playerDataHandler.currentPlayerData.userName} {DateTime.Now}.png"), bytes); //Screenshot

        //Text file part
        //File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Hack the Hacker report - {playerDataHandler.currentPlayerData.userName} {DateTime.Now}.txt"), sb.ToString()); //Works

        //----Pdf----
        Document document = new();
        string filePath = "";
        //Check if platform is Windows or Mac
        switch (Application.platform) {
            //Mac
            case RuntimePlatform.OSXPlayer:
            filePath = Path.Combine(Application.persistentDataPath, $"Hack the Hacker report - {playerDataHandler.currentPlayerData.userName} {DateTime.Now}.pdf");
            break;
            case RuntimePlatform.OSXEditor:
            filePath = Path.Combine(Application.persistentDataPath, $"Hack the Hacker report - {playerDataHandler.currentPlayerData.userName} {DateTime.Now}.pdf");
            break;

            //Windows
            case RuntimePlatform.WindowsPlayer:
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Hack the Hacker report - {playerDataHandler.currentPlayerData.userName} {DateTime.Now}.pdf");
            break;
            case RuntimePlatform.WindowsEditor:
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Hack the Hacker report - {playerDataHandler.currentPlayerData.userName} {DateTime.Now}.pdf");
            break;
        }

        // Create a PdfWriter instance to write the document to the specified file
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

        // Open the document for writing
        document.Open();

        // Add content to the document
        //document.Add(new Paragraph(sb.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 18))); Old, using the whole string builder. Cant modify headers this way
        document.Add(new Paragraph(mainTitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, BaseColor.BLACK)));
        document.Add(new Paragraph(userNameAndEmail, FontFactory.GetFont(FontFactory.HELVETICA, 14)));
        document.Add(new Paragraph($"Task EE attempts: {task_EE_Attempts}" +
                                   $"\nTask EA attempts: {task_EA_Attempts}" +
                                   $"\nTask EI attempts: {task_EI_Attempts}" +
                                   $"\nTask EP attempts: {task_EP_Attempts}" +
                                   $"\nTask ME attempts: {task_ME_Attempts}" +
                                   $"\nTask MA attempts: {task_MA_Attempts}" +
                                   $"\nTask MI attempts: {task_MI_Attempts}" +
                                   $"\nTask MP attempts: {task_MP_Attempts}" +
                                   $"\nTask HE attempts: {task_HE_Attempts}" +
                                   $"\nTask HA attempts: {task_HA_Attempts}" +
                                   $"\nTask HI attempts: {task_HI_Attempts}" +
                                   $"\nTask HP attempts: {task_HP_Attempts}" +
                                   $"\nAll attempts: {allAttempts}", FontFactory.GetFont(FontFactory.HELVETICA, 14)));

        document.Add(new Paragraph(EETitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, new BaseColor(30, 115, 45))));
        document.Add(new Paragraph(EEData, FontFactory.GetFont(FontFactory.HELVETICA, 14)));

        document.Add(new Paragraph(EATitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, new BaseColor(30, 115, 45))));
        document.Add(new Paragraph(EAData, FontFactory.GetFont(FontFactory.HELVETICA, 14)));

        document.Add(new Paragraph(EITitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, new BaseColor(30, 115, 45))));
        document.Add(new Paragraph(EIData, FontFactory.GetFont(FontFactory.HELVETICA, 14)));

        document.Add(new Paragraph(EPTitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, new BaseColor(30, 115, 45))));
        document.Add(new Paragraph(EPData, FontFactory.GetFont(FontFactory.HELVETICA, 14)));

        document.Add(new Paragraph(METitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, new BaseColor(110, 110, 50))));
        document.Add(new Paragraph(MEData, FontFactory.GetFont(FontFactory.HELVETICA, 14)));

        document.Add(new Paragraph(MATitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, new BaseColor(110, 110, 50))));
        document.Add(new Paragraph(MAData, FontFactory.GetFont(FontFactory.HELVETICA, 14)));

        document.Add(new Paragraph(MITitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, new BaseColor(110, 110, 50))));
        document.Add(new Paragraph(MIData, FontFactory.GetFont(FontFactory.HELVETICA, 14)));

        document.Add(new Paragraph(MPTitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, new BaseColor(110, 110, 50))));
        document.Add(new Paragraph(MPData, FontFactory.GetFont(FontFactory.HELVETICA, 14)));

        /* Doesn't work.. yet at least.
        // Set background color for the whole document
        Rectangle pageSize;
        PdfContentByte canvas;
        int totalPages = document.PageNumber;
        List<Rectangle> pages = new List<Rectangle>();
        for (int i = 0; i <= document.PageNumber; i++) {
            pageSize = document.PageSize;
            canvas = writer.DirectContentUnder;
            canvas.SetColorFill(new BaseColor(30, 30, 30));
            canvas.Rectangle(pageSize.Left, pageSize.Bottom, pageSize.Width, pageSize.Height);
            document.NewPage(); // Move to the next page
            canvas.Fill();
        }
        */
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