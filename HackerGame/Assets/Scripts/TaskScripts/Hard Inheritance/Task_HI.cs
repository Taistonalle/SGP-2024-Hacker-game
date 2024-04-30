using System;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Hard_Task_Data {
    public int attempt;
    public int correctAmount;
    public InputField_Data[] inputField_datas; //Adjust in inspector to be same amount as fields. But don't fill them manually, do this within the field object itself

    public Hard_Task_Data DataCopy() {
        Hard_Task_Data copy = new Hard_Task_Data();
        copy.attempt = attempt;
        copy.correctAmount = correctAmount;
        copy.inputField_datas = new InputField_Data[inputField_datas.Length];
        Array.Copy(inputField_datas, copy.inputField_datas, inputField_datas.Length);
        return copy;
    }
}

public class Task_HI : MonoBehaviour {
    protected GameManager gameManager;
    [SerializeField] protected TMP_InputField[] inputFields;
    [SerializeField] protected TextMeshProUGUI terminalTxt;
    [SerializeField] protected float messageTimeOnTerminal;

    [Space(10f)]
    [Header("Terminal messages")]
    [SerializeField] protected string correctMessage;
    [SerializeField] protected string wrongMessage;

    [Header("Notepad related")]
    [SerializeField] protected TextMeshProUGUI notePadTxt;
    [SerializeField] protected Button hintButton;
    [TextArea(5, 10)] //This makes inspector text box bigger
    [SerializeField] protected string hintTxt;
    protected int hintNoteCounter; //When this reaches like 3 give player option use hint button
    protected bool hintButtonShown;

    [Header("Data for handler")]
    [SerializeField] protected Hard_Task_Data data;

    protected void Start() {
        gameManager = FindObjectOfType<GameManager>();
        GetFieldDatasInfo();
        try {
            GetTaskAttemptData();
        } catch {
            Debug.Log("No data about this task yet or PlayerDataHandler is missing from scene. Starting as first attempt.");
            data.attempt = 1;
        }
    }

    //Call this function from button
    public virtual void CheckAnswer() {
        hintNoteCounter++;

        //Field 1 checks
        switch (inputFields[0].text) {
            case "register;":
            data.correctAmount++;
            inputFields[0].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(0);
            break;
            default:
            Debug.Log($"No correct answer for {inputFields[0].name}");
            inputFields[0].GetComponent<InputField_HI>().UpdateData(false);
            GetSpecificFieldData(0);
            break;
        }

        //Field 2 checks
        Regex rx2_1 = new Regex(@"\b(?=.*(string model)\b)(?=.*(string register)\b)");
        Regex rx2_2 = new Regex(@"^\(.*\)$");
        Regex rx2_3 = new Regex(@"^\(([^,\n]*),([^,\n]*)\)$");
        #region Pattern explanations_1
        /*
           rx2_3 Explanation:
           ^: Asserts the start of the string.
           \(: Matches the opening parenthesis "(".
           ([^,\n]*): Matches zero or more characters that are not commas or newline characters. This captures the characters between the opening parenthesis and the first comma into the first group.
           ,: Matches the comma between "word1" and "word2".
           ([^,\n]*): Matches zero or more characters that are not commas or newline characters. This captures the characters between the comma and the closing parenthesis into the second group.
           \): Matches the closing parenthesis ")".
           $: Asserts the end of the string.

           This pattern ensures that there is exactly one comma between "word1" and "word2" within parentheses.
         */
        #endregion

        if (rx2_1.IsMatch(inputFields[1].text) && rx2_2.IsMatch(inputFields[1].text) && rx2_3.IsMatch(inputFields[1].text)) {
            data.correctAmount++;
            inputFields[1].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(1);
        }
        else {
            Debug.Log($"No correct answer for {inputFields[1].name}");
            inputFields[1].GetComponent<InputField_HI>().UpdateData(false);
            GetSpecificFieldData(1);
        }

        #region Pattern explanations_2
        /* Terveisin ChatGPT, patternit on vaikeita ymm‰rt‰‰ mikrosoftin oman dokumentaation kautta
           rx3_1 Explanation:

           \b(?=.*string model\b): Positive lookahead assertion to ensure that the phrase "string model" exists somewhere in the string.
           \b(?=.*string register\b): Positive lookahead assertion to ensure that the phrase "string register" exists somewhere in the string.
           \b(?=.*string colour\b): Positive lookahead assertion to ensure that the phrase "string colour" exists somewhere in the string.

           This pattern will correctly match the phrase "string model", "string register", and "string colour" regardless of their order.
           @"\b(?=.*string model\b)(?=.*string register\b)(?=.*string colour\b)"

           rx3_2 Explanation:

           ^: Asserts the start of the string.
           \(: Matches the "(" character.
           .*: Matches any character (except for newline characters) zero or more times.
           \): Matches the ")" character.
           $: Asserts the end of the string.

           So, this pattern ensures that the string starts with "(" and ends with ")". Anything can appear in between them.
           @"^\(.*\)$"

           rx3_3 Explanation:

           ^: Asserts the start of the string.
           (?=[^,\n]*,[^,\n]*,[^,\n]*$): Positive lookahead assertion to ensure there are at least two commas in the string.
           [^,\s][^,]*[^,\s]: Matches a sequence of characters that are not commas or spaces, ensuring that there are no empty spaces between them.
           (?:,\s*[^,\s][^,]*[^,\s])*: Matches zero or more occurrences of a comma followed by optional spaces and a sequence of characters that are not commas or spaces, ensuring that there are no empty spaces between them.
           $: Asserts the end of the string.

           This pattern ensures that there are at least two commas in the string, and between the commas, there are no empty spaces.
           ^(?=[^,\n]*,[^,\n]*,[^,\n]*$)[^,\s][^,]*[^,\s](?:,\s*[^,\s][^,]*[^,\s])*$
          */
        #endregion
        Regex rx3_1 = new Regex(@"\b(?=.*(string model)\b)(?=.*(string register)\b)(?=.*(string colour)\b)");
        Regex rx3_2 = new Regex(@"^\(.*\)$");
        Regex rx3_3 = new Regex(@"^(?=[^,\n]*,[^,\n]*,[^,\n]*$)[^,\s][^,]*[^,\s](?:,\s*[^,\s][^,]*[^,\s])*$");

        //Field 3 checks
        if (rx3_1.IsMatch(inputFields[2].text) && rx3_2.IsMatch(inputFields[2].text) && rx3_3.IsMatch(inputFields[2].text)) {
            data.correctAmount++;
            inputFields[2].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(2);
        }
        else {
            Debug.Log($"No correct answer for {inputFields[2].name}");
            inputFields[2].GetComponent<InputField_HI>().UpdateData(false);
            GetSpecificFieldData(2);
        }

        //Field 4 checks
        switch (inputFields[3].text) {
            case "(transit.GetCarInfo());":
            data.correctAmount++;
            inputFields[3].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(3);
            break;
            default:
            inputFields[3].GetComponent<InputField_HI>().UpdateData(false);
            GetSpecificFieldData(3);
            break;
        }

        //Field 5 checks
        switch (inputFields[4].text) {
            case "(transit.GetColour());":
            data.correctAmount++;
            inputFields[4].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(4);
            break;
            default:
            inputFields[4].GetComponent<InputField_HI>().UpdateData(false);
            GetSpecificFieldData(4);
            break;
        }

        if (data.correctAmount == inputFields.Length) StartCoroutine(TerminalMessage(correctMessage, true));
        else {
            foreach (TMP_InputField field in inputFields) field.gameObject.SetActive(false);
            StartCoroutine(TerminalMessage(wrongMessage, false));
        }
    }

    public void CloseWindow(GameObject window) {
        window.SetActive(false);
    }

    public void CloseTask(GameObject task) {
        Destroy(task);
    }

    protected void ResetAttemptData() {
        for (int i = 0; i < data.inputField_datas.Length; i++) data.inputField_datas[i].wasCorrect = false;
        data.correctAmount = 0;
        data.attempt++;
    }

    protected virtual void GetSpecificFieldData(int inputFieldIndex) {
        data.inputField_datas[inputFieldIndex].whatWasWritten = inputFields[inputFieldIndex].GetComponent<InputField_HI>().fieldData.whatWasWritten;
        data.inputField_datas[inputFieldIndex].wasCorrect = inputFields[inputFieldIndex].GetComponent<InputField_HI>().fieldData.wasCorrect;
    }
    protected virtual void GetFieldDatasInfo() {
        for (int i = 0; i < inputFields.Length; ++i) {
            data.inputField_datas[i].fieldName = inputFields[i].GetComponent<InputField_HI>().fieldData.fieldName;
            data.inputField_datas[i].description = inputFields[i].GetComponent<InputField_HI>().fieldData.description;
        }
    }

    protected virtual void GetTaskAttemptData() {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        if (handler.currentPlayerData.task_HI_data.Count == 0) data.attempt = 1;
        else data.attempt = handler.currentPlayerData.task_HI_data.Count + 1;
    }

    protected virtual void UpdateTaskData(bool correctAttempt) {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        Hard_Task_Data currentData = data.DataCopy();
        switch (correctAttempt) {
            case true:
            handler.currentPlayerData.task_HI_data.Add(currentData);
            handler.currentPlayerData.correctAttemptAmount_HI++;
            gameManager.EnableCheckMark(10);
            break;

            case false:
            handler.currentPlayerData.task_HI_data.Add(currentData);
            break;
        }
    }

    //Button function, keep public
    public virtual void DisplayHintMsg() {
        string oldMsg = notePadTxt.text;
        notePadTxt.text = $"{oldMsg}\n\n{hintTxt}";

        hintButton.gameObject.SetActive(false);
    }

    protected virtual void CheckIfHintButtonShouldBeShown(int counterLimit) {
        if (hintNoteCounter == counterLimit) {
            switch (hintButtonShown) {
                case false:
                hintButtonShown = true;
                hintButton.gameObject.SetActive(true);
                break;
            }
        }
    }

    protected virtual IEnumerator TerminalMessage(string message, bool correct) {
        //Store old message
        string oldMessage = terminalTxt.text;

        switch (correct) {
            case true:
            terminalTxt.text = $"{data.correctAmount} out of {inputFields.Length} were right\nAttempt: {data.attempt}\n" + message;
            Debug.Log("Implement unlocking for harder task");
            foreach (TMP_InputField field in inputFields) field.gameObject.SetActive(false);
            yield return new WaitForSeconds(messageTimeOnTerminal);
            UpdateTaskData(true);
            FindObjectOfType<PlayerDataHandler>().LocalSaveData(); //Placeholder
            Destroy(gameObject);
            break;

            case false:
            terminalTxt.text = $"{data.correctAmount} out of {inputFields.Length} were right\nAttempt: {data.attempt}\n" + message;
            yield return new WaitForSeconds(messageTimeOnTerminal);
            terminalTxt.text = oldMessage;
            foreach (TMP_InputField field in inputFields) field.gameObject.SetActive(true);
            UpdateTaskData(false);
            ResetAttemptData();
            CheckIfHintButtonShouldBeShown(3);
            FindObjectOfType<PlayerDataHandler>().LocalSaveData(); //Placeholder
            break;
        }
    }
}
