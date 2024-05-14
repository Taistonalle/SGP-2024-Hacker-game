using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoginMenuHandler_PH : MonoBehaviour {
    [SerializeField] GameObject screen;
    [SerializeField] GameObject notificationBox;
    [SerializeField] VideoPlayer animationPlayer;
    [SerializeField] GameObject skipButton;
    [SerializeField] TextMeshProUGUI notificationHeaderTxt;
    [SerializeField] TextMeshProUGUI notificationTxt;
    [SerializeField] float notificationOnScreenTime;

    public void CloseGame() {
        Application.Quit();
    }

    public void SoundSettings() {
        Debug.Log("Do sound settings stuff");
    }

    public IEnumerator NotificationMessage(string message, bool error) {
        switch (error) {
            case true:
            notificationHeaderTxt.text = "Error!";
            break;

            case false:
            notificationHeaderTxt.text = "Succes!";
            break;
        }

        //Enable notification box for a few seconds
        notificationBox.SetActive(true);
        notificationTxt.text = message;
        yield return new WaitForSeconds(notificationOnScreenTime);
        notificationBox.SetActive(false);
    }

    public IEnumerator LoadSceneAfterAnimation() {
        float animLenght = (float)animationPlayer.length;
        animationPlayer.Prepare();
        skipButton.SetActive(true);
        screen.SetActive(false);
        animationPlayer.Play();
        yield return new WaitForSeconds(animLenght); //Play animation first and then change scene
        SceneManager.LoadScene(1);
    }

    public void SkipAnimation() {
        StopAllCoroutines();
        SceneManager.LoadScene(1);
    }
}
