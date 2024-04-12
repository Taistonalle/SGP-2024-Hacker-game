using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class Task_HI : MonoBehaviour {
    [SerializeField] protected TMP_InputField[] inputFields;
    [SerializeField] protected TextMeshProUGUI terminalTxt;
    [SerializeField] protected float messageTimeOnTerminal;

    [Space(10f)]
    [Header("Terminal messages")]
    [SerializeField] protected string correctMessage;
    [SerializeField] protected string wrongMessage;

    [Header("Data for dabase?")]
    public int correctAmount;
    public int attempt;

    //Call this function from button
    public virtual void CheckAnswer() {
        //Field 1 checks
        switch (inputFields[0].text) {
            case "register;":
            correctAmount++;
            break;
            default:
            Debug.Log($"No correct answer for {inputFields[0].name}");
            break;
        }

        //Field 2 checks [Muuta tähän myöhemmin myös patterni check]
        switch (inputFields[1].text) {
            case "(string model, string register)":
            correctAmount++;
            break;
            case "(string model,string register)": //No space after comma
            correctAmount++;
            break;
            case "(string register, string model)":
            correctAmount++;
            break;
            case "(string register,string model)": //No space after comma
            correctAmount++;
            break;
            default:
            Debug.Log($"No correct answer for {inputFields[1].name}");
            break;
        }

        /* Terveisin ChatGPT, patternit on vaikeita ymmärtää mikrosoftin oman dokumentaation kautta
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

        Regex rx3_1 = new Regex(@"\b(?=.*(string model)\b)(?=.*(string register)\b)(?=.*(string colour)\b)");
        Regex rx3_2 = new Regex(@"^\(.*\)$");
        Regex rx3_3 = new Regex(@"^(?=[^,\n]*,[^,\n]*,[^,\n]*$)[^,\s][^,]*[^,\s](?:,\s*[^,\s][^,]*[^,\s])*$");

        //Field 3 checks
        if (rx3_1.IsMatch(inputFields[2].text) && rx3_2.IsMatch(inputFields[2].text) && rx3_3.IsMatch(inputFields[2].text)) correctAmount++;
        //if (rx3_3.IsMatch(inputFields[2].text)) correctAmount++;
        else Debug.Log($"No correct answer for {inputFields[2].name}");

        if (correctAmount == inputFields.Length) StartCoroutine(TerminalMessage(correctMessage, true));
        else {
            foreach (TMP_InputField field in inputFields) field.gameObject.SetActive(false);
            StartCoroutine(TerminalMessage(wrongMessage, false));
        }
    }

    protected void ResetCorretCounter() {
        correctAmount = 0;
    }

    public virtual IEnumerator TerminalMessage(string message, bool correct) {
        //Store old message
        string oldMessage = terminalTxt.text;

        switch (correct) {
            case true:
            terminalTxt.text = $"{correctAmount} out of {inputFields.Length} were right\n" + message;
            Debug.Log("Implement unlocking for harder task");
            foreach (TMP_InputField field in inputFields) field.gameObject.SetActive(false);
            yield return new WaitForSeconds(messageTimeOnTerminal);
            Destroy(gameObject);
            break;

            case false:
            terminalTxt.text = $"{correctAmount} out of {inputFields.Length} were right\n" + message;
            yield return new WaitForSeconds(messageTimeOnTerminal);
            terminalTxt.text = oldMessage;
            foreach (TMP_InputField field in inputFields) field.gameObject.SetActive(true);
            ResetCorretCounter();
            break;
        }
    }
}
