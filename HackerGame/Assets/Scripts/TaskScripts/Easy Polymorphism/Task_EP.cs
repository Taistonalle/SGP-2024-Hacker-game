using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_EP : Task_EE
{
    protected override void UpdateTaskData(bool correctAttempt)
    {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        Easy_Task_Data currentData = data.DataCopy();
        switch (correctAttempt)
        {
            case true:
                handler.currentPlayerData.task_EP_data.Add(currentData);
                handler.currentPlayerData.correctAttemptAmount_EP++;
                break;

            case false:
                handler.currentPlayerData.task_EP_data.Add(currentData);
                break;
        }
    }

    protected override void GetTaskAttemptData()
    {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();
        if (handler.currentPlayerData.task_EP_data.Count == 0) data.attempt = 1;
        else data.attempt = handler.currentPlayerData.task_EP_data.Count;
    }

    protected override void GetSlotDatasInfo()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            data.slot_datas[i].slotName = slots[i].GetComponent<Slot_EP>().slotData.slotName;
            data.slot_datas[i].description = slots[i].GetComponent<Slot_EP>().slotData.description;
        }
    }

}
