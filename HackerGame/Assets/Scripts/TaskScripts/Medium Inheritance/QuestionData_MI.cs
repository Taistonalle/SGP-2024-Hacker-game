using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Question", order = 1)]
public class QuestionData_MI : ScriptableObject
{
    // The question text
    public string question;

    // Array to hold the answer choices
    [Tooltip("The correct anwser should always be listed first, they are randomized later")]
    public string[] answers;
}
