using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSetup_MP : QuestionSetup_MI
{
    // Overrides the location the questions are fetch
    public override void GetQuestionAssets()
    {
        // Get all of the questions from the questions folder
        questions = new List<QuestionData_MI>(Resources.LoadAll<QuestionData_MI>("Questions_MP"));
    }

    public override void GetTaskAttemptData() {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        if (handler.currentPlayerData.task_MP_data.Count == 0) data.attempt = 1;
        else data.attempt = handler.currentPlayerData.task_MP_data.Count + 1;
    }

    public override void UpdateTaskData(bool correctAttempt) {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        Medium_Task_Data currentData = data.DataCopy();
        switch (correctAttempt) {
            case true:
            handler.currentPlayerData.task_MP_data.Add(currentData);
            handler.currentPlayerData.correctAttemptAmount_MP++;
            gameManager.EnableCheckMark(7);
            gameManager.UnlockFolder(7);
            break;

            case false:
            handler.currentPlayerData.task_MP_data.Add(currentData);
            break;
        }
    }
}
