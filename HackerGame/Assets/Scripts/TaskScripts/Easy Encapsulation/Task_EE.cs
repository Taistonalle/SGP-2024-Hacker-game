using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct Easy_Task_Data {
    public int attempt;
    public int correctAmount;
    //public bool[] slotThatWasCorrect; //Should be same amount as block amount. Adjust from inspector
    public Slot_Data[] slot_datas; //Same amount as blocks adjust from inspector

    //Deep copy
    public Easy_Task_Data DataCopy() {
        Easy_Task_Data copy = new Easy_Task_Data();
        copy.attempt = attempt;
        copy.correctAmount = correctAmount;
        //copy.slotThatWasCorrect = new bool[slotThatWasCorrect.Length];
        //Array.Copy(slotThatWasCorrect, copy.slotThatWasCorrect, slotThatWasCorrect.Length);
        copy.slot_datas = new Slot_Data[slot_datas.Length];
        Array.Copy(slot_datas, copy.slot_datas, slot_datas.Length);
        return copy;
    }
}

public class Task_EE : MonoBehaviour {
    protected GameManager gameManager;
    [SerializeField] protected GameObject[] slots, blocks;
    [SerializeField] protected TextMeshProUGUI terminalTxt;
    [SerializeField] protected float messageTimeOnTerminal;

    [Space(10f)]
    [Header("Terminal messages")]
    [SerializeField] protected string correctMessage;
    [SerializeField] protected string wrongMessage;

    [Header("Data for handler")]
    [SerializeField] protected Easy_Task_Data data;

    protected void Start() {
        gameManager = FindObjectOfType<GameManager>();
        GetSlotDatasInfo();
        try {
            GetTaskAttemptData();
        }
        catch {
            Debug.Log("No data about this task yet or PlayerDataHandler is missing from scene. Starting as first attempt.");
            data.attempt = 1;
        }
    }

    //Call this function from button
    public void CheckAnswer() {
        /* Old check style, checking if the position of slot and block are the same, works in unreliable way
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].transform.position == blocks[i].transform.position) {
                //correct = false;
                //break;
                ++correctAmount;
            }
            //else {
            //    ++correctAmount;
            //    //correct = true;
            //}
        }
        */

        //Check for each block if their bool value says they are in correct place. Yes => correctAmount + 1 and mark in data that slot was correct
        for (int i = 0; i < blocks.Length; i++) {
            if (blocks[i].GetComponent<CodeBlock_EE>().inCorrectSlot) {
                data.correctAmount++;
                //data.slotThatWasCorrect[i] = true;
                data.slot_datas[i].wasCorrect = true;
            }
        }
        if (data.correctAmount == slots.Length) StartCoroutine(TerminalMessage(correctMessage, true));
        else {
            foreach (GameObject block in blocks) block.GetComponent<CodeBlock_EE>().ResetBlockPos();
            foreach (GameObject slot in slots) slot.SetActive(false);

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
        //for (int i = 0; i < data.slotThatWasCorrect.Length; i++) data.slotThatWasCorrect[i] = false;
        for (int i = 0; i < data.slot_datas.Length; i++) data.slot_datas[i].wasCorrect = false;
        for (int i = 0; i < blocks.Length; ++i) blocks[i].GetComponent<CodeBlock_EE>().inCorrectSlot = false;
        data.correctAmount = 0;
        data.attempt++;
    }

    protected virtual void GetSlotDatasInfo() {
        for (int i = 0; i < slots.Length; ++i) {
            data.slot_datas[i].slotName = slots[i].GetComponent<Slot_EE>().slotData.slotName;
            data.slot_datas[i].description = slots[i].GetComponent<Slot_EE>().slotData.description;
        }
    }

    protected virtual void GetTaskAttemptData() {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        if (handler.currentPlayerData.task_EE_data.Count == 0) data.attempt = 1;
        else data.attempt = handler.currentPlayerData.task_EE_data.Count + 1;
    }

    protected virtual void UpdateTaskData(bool correctAttempt) {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        Easy_Task_Data currentData = data.DataCopy();
        switch (correctAttempt) {
            case true:
            handler.currentPlayerData.task_EE_data.Add(currentData);
            handler.currentPlayerData.correctAttemptAmount_EE++;
            gameManager.EnableCheckMark(0);
            break;

            case false:
            handler.currentPlayerData.task_EE_data.Add(currentData);
            break;
        }
    }

    protected virtual IEnumerator TerminalMessage(string message, bool correct) {
        //Store old message
        string oldMessage = terminalTxt.text;

        switch (correct) {
            case true:
            terminalTxt.text = $"{data.correctAmount} out of {slots.Length} were right\nAttempt: {data.attempt}\n" + message;
            Debug.Log("Implement unlocking for harder task");
            foreach (GameObject slot in slots) slot.SetActive(false);
            foreach (GameObject block in blocks) block.SetActive(false);
            yield return new WaitForSeconds(messageTimeOnTerminal);
            UpdateTaskData(true);
            Destroy(gameObject);
            break;

            case false:
            terminalTxt.text = $"{data.correctAmount} out of {slots.Length} were right\nAttempt: {data.attempt}\n" + message;
            yield return new WaitForSeconds(messageTimeOnTerminal);
            terminalTxt.text = oldMessage;
            foreach (GameObject slot in slots) slot.SetActive(true);
            UpdateTaskData(false);
            ResetAttemptData();
            break;
        }
    }
}
