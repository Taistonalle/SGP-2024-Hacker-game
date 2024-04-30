using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
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

    [Header("Folder check marks")]
    [SerializeField] GameObject[] mark;

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

    public void EnableCheckMark(int markIndex) {
        mark[markIndex].SetActive(true);
    }

    private void CheckProgress() {
        PlayerDataHandler handler = FindObjectOfType<PlayerDataHandler>();

        if (handler.currentPlayerData.correctAttemptAmount_EE > 0) EnableCheckMark(0);
        if (handler.currentPlayerData.correctAttemptAmount_EA > 0) EnableCheckMark(1);
        if (handler.currentPlayerData.correctAttemptAmount_EI > 0) EnableCheckMark(2);
        if (handler.currentPlayerData.correctAttemptAmount_EP > 0) EnableCheckMark(3);
        if (handler.currentPlayerData.correctAttemptAmount_ME > 0) EnableCheckMark(4);
        if (handler.currentPlayerData.correctAttemptAmount_MA > 0) EnableCheckMark(5);
        if (handler.currentPlayerData.correctAttemptAmount_MI > 0) EnableCheckMark(6);
        if (handler.currentPlayerData.correctAttemptAmount_MP > 0) EnableCheckMark(7);
        if (handler.currentPlayerData.correctAttemptAmount_HE > 0) EnableCheckMark(8);
        if (handler.currentPlayerData.correctAttemptAmount_HA > 0) EnableCheckMark(9);
        if (handler.currentPlayerData.correctAttemptAmount_HI > 0) EnableCheckMark(10);
        if (handler.currentPlayerData.correctAttemptAmount_HP > 0) EnableCheckMark(11);
    }
}
