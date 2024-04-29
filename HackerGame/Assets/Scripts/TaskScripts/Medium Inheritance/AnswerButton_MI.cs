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

            //Teemu K additions below
            //For data sheet, check the question, what aswer was selected, was it correct and set that to data sheet
            questionSetup.data.questionData[questionSetup.questions.Count].question = questionSetup.currentQuestion.question;
            questionSetup.data.questionData[questionSetup.questions.Count].whatWasSelected = answerText.text;
            questionSetup.data.questionData[questionSetup.questions.Count].wasCorrect = true;
        }
        else
        {
            Debug.Log("WRONG ANSWER");

            //Teemu K additions below
            //For data sheet, check the question, what aswer was selected, was it correct and set that to data sheet
            questionSetup.data.questionData[questionSetup.questions.Count].question = questionSetup.currentQuestion.question;
            questionSetup.data.questionData[questionSetup.questions.Count].whatWasSelected = answerText.text;
            questionSetup.data.questionData[questionSetup.questions.Count].wasCorrect = false;
        }

        // Get the next question if there are more in the list
        if (questionSetup.questions.Count > 0)
        {
            // Generate a new question
            questionSetup.Start(); //Question for Waltuh. Why not make a seperate function for this part? Using start seems a bit silly imo. - Teemu K
        }
        else
        {
            //Debug.Log("all questions have been answered, disable the task");
            //Debug.Log("Correct Answers: " + questionSetup.correctAnswersCount + "/5");
            Debug.Log("Correct Answers: " + questionSetup.data.correctAmount + "/5");
            //task.SetActive(false);

            //Teemu K additions below
            switch (questionSetup.data.correctAmount) {
                case 5:
                //All correct, update as correct attempt and destroy task object
                questionSetup.StartCoroutine(questionSetup.TerminalMessage(questionSetup.correctMessage, true));
                break;

                default:
                //Default aka else, update as incorrect attempt and restart task
                questionSetup.StartCoroutine(questionSetup.TerminalMessage(questionSetup.wrongMessage, false));
                break;
            }
        }
    }
}
