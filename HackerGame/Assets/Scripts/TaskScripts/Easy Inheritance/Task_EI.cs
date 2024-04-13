// TEEMU PROTO
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;

// public class Task_EI : MonoBehaviour
// {
//     [SerializeField] GameObject[] slots, blocks;
//     [SerializeField] TextMeshProUGUI terminalTxt;
//     [SerializeField] float messageTimeOnTerminal;

//     [Space(10f)]
//     [Header("Terminal messages")]
//     [SerializeField] string correctMessage;
//     [SerializeField] string wrongMessage;

//     // Reference to the PlayerDataHandler object
//     public PlayerDataHandler playerDataHandler;

//     // Call this function from button
//     public void CheckAnswer()
//     {
//         bool correct = true; // Assume correct initially

//         for (int i = 0; i < slots.Length; i++)
//         {
//             // Check if block is in the correct slot
//             if (blocks[i].transform.position != slots[i].transform.position)
//             {
//                 correct = false;
//                 break;
//             }
//         }

//         if (correct)
//         {
//             // Send data to PlayerDataHandler if blocks are in correct slots
//             SendDataToPlayerDataHandler();
//             StartCoroutine(TerminalMessage(correctMessage, true));
//         }
//         else
//         {
//             foreach (GameObject block in blocks)
//             {
//                 block.GetComponent<CodeBlock_EI>().ResetBlockPos();
//             }
//             foreach (GameObject slot in slots)
//             {
//                 slot.SetActive(false);
//             }

//             StartCoroutine(TerminalMessage(wrongMessage, false));
//         }
//     }

//     // Send data to PlayerDataHandler
//     private void SendDataToPlayerDataHandler()
//     {
//         // Assuming playerDataHandler has PlayerData instance
//         if (playerDataHandler.currentPlayerData != null)
//         {
//             playerDataHandler.currentPlayerData.block1 = blocks[0].transform.position == slots[0].transform.position;
//             playerDataHandler.currentPlayerData.block2 = blocks[1].transform.position == slots[1].transform.position;
//             playerDataHandler.currentPlayerData.block3 = blocks[2].transform.position == slots[2].transform.position;
//             playerDataHandler.currentPlayerData.block4 = blocks[3].transform.position == slots[3].transform.position;
//             playerDataHandler.currentPlayerData.block5 = blocks[4].transform.position == slots[4].transform.position;
//         }
//     }

//     IEnumerator TerminalMessage(string message, bool correct)
//     {
//         // Store old message
//         string oldMessage = terminalTxt.text;

//         switch (correct)
//         {
//             case true:
//                 terminalTxt.text = message;
//                 Debug.Log("Implement unlocking for harder task");
//                 foreach (GameObject slot in slots)
//                 {
//                     slot.SetActive(false);
//                 }
//                 foreach (GameObject block in blocks)
//                 {
//                     block.SetActive(false);
//                 }
//                 yield return new WaitForSeconds(messageTimeOnTerminal);
//                 Destroy(gameObject);
//                 break;

//             case false:
//                 terminalTxt.text = message;
//                 yield return new WaitForSeconds(messageTimeOnTerminal);
//                 terminalTxt.text = oldMessage;
//                 foreach (GameObject slot in slots)
//                 {
//                     slot.SetActive(true);
//                 }
//                 break;
//         }
//     }
// }


///////////////////////////////////////////
////// THE ORIGINAL WALTTERI SCRIPT
//// 
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
