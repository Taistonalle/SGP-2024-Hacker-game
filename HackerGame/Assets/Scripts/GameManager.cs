using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    /*
    [Header("Encapsulation prefabs")]
    [SerializeField] GameObject easyEncapsulation;
    [SerializeField] GameObject mediumEncapsulation;
    [SerializeField] GameObject hardEncapsulation;

    [Space(10f)]
    [Header("Abstraction prefabs")]
    [SerializeField] GameObject easyAbstraction;
    [SerializeField] GameObject mediumAbstraction;
    [SerializeField] GameObject hardAbstraction;

    [Header("Inheritance prefabs")]
    [SerializeField] GameObject easyInheritance;
    [SerializeField] GameObject mediumInheritance;
    [SerializeField] GameObject hardInheritance;

    [Header("Polymorphism prefabs")]
    [SerializeField] GameObject easyPolymorphism;
    [SerializeField] GameObject mediumPolymorphism;
    [SerializeField] GameObject hardPolymorphism;
    */
    [Header("Folder check marks")]
    [SerializeField] GameObject[] mark;

    [Header("Folder buttons")]
    [SerializeField] Button[] folderButton;

    [Header("Hacked folder images")]
    [SerializeField] Image[] hackedImage;

    void Start() {
        CheckProgress();
    }

    public void LoadPrefab(GameObject prefabToLoad) {
        GameObject loadedPrefab;

        //Check if there already is active task on scene. Yes -> skip loading.
        if (GameObject.FindGameObjectWithTag("Task")) Debug.Log("Already one active task in scene");
        else loadedPrefab = Instantiate(prefabToLoad);
    }

    public void ScreenPowerButton() {
        Debug.Log("Implement returning back to login screen?");
        Application.Quit(); //For now just quit
    }

    public void EnableCheckMark(int markIndex) { //Index is seen from the inspector array. Semi hard coded like this, it is what it is.
        mark[markIndex].SetActive(true);
    }

    private void CheckProgress() {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();

        if (handler.currentPlayerData.correctAttemptAmount_EE > 0) {
            EnableCheckMark(0);
            UnlockFolder(0);
        }
        if (handler.currentPlayerData.correctAttemptAmount_EA > 0) {
            EnableCheckMark(1);
            UnlockFolder(1);
        }
        if (handler.currentPlayerData.correctAttemptAmount_EI > 0) {
            EnableCheckMark(2);
            UnlockFolder(2);
        }
        if (handler.currentPlayerData.correctAttemptAmount_EP > 0) {
            EnableCheckMark(3);
            UnlockFolder(3);
        }
        if (handler.currentPlayerData.correctAttemptAmount_ME > 0) {
            EnableCheckMark(4);
            UnlockFolder(0);
            UnlockFolder(4);
        }
        if (handler.currentPlayerData.correctAttemptAmount_MA > 0) {
            EnableCheckMark(5);
            UnlockFolder(1);
            UnlockFolder(5);
        }
        if (handler.currentPlayerData.correctAttemptAmount_MI > 0) {
            EnableCheckMark(6);
            UnlockFolder(2);
            UnlockFolder(6);
        }
        if (handler.currentPlayerData.correctAttemptAmount_MP > 0) {
            EnableCheckMark(7);
            UnlockFolder(3);
            UnlockFolder(7);
        }
        if (handler.currentPlayerData.correctAttemptAmount_HE > 0) {
            EnableCheckMark(8);
            UnlockFolder(4);
        }
        if (handler.currentPlayerData.correctAttemptAmount_HA > 0) {
            EnableCheckMark(9);
            UnlockFolder(5);
        }
        if (handler.currentPlayerData.correctAttemptAmount_HI > 0) {
            EnableCheckMark(10);
            UnlockFolder(6);
        }
        if (handler.currentPlayerData.correctAttemptAmount_HP > 0) {
            EnableCheckMark(11);
            UnlockFolder(7);
        }
    }

    public void UnlockFolder(int folderIndex) { //Again, index is seen from inspector array.
        folderButton[folderIndex].enabled = true;
        hackedImage[folderIndex].material = null;
    }
}
