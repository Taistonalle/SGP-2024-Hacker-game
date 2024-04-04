using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeBlock_EI : DragUIElement
{

    [SerializeField] GameObject[] possibleSlots;
    /*
     [SerializeField] private int id;
     public int Id {
         get { return id; }
         set { id = value; }
     }
    */
    [SerializeField] float xPosTreshold, yPosTreshold;
    Vector2 startingPos;

    void Start()
    {
        startingPos = transform.position;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        CheckPosition();
    }

    void CheckPosition()
    {
        bool slotFound = false;
        Debug.Log($"Checking if drag ended over possible slot");
        for (int i = 0; i < possibleSlots.Length; i++)
        {
            if (transform.position.x < possibleSlots[i].transform.position.x - xPosTreshold || transform.position.y < possibleSlots[i].transform.position.y - yPosTreshold)
            {
                Debug.Log($"Current position on x: {transform.position.x} and slot position x for {possibleSlots[i].name} {possibleSlots[i].transform.position.x}" +
                    $"\nCurrent position on y: {transform.position.y} and slot position y for {possibleSlots[i].name} {possibleSlots[i].transform.position.y}" +
                    $"\nX or Y positions are still less!");

            }
            else if (transform.position.x > possibleSlots[i].transform.position.x + xPosTreshold || transform.position.y > possibleSlots[i].transform.position.y + yPosTreshold)
            {
                Debug.Log($"Current position on x: {transform.position.x} and slot position x for {possibleSlots[i].name} {possibleSlots[i].transform.position.x}" +
                  $"\nCurrent position on y: {transform.position.y} and slot position y for {possibleSlots[i].name} {possibleSlots[i].transform.position.y}" +
                  $"\nX or Y positions are still too much!");

            }
            else if (transform.position.x < possibleSlots[i].transform.position.x - xPosTreshold || transform.position.y > possibleSlots[i].transform.position.y + yPosTreshold)
            {
                Debug.Log($"Current position on x: {transform.position.x} and slot position x for {possibleSlots[i].name} {possibleSlots[i].transform.position.x}" +
                  $"\nCurrent position on y: {transform.position.y} and slot position y for {possibleSlots[i].name} {possibleSlots[i].transform.position.y}" +
                  $"\nX pos still too less or Y too much!");
            }
            else if (transform.position.x > possibleSlots[i].transform.position.x + xPosTreshold || transform.position.y < possibleSlots[i].transform.position.y - yPosTreshold)
            {
                Debug.Log($"Current position on x: {transform.position.x} and slot position x for {possibleSlots[i].name} {possibleSlots[i].transform.position.x}" +
                  $"\nCurrent position on y: {transform.position.y} and slot position y for {possibleSlots[i].name} {possibleSlots[i].transform.position.y}" +
                  $"\nX pos still too much or Y too less!");
            }
            else
            {
                LockBlockPos(i);
                slotFound = true;
                break;
            }
        }
        if (!slotFound)
        {
            Debug.Log("No matching slots");
            ResetBlockPos();
        }
    }

    public void ResetBlockPos()
    {
        Debug.Log($"Reseting {gameObject.name} to original space");
        transform.position = startingPos;
    }

    void LockBlockPos(int slot)
    {
        Debug.Log($"{gameObject.name} is close enough for to lock in {possibleSlots[slot]}");
        transform.position = possibleSlots[slot].transform.position;
    }
}