using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Callbacks;

public class QuestionSetup_MI : MonoBehaviour
{

    [SerializeField] public List<QuestionData_MI> questions;
    private QuestionData_MI currentQuestion;

    [SerializeField] private TextMeshProUGUI quesionText;
    [SerializeField] private AnwserButton_MI[] answerButtons;

    [SerializeField] private int correctAnwserChoice;

    private void Awake()
    {
        // Get all the questions ready
        GetQuestionAssets();
    }

    public void Start()
    {
        //Get new question
        SelectNewQuestion();

        // Set all text and values on screen
        SetQuestionValues();

        // Set all of the anwser buttons text and correct anwser values
        SetAnwserValues();
    }

    private void GetQuestionAssets()
    {
        //Get all of the questions from the question folder
        questions = new List<QuestionData_MI>(Resources.LoadAll<QuestionData_MI>("Questions_MI"));
    }

    public void SelectNewQuestion()
    {
        if (questions.Count == 0)
        {
            Debug.LogWarning("No more questions available.");
            return;
        }

        // Shuffle the list of questions
        ShuffleQuestions();

        // Set the question to the first index (which is now a random question)
        currentQuestion = questions[0];

        // Remove this question from the list so it will not be repeared (until game is restarted)
        questions.RemoveAt(0);
    }
    private void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            // Swap the current question with a random question in the list
            int randomIndex = Random.Range(i, questions.Count);
            QuestionData_MI temp = questions[i];
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
    }

    public void SetQuestionValues()
    {
        // Set the question text
        quesionText.text = currentQuestion.question;

    }

    public void SetAnwserValues()
    {
        // Randomize the anwser button order
        List<string> anwsers = RandomizeAnswers(new List<string>(currentQuestion.answers));

        // Set up the anwser buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Create a temporary boolean to pass to the buttons
            bool isCorrect = false;

            // If it is correct anwser, set the bool to true
            if (i == correctAnwserChoice)
            {
                isCorrect = true;
            }

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnwserText(anwsers[i]);
        }
    }

    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChosen = false;

        List<string> newList = new List<string>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Get a random number of the remaining choices
            int random = Random.Range(0, originalList.Count);

            // If thr random number is 0, this is the correct answer, MAKE SURE THIS IS ONLY USED ONCE
            if (random == 0 && !correctAnswerChosen)
            {
                correctAnwserChoice = i;
                correctAnswerChosen = true;
            }

            // Add this to the new list
            newList.Add(originalList[random]);

            // Remove this choice from the original list (it has been used)
            originalList.RemoveAt(random);
        }

        return newList;
    }

}
