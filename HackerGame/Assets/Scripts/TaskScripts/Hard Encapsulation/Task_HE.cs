using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class Task_HE : Task_HI {

    public override void CheckAnswer() {
        hintNoteCounter++;

        //Field 1 check
        switch (inputFields[0].text) {
            case "CardName":
            data.correctAmount++;
            inputFields[0].GetComponent<InputField_HE>().UpdateData(true);
            GetSpecificFieldData(0);
            break;
            default:
            Debug.Log($"No correct answer for {inputFields[0].name}");
            inputFields[0].GetComponent<InputField_HE>().UpdateData(false);
            GetSpecificFieldData(0);
            break;
        }

        Regex rx2_1 = new Regex(@"^\{.*\}$");
        Regex rx2_2 = new Regex(@"\b(?=.*return keyCode;)\b");

        //Field 2 check
        if (rx2_1.IsMatch(inputFields[1].text) && rx2_2.IsMatch(inputFields[1].text)) {
            data.correctAmount++;
            inputFields[1].GetComponent<InputField_HE>().UpdateData(true);
            GetSpecificFieldData(1);
        }
        else {
            Debug.Log($"No correct answer for {inputFields[1].name}");
            inputFields[1].GetComponent<InputField_HE>().UpdateData(false);
            GetSpecificFieldData(1);
        }

        //Field 3 check
        switch (inputFields[2].text) {
            case "Keycard()":
            data.correctAmount++;
            inputFields[2].GetComponent<InputField_HE>().UpdateData(true);
            GetSpecificFieldData(2);
            break;
            default:
            inputFields[2].GetComponent<InputField_HE>().UpdateData(false);
            GetSpecificFieldData(2);
            break;
        }

        //Field 4 check
        switch (inputFields[3].text) {
            case "321":
            data.correctAmount++;
            inputFields[3].GetComponent<InputField_HE>().UpdateData(true);
            GetSpecificFieldData(3);
            break;
            default:
            inputFields[3].GetComponent<InputField_HE>().UpdateData(false);
            GetSpecificFieldData(3);
            break;
        }

        if (data.correctAmount == inputFields.Length) StartCoroutine(TerminalMessage(correctMessage, true));
        else {
            foreach (TMP_InputField field in inputFields) field.gameObject.SetActive(false);
            StartCoroutine(TerminalMessage(wrongMessage, false));
        }
    }

    protected override void GetSpecificFieldData(int inputFieldIndex) {
        data.inputField_datas[inputFieldIndex].whatWasWritten = inputFields[inputFieldIndex].GetComponent<InputField_HE>().fieldData.whatWasWritten;
        data.inputField_datas[inputFieldIndex].wasCorrect = inputFields[inputFieldIndex].GetComponent<InputField_HE>().fieldData.wasCorrect;
    }
    protected override void GetFieldDatasInfo() {
        for (int i = 0; i < inputFields.Length; ++i) {
            data.inputField_datas[i].fieldName = inputFields[i].GetComponent<InputField_HE>().fieldData.fieldName;
            data.inputField_datas[i].description = inputFields[i].GetComponent<InputField_HE>().fieldData.description;
        }
    }

    protected override void GetTaskAttemptData() {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        if (handler.currentPlayerData.task_HE_data.Count == 0) data.attempt = 1;
        else data.attempt = handler.currentPlayerData.task_HE_data.Count + 1;
    }

    protected override void UpdateTaskData(bool correctAttempt) {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        Hard_Task_Data currentData = data.DataCopy();
        switch (correctAttempt) {
            case true:
            handler.currentPlayerData.task_HE_data.Add(currentData);
            handler.currentPlayerData.correctAttemptAmount_HE++;
            gameManager.EnableCheckMark(8);
            break;

            case false:
            handler.currentPlayerData.task_HE_data.Add(currentData);
            break;
        }
    }
}
