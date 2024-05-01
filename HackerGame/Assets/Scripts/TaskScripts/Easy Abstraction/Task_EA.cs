using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_EA : Task_EE {
    //Everything is inherited from Task_EE apart from few overrides related to data

    protected override void UpdateTaskData(bool correctAttempt) {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        Easy_Task_Data currentData = data.DataCopy();
        switch (correctAttempt) {
            case true:
            handler.currentPlayerData.task_EA_data.Add(currentData);
            handler.currentPlayerData.correctAttemptAmount_EA++;
            gameManager.EnableCheckMark(1);
            gameManager.UnlockFolder(1);
            break;

            case false:
            handler.currentPlayerData.task_EA_data.Add(currentData);
            break;
        }
    }

    protected override void GetTaskAttemptData() {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        if (handler.currentPlayerData.task_EA_data.Count == 0) data.attempt = 1;
        else data.attempt = handler.currentPlayerData.task_EA_data.Count + 1;
    }

    protected override void GetSlotDatasInfo() {
        for (int i = 0; i < slots.Length; ++i) {
            data.slot_datas[i].slotName = slots[i].GetComponent<Slot_EA>().slotData.slotName;
            data.slot_datas[i].description = slots[i].GetComponent<Slot_EA>().slotData.description;
        }
    }

}

    /* Old Override for demo purpose
    public override IEnumerator TerminalMessage(string message, bool correct) {
        //Store old message
        string oldMessage = terminalTxt.text;

        switch (correct) {
            case true:
            terminalTxt.text = $"{correctAmount} out of {slots.Length} were right\n" + message;
            Debug.Log("Implement unlocking for harder task");
            foreach (GameObject slot in slots) slot.SetActive(false);
            foreach (GameObject block in blocks) block.SetActive(false);
            yield return new WaitForSeconds(messageTimeOnTerminal);
            terminalTxt.text = "Critical error! Game is not finished, ending demo.."; //Remove this later
            yield return new WaitForSeconds(messageTimeOnTerminal);
            Debug.Log("Demo application closes");
            Application.Quit();
            //Destroy(gameObject);
            break;

            case false:
            terminalTxt.text = $"{correctAmount} out of {slots.Length} were right\n" + message;
            yield return new WaitForSeconds(messageTimeOnTerminal);
            terminalTxt.text = oldMessage;
            foreach (GameObject slot in slots) slot.SetActive(true);
            ResetCorretCounter();
            break;
        }
    }
    */

