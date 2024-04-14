using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks.Sources;

public class AnwserButton_MI : MonoBehaviour
{

    private bool isCorrect;
    [SerializeField] private TextMeshProUGUI anwserText;

    public QuestionSetup_MI questionSetup_MI;

    private bool isWaitingForReset = false;
    private float resetTimer = 5f;

    private void Update()
    {
        if (isWaitingForReset)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
            {
                ResetTask();

                isWaitingForReset = false;
            }
        }
    }

    public void SetAnwserText(string newText)
    {
        anwserText.text = newText;
    }

    public void SetIsCorrect(bool newBool) 
    { 
        isCorrect = newBool;
    }

    private void ResetTask()
    {
        resetTimer = 5f;
        isWaitingForReset = true;
        questionSetup_MI.Start();
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            questionSetup_MI.SelectNewQuestion();
            questionSetup_MI.SetQuestionValues();
            questionSetup_MI.SetAnwserValues();
        }
        else
        {
            Debug.Log("Wrong Answer!");
        }
        
        if (questionSetup_MI.questions.Count > 0)
        {
            // Generate a new question
            questionSetup_MI.Start();
        }
    }
}
