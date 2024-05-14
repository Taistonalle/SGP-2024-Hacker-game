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
    [SerializeField] VideoClip startingAnimation, loopingAnimation, animAfterLogin;
    [SerializeField] GameObject skipButton;
    [SerializeField] TextMeshProUGUI notificationHeaderTxt;
    [SerializeField] TextMeshProUGUI notificationTxt;
    [SerializeField] float notificationOnScreenTime;

    private void Start() {
        StartCoroutine(GameStartAnimation());
    }

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
        //float animLenght = (float)animationPlayer.length;
        animationPlayer.clip = animAfterLogin;
        animationPlayer.Prepare();
        skipButton.SetActive(true);
        screen.SetActive(false);
        animationPlayer.Play();
        yield return new WaitUntil(() => (float)animationPlayer.frame >= animationPlayer.frameCount - 1); //Play animation first and then change scene
        SceneManager.LoadScene(1);
    }

    IEnumerator GameStartAnimation() {
        //float animLenght = (float)animationPlayer.length;
        animationPlayer.Prepare();
        screen.SetActive(false);
        animationPlayer.Play();
        yield return new WaitUntil(() => (float)animationPlayer.frame >= animationPlayer.frameCount - 1); // -1 ensures that animation actually stops
        screen.SetActive(true);
        animationPlayer.clip = loopingAnimation;
        animationPlayer.isLooping = true;
        animationPlayer.Prepare();
        animationPlayer.Play();
    }

    public void SkipAnimation() {
        StopCoroutine(LoadSceneAfterAnimation());
        SceneManager.LoadScene(1);
    }
}
