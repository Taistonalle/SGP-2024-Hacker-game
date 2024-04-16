using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task_EI : Task_EE
{
    protected override void UpdateTaskData(bool correctAttempt) {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        Easy_Task_Data currentData = data.DataCopy();
        switch (correctAttempt) {
            case true:
            handler.currentPlayerData.task_EI_data.Add(currentData);
            handler.currentPlayerData.correctAttemptAmount_EI++;
            break;

            case false:
            handler.currentPlayerData.task_EI_data.Add(currentData);
            break;
        }
    }

    protected override void GetTaskAttemptData() {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        if (handler.currentPlayerData.task_EI_data.Count == 0) data.attempt = 1;
        else data.attempt = handler.currentPlayerData.task_EI_data.Count;
    }

    protected override void GetSlotDatasInfo() {
        for (int i = 0; i < slots.Length; ++i) {
            data.slot_datas[i].slotName = slots[i].GetComponent<Slot_EI>().slotData.slotName;
            data.slot_datas[i].description = slots[i].GetComponent<Slot_EI>().slotData.description;
        }
    }

}