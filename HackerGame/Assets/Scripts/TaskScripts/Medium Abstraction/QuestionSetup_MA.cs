using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSetup_MA : QuestionSetup_MI
{
    // Overrides the location the questions are fetch
    public override void GetQuestionAssets()
    {
        // Get all of the questions from the questions folder
        questions = new List<QuestionData_MI>(Resources.LoadAll<QuestionData_MI>("Questions_MA"));
    }
}
