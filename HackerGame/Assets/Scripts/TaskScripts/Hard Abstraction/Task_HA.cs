using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task_HA : Task_HI {

    public override void CheckAnswer() {
        //Leave function if checkActive bool is true, else continue check
        if (checkActive) return;
        checkActive = true;
        hintNoteCounter++;

        //Field 1 check
        switch (inputFields[0].text) {
            case "this.id = id;":
            data.correctAmount++;
            inputFields[0].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(0);
            break;
            case "this.id= id;":
            data.correctAmount++;
            inputFields[0].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(0);
            break;
            case "this.id =id;":
            data.correctAmount++;
            inputFields[0].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(0);
            break;
            case "this.id=id;":
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

        //Field 2 check
        switch (inputFields[1].text) {
            case "{employee.Name}\");":
            data.correctAmount++;
            inputFields[1].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(1);
            break;
            default:
            inputFields[1].GetComponent<InputField_HI>().UpdateData(false);
            GetSpecificFieldData(1);
            break;
        }

        //Field 3 check
        switch (inputFields[2].text) {
            case "(EmployeeData employee)":
            data.correctAmount++;
            inputFields[2].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(2);
            break;
            default:
            inputFields[2].GetComponent<InputField_HI>().UpdateData(false);
            GetSpecificFieldData(2);
            break;
        }

        //Field 4 check
        switch (inputFields[3].text) {
            case "bossMan.GiveTaskToEmployee(dataGuy);":
            data.correctAmount++;
            inputFields[3].GetComponent<InputField_HI>().UpdateData(true);
            GetSpecificFieldData(3);
            break;
            default:
            inputFields[3].GetComponent<InputField_HI>().UpdateData(false);
            GetSpecificFieldData(3);
            break;
        }

        //Field 5 check
        switch (inputFields[4].text) {
            case "bossMan.CompareAge(dataGuy);":
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

    protected override void GetSpecificFieldData(int inputFieldIndex) {
        data.inputField_datas[inputFieldIndex].whatWasWritten = inputFields[inputFieldIndex].GetComponent<InputField_HA>().fieldData.whatWasWritten;
        data.inputField_datas[inputFieldIndex].wasCorrect = inputFields[inputFieldIndex].GetComponent<InputField_HA>().fieldData.wasCorrect;
    }
    protected override void GetFieldDatasInfo() {
        for (int i = 0; i < inputFields.Length; ++i) {
            data.inputField_datas[i].fieldName = inputFields[i].GetComponent<InputField_HA>().fieldData.fieldName;
            data.inputField_datas[i].description = inputFields[i].GetComponent<InputField_HA>().fieldData.description;
        }
    }

    protected override void GetTaskAttemptData() {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        if (handler.currentPlayerData.task_HA_data.Count == 0) data.attempt = 1;
        else data.attempt = handler.currentPlayerData.task_HA_data.Count + 1;
    }

    protected override void UpdateTaskData(bool correctAttempt) {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        Hard_Task_Data currentData = data.DataCopy();
        switch (correctAttempt) {
            case true:
            handler.currentPlayerData.task_HA_data.Add(currentData);
            handler.currentPlayerData.correctAttemptAmount_HA++;
            gameManager.EnableCheckMark(9);
            StartCoroutine(gameManager.CheckIfHardsDone());
            break;

            case false:
            handler.currentPlayerData.task_HA_data.Add(currentData);
            break;
        }
    }
}
