using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_EA : Task_EE {
    
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
    
}
