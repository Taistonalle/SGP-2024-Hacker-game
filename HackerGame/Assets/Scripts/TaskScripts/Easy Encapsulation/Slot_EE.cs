using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Slot_Data { //Adjust for each slot their name and description
    public string slotName;
    public string description;
    public bool wasCorrect;
}
public class Slot_EE : MonoBehaviour {
    [SerializeField] private int id;
    public int Id {
        get { return id; }
        set { id = value; }
    }

    public Slot_Data slotData;
}
