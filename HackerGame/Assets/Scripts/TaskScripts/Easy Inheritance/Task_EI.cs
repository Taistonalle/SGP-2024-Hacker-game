using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task_EI : MonoBehaviour
{

    [SerializeField] GameObject[] slots, blocks;
    [SerializeField] TextMeshProUGUI terminalTxt;
    [SerializeField] float messageTimeOnTerminal;

    [Space(10f)]
    [Header("Terminal messages")]
    [SerializeField] string correctMessage;
    [SerializeField] string wrongMessage;

    //Call this function from button
    public void CheckAnswer()
    {
        bool correct = false;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.position != blocks[i].transform.position)
            {
                correct = false;
                break;
            }
            else correct = true;
        }

        if (correct) StartCoroutine(TerminalMessage(correctMessage, true));
        else if (!correct)
        {
            foreach (GameObject block in blocks) block.GetComponent<CodeBlock_EI>().ResetBlockPos();
            foreach (GameObject slot in slots) slot.SetActive(false);

            StartCoroutine(TerminalMessage(wrongMessage, false));
        }
    }


    IEnumerator TerminalMessage(string message, bool correct)
    {
        //Store old message
        string oldMessage = terminalTxt.text;

        switch (correct)
        {
            case true:
                terminalTxt.text = message;
                Debug.Log("Implement unlocking for harder task");
                foreach (GameObject slot in slots) slot.SetActive(false);
                foreach (GameObject block in blocks) block.SetActive(false);
                yield return new WaitForSeconds(messageTimeOnTerminal);
                Destroy(gameObject);
                break;

            case false:
                terminalTxt.text = message;
                yield return new WaitForSeconds(messageTimeOnTerminal);
                terminalTxt.text = oldMessage;
                foreach (GameObject slot in slots) slot.SetActive(true);
                break;
        }
    }
}
