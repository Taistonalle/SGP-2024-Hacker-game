using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct InputField_Data { //Adjust for each field
    public string fieldName;
    public string description;
    public string whatWasWritten; //This gets updated from the input field text
    public bool wasCorrect;
}

public class InputField_HI : MonoBehaviour {
    protected TMP_InputField field;
    public InputField_Data fieldData;

    protected void Start() {
        field = GetComponent<TMP_InputField>();
    }

    public void UpdateData(bool correct) {
        switch (correct) {
            case true:
            fieldData.wasCorrect = true;
            fieldData.whatWasWritten = field.text;
            break;
            case false:
            fieldData.wasCorrect = false;
            fieldData.whatWasWritten = field.text;
            break;
        }
    }
}
