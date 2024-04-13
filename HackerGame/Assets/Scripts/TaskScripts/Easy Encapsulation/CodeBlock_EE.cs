using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeBlock_EE : DragUIElement {

    [SerializeField] protected GameObject[] possibleSlots;

    [SerializeField] private int id;
    public int Id {
        get { return id; }
        set { id = value; }
    }

    //[SerializeField] protected float xPosTreshold, yPosTreshold;
    [SerializeField] protected float minDistanceFromSlot;
    protected BoxCollider2D colllider;
    protected ColliderDistance2D colliderDistance2D;
    Vector2 startingPos;

    [Header("Slot in correct place bool")]
    public bool inCorrectSlot = false;

    void Start() {
        colllider = GetComponent<BoxCollider2D>();
        SetStartPos();
    }
    public override void OnEndDrag(PointerEventData eventData) {
        CheckPosition();
    }

    // Old style checking positon, this was with transform positions, kinda works but also kinda buggy. Unreliable
    //public virtual void CheckPosition() {
    //    bool slotFound = false;
    //    Debug.Log($"Checking if drag ended over possible slot");
    //    for (int i = 0; i < possibleSlots.Length; i++) {
    //        if (transform.position.x < possibleSlots[i].transform.position.x - xPosTreshold || transform.position.y < possibleSlots[i].transform.position.y - yPosTreshold) {
    //            /*
    //            Debug.Log($"Current position on x: {transform.position.x} and slot position x for {possibleSlots[i].name} {possibleSlots[i].transform.position.x}" +
    //                $"\nCurrent position on y: {transform.position.y} and slot position y for {possibleSlots[i].name} {possibleSlots[i].transform.position.y}" +
    //                $"\nX or Y positions are still less!");
    //            */
    //        }
    //        else if (transform.position.x > possibleSlots[i].transform.position.x + xPosTreshold || transform.position.y > possibleSlots[i].transform.position.y + yPosTreshold) {
    //            /*
    //            Debug.Log($"Current position on x: {transform.position.x} and slot position x for {possibleSlots[i].name} {possibleSlots[i].transform.position.x}" +
    //              $"\nCurrent position on y: {transform.position.y} and slot position y for {possibleSlots[i].name} {possibleSlots[i].transform.position.y}" +
    //              $"\nX or Y positions are still too much!");
    //            */
    //        }
    //        else if (transform.position.x < possibleSlots[i].transform.position.x - xPosTreshold || transform.position.y > possibleSlots[i].transform.position.y + yPosTreshold) {
    //            /*
    //            Debug.Log($"Current position on x: {transform.position.x} and slot position x for {possibleSlots[i].name} {possibleSlots[i].transform.position.x}" +
    //              $"\nCurrent position on y: {transform.position.y} and slot position y for {possibleSlots[i].name} {possibleSlots[i].transform.position.y}" +
    //              $"\nX pos still too less or Y too much!");
    //            */
    //        }
    //        else if (transform.position.x > possibleSlots[i].transform.position.x + xPosTreshold || transform.position.y < possibleSlots[i].transform.position.y - yPosTreshold) {
    //            /*
    //            Debug.Log($"Current position on x: {transform.position.x} and slot position x for {possibleSlots[i].name} {possibleSlots[i].transform.position.x}" +
    //              $"\nCurrent position on y: {transform.position.y} and slot position y for {possibleSlots[i].name} {possibleSlots[i].transform.position.y}" +
    //              $"\nX pos still too much or Y too less!");
    //            */
    //        }
    //        else {
    //            LockBlockPos(i);
    //            slotFound = true;
    //            break;
    //        }
    //    }
    //    if (!slotFound) {
    //        Debug.Log("No matching slots");
    //        ResetBlockPos();
    //    }
    //}

    public virtual void CheckPosition() {
        int slotId;
        Debug.Log("Checking if drag ended over a slot");
        for (int i = 0; i < possibleSlots.Length; i++) {
            colliderDistance2D = possibleSlots[i].GetComponent<BoxCollider2D>().Distance(colllider); //Check the distance from possible slot
            if (colliderDistance2D.distance <= minDistanceFromSlot) { //When colliders overlap eachother the distance value goes below zero. And touching its zero
                slotId = possibleSlots[i].GetComponent<Slot_EE>().Id;
                LockBlockPos(i);
                if (slotId ==  id) inCorrectSlot = true;
                else inCorrectSlot = false;
                break;
            }
            else {
                Debug.Log("Not near slot, reseting back to starting position");
                inCorrectSlot = false;
                ResetBlockPos();
            }
        }
    }

    public void ResetBlockPos() {
        Debug.Log($"Reseting {gameObject.name} to original space");
        transform.position = startingPos;
    }
    public void SetStartPos() {
        startingPos = transform.position;
    }

    public void LockBlockPos(int slot) {
        Debug.Log($"{gameObject.name} is close enough for to lock in {possibleSlots[slot]}");
        transform.position = possibleSlots[slot].transform.position;
    }
}
