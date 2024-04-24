using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSetup_MP : QuestionSetup_MI
{
    public override void GetQuestionAssets()
    {
        // Get all of the questions from the questions folder
        questions = new List<QuestionData_MI>(Resources.LoadAll<QuestionData_MI>("Questions_MP"));
    }
}
