using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct Medium_Task_Data {
    public int attempt;
    public int correctAmount;
    public QuestionSelectionData[] questionData;

    //Deep copy
    public Medium_Task_Data DataCopy() {
        Medium_Task_Data copy = new Medium_Task_Data();
        copy.attempt = attempt;
        copy.correctAmount = correctAmount;
        copy.questionData = new QuestionSelectionData[questionData.Length];
        System.Array.Copy(questionData, copy.questionData, questionData.Length);
        return copy;
    }
}
[System.Serializable]
public struct QuestionSelectionData {
    public string question;
    public string whatWasSelected;
    public bool wasCorrect;
}

public class QuestionSetup_MI : MonoBehaviour
{
    //----Teemu K additions----
    private int persistentQuestionCount;
    protected GameManager gameManager;
    [Header("Data for handler")]
    public Medium_Task_Data data;

    [Space(10f)]
    [Header("Terminal messages")]
    [SerializeField] float messageTimeOnTerminal;
    [TextArea(2,4)]
    public string correctMessage;
    [TextArea(2,4)]
    public string wrongMessage;
    [SerializeField] TextMeshProUGUI terminalQuestionHeader;
    [SerializeField] TextMeshProUGUI terminalQuestionTxt;
    [SerializeField] GameObject[] objectsToHide; //Hide these while terminal message is active

    [Header("Original stuff below")]
    //----End of additions----

    [SerializeField]
    public List<QuestionData_MI> questions;
    public QuestionData_MI currentQuestion; //Changed this to public - Teemu K

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private AnswerButton_MI[] answerButtons;

    [SerializeField]
    private int correctAnswerChoice;

    //public int correctAnswersCount = 0; // Variable to store the count of correct answers ----Commented this to use struct int instead----

    private void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();

        // Get all the questions ready
        GetQuestionAssets();
        data.questionData = new QuestionSelectionData[questions.Count]; //Make sure the questionData list is long enough inside the data sheet
        persistentQuestionCount = questions.Count;
        try {
            GetTaskAttemptData();
        } catch {
            Debug.Log("No data about this task yet or PlayerDataHandler is missing from scene. Starting as first attempt.");
            data.attempt = 1;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        //Get a new question
        SelectNewQuestion();
        // Set all text and values on screen
        SetQuestionValues();
        // Set all of the answer buttons text and correct answer values
        SetAnswerValues();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void GetQuestionAssets()
    {
        // Get all of the questions from the questions folder
        questions = new List<QuestionData_MI>(Resources.LoadAll<QuestionData_MI>("Questions_MI"));
    }

    private void SelectNewQuestion()
    {
        // Get a random value for which question to choose
        int randomQuestionIndex = Random.Range(0, questions.Count);
        //Set the question to the randon index
        currentQuestion = questions[randomQuestionIndex];
        // Remove this question from the list so it will not be repeated (until the game is restarted)
        questions.RemoveAt(randomQuestionIndex);
    }

    private void SetQuestionValues()
    {
        // Set the question text
        questionText.text = currentQuestion.question;
    }

    private void SetAnswerValues()
    {
        // Randomize the answer button order
        List<string> answers = RandomizeAnswers(new List<string>(currentQuestion.answers));

        // Set up the answer buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Create a temporary boolean to pass to the buttons
            bool isCorrect = false;

            // If it is the correct answer, set the bool to true
            if (i == correctAnswerChoice)
            {
                isCorrect = true;
            }

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
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

            // If the random number is 0, this is the correct answer, MAKE SURE THIS IS ONLY USED ONCE
            if (random == 0 && !correctAnswerChosen)
            {
                correctAnswerChoice = i;
                correctAnswerChosen = true;
            }

            // Add this to the new list
            newList.Add(originalList[random]);
            // Remove this choice from the original list (it has been used)
            originalList.RemoveAt(random);
        }

        return newList;
    }

    // Method to increment the count of correct answers
    public void IncrementCorrectAnswersCount()
    {
        //correctAnswersCount++;
        data.correctAmount++;
    }

    //Functions added by Teemu K below
    public void CloseTask(GameObject task) {
        Destroy(task);
    }
    public void CloseWindow(GameObject window) {
        window.SetActive(false);
    }

    public void ResetAttemptData() {
        GetQuestionAssets();
        SelectNewQuestion();
        SetQuestionValues();
        SetAnswerValues();
        for (int i = 0; i < data.questionData.Length; i++) {
            data.questionData[i].question = "";
            data.questionData[i].whatWasSelected = "";
            data.questionData[i].wasCorrect = false;
        }
        data.correctAmount = 0;
        data.attempt++;
    }

    public virtual void GetTaskAttemptData() {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        if (handler.currentPlayerData.task_MI_data.Count == 0) data.attempt = 1;
        else data.attempt = handler.currentPlayerData.task_MI_data.Count + 1;
    }

    public virtual void UpdateTaskData(bool correctAttempt) {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        Medium_Task_Data currentData = data.DataCopy();
        switch (correctAttempt) {
            case true:
            handler.currentPlayerData.task_MI_data.Add(currentData);
            handler.currentPlayerData.correctAttemptAmount_MI++;
            gameManager.EnableCheckMark(6);
            gameManager.UnlockFolder(6);
            break;

            case false:
            handler.currentPlayerData.task_MI_data.Add(currentData);
            break;
        }
    }

    public virtual IEnumerator TerminalMessage(string message, bool correct) {
        //Store old text fields
        string oldQuestinHeader = terminalQuestionHeader.text;
        string oldQuestionTxt = terminalQuestionTxt.text;

        switch (correct) {
            case true:
            gameManager.soundManager.PlayOneShot(1, true);
            terminalQuestionHeader.text = "Result";
            terminalQuestionTxt.text = $"{data.correctAmount} out of {persistentQuestionCount} were right\nAttempt: {data.attempt}\n" + message;
            foreach (GameObject objekti in objectsToHide) objekti.SetActive(false);
            yield return new WaitForSeconds(messageTimeOnTerminal);
            gameManager.soundManager.PlayOneShot(2, true);
            UpdateTaskData(true);
            FindObjectOfType<PlayerDataHandler>().LocalSaveData(); //Placeholder
            Destroy(gameObject);
            break;

            case false:
            gameManager.soundManager.PlayOneShot(0, true);
            terminalQuestionHeader.text = "Result";
            terminalQuestionTxt.text = $"{data.correctAmount} out of {persistentQuestionCount} were right\nAttempt: {data.attempt}\n" + message;
            foreach (GameObject objekti in objectsToHide) objekti.SetActive(false);
            yield return new WaitForSeconds(messageTimeOnTerminal);
            terminalQuestionHeader.text = oldQuestinHeader;
            terminalQuestionTxt.text = oldQuestionTxt;
            UpdateTaskData(false);
            ResetAttemptData();
            FindObjectOfType<PlayerDataHandler>().LocalSaveData(); //Placeholder
            foreach (GameObject objekti in objectsToHide) objekti.SetActive(true);
            break;
        }
    }
}
