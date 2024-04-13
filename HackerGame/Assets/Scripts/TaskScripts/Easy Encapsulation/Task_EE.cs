using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task_EE : MonoBehaviour {

    [SerializeField] protected GameObject[] slots, blocks;
    [SerializeField] protected TextMeshProUGUI terminalTxt;
    [SerializeField] protected float messageTimeOnTerminal;

    [Space(10f)]
    [Header("Terminal messages")]
    [SerializeField] protected string correctMessage;
    [SerializeField] protected string wrongMessage;

    [Header("Data for database?")]
    public int correctAmount;
    public int attempt;

    void Start() {
        GetAttemptCount();
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

        //Check for each block if their bool value says they are in correct place. Yes => correctAmount + 1
        foreach (CodeBlock_EE block in FindObjectsOfType<CodeBlock_EE>()) { 
            if (block.inCorrectSlot) correctAmount++;
        }
        if (correctAmount == slots.Length) StartCoroutine(TerminalMessage(correctMessage, true));
        else {
            foreach (GameObject block in blocks) block.GetComponent<CodeBlock_EE>().ResetBlockPos();
            foreach (GameObject slot in slots) slot.SetActive(false);

            StartCoroutine(TerminalMessage(wrongMessage, false));
        }
    }

    protected void ResetCorretCounter() {
        correctAmount = 0;
        attempt++;
    }

    protected void GetAttemptCount() {
        Debug.Log("Implement data retrieval from data handler");
    }

    protected void SendAttemptCount() {
        Debug.Log("Implement data sending for data handler");
    }

    public virtual IEnumerator TerminalMessage(string message, bool correct) {
        //Store old message
        string oldMessage = terminalTxt.text;

        switch (correct) {
            case true:
            terminalTxt.text = $"{correctAmount} out of {slots.Length} were right\n" + message;
            Debug.Log("Implement unlocking for harder task");
            foreach (GameObject slot in slots) slot.SetActive(false);
            foreach (GameObject block in blocks) block.SetActive(false);
            yield return new WaitForSeconds(messageTimeOnTerminal);
            SendAttemptCount();
            Destroy(gameObject);
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
}
