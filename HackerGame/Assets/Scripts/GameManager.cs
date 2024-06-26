using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.Video;

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

    public SoundManager soundManager;
    [SerializeField] GameObject volumeSlider;
    [SerializeField] VideoPlayer animationPlayer;
    [SerializeField] GameObject screen;

    void Start() {
        CheckProgress();
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) soundManager.PlayClickSound(0.05f);
    }

    public void LoadPrefab(GameObject prefabToLoad) {
        GameObject loadedPrefab;

        //Check if there already is active task on scene. Yes -> skip loading.
        if (GameObject.FindGameObjectWithTag("Task")) Debug.Log("Already one active task in scene");
        else loadedPrefab = Instantiate(prefabToLoad);
    }

    public void ScreenPowerButton() {
        soundManager.PlayOneShot(4, false);
        Destroy(FindObjectOfType<PlayerDataHandler>().gameObject); //Destroy "old" handler so login screen doesn't get confused.
        StartCoroutine(ChangeSceneWithDelay());
    }

    public void VolumeButton() {
        switch (volumeSlider.activeSelf) {
            case false:
            volumeSlider.SetActive(true);
            break;

            case true:
            volumeSlider.SetActive(false);
            break;
        }
        /*
        AudioSource gm = GetComponent<AudioSource>();
        AudioSource sm = soundManager.GetComponent<AudioSource>();

        switch (sm.mute) {
            case true:
            sm.mute = false;
            gm.mute = false;
            break;

            case false:
            sm.mute = true;
            gm.mute = true;
            break;
        }
        */
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
   
    public IEnumerator CheckIfHardsDone() {
        var handler = FindObjectOfType<PlayerDataHandler>().currentPlayerData;
        bool allDone = handler.correctAttemptAmount_HE > 0 && handler.correctAttemptAmount_HA > 0 && handler.correctAttemptAmount_HI > 0 && handler.correctAttemptAmount_HP > 0;

        if (allDone && !handler.unlockedAll) { //Further check if animation should be played
            float animLength = (float)animationPlayer.length;
            handler.unlockedAll = true;
            animationPlayer.Prepare();
            screen.SetActive(false);
            animationPlayer.Play();
            yield return StartCoroutine(WorkAroundTimer(animLength));
        }
    }

    IEnumerator WorkAroundTimer(float time) {
        yield return new WaitForSeconds(time);
        screen.SetActive(true);
        Destroy(animationPlayer.gameObject);
    }

    IEnumerator ChangeSceneWithDelay() {
        AudioSource sm = soundManager.GetComponent<AudioSource>();
        yield return new WaitUntil(() => sm.isPlaying == false); //Waits until sound is fully played
        SceneManager.LoadScene(0);
    }

    /*
    //Test screenshot save
    IEnumerator ScreenSaveTest() {
        //Read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();

        //Create a texture in RGB format the size of the screen
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new(width, height, TextureFormat.RGB24, false);

        //Read the screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        //Encode the texture in JPG format
        byte[] bytes = tex.EncodeToJPG();
        Destroy(tex);

        //Write the returned byte array to a file in desktop
        File.WriteAllBytes(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Testi.jpg"), bytes);
        Debug.Log($"Finished screen save to: {Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}");
    }
    */
}
