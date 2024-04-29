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
}
