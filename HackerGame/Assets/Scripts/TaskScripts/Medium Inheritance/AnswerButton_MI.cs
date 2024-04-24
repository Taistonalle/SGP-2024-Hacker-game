using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton_MI : MonoBehaviour
{
    private bool isCorrect;
    [SerializeField] private TextMeshProUGUI answerText;

    // To make it ask a new question after the first question
    [SerializeField] private QuestionSetup_MI questionSetup;

    // This is the game object that is holding all the objects in the task, example "Proto_MI"
    [SerializeField] private GameObject task;

    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            Debug.Log("CORRECT ANSWER");
            questionSetup.IncrementCorrectAnswersCount();
        }
        else
        {
            Debug.Log("WRONG ANSWER");
        }

        // Get the next question if there are more in the list
        if (questionSetup.questions.Count > 0)
        {
            // Generate a new question
            questionSetup.Start();
        }
        else
        {
            Debug.Log("all questions have been answered, disable the task");
            Debug.Log("Correct Answers: " + questionSetup.correctAnswersCount + "/5");
            task.SetActive(false);
        }
    }
}
