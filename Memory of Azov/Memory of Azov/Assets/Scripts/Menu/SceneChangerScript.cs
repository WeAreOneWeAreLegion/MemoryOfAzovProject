using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangerScript : MonoBehaviour {

    public float timeToInitialize = 0.2f;

    private float timer;
    private int sceneNum;
    private bool initialized;

    private void Start()
    {
        initialized = false;
    }

    public void Update()
    {
        if (initialized)
            if (Time.time >= timer + timeToInitialize)
            {
                SceneManager.LoadScene(sceneNum);
                initialized = false;
            }
    }

    public void GoToPlayScene()
    {
        Debug.Log("Go to play");
        sceneNum = 1;
        initialized = true;
    }

    /*public void GoToHowToPlayScene()
    {
        StartCoroutine(DelaySceneLoad(3));
    }*/

    public void GoToCreditsScene()
    {
        Debug.Log("Go to scene");
        sceneNum = 2;
        initialized = true;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        //UnityEditor.EditorApplication.isPlaying = false; //si le damos al botón de Quit en Unity, parará de jugar
#else
        Application.Quit(); //si le damos Quit fuera de Unity, cerrará el programa
#endif
    }

    public void GoBackToMenu()
    {
        StartCoroutine(DelaySceneLoad(0));
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


    IEnumerator DelaySceneLoad(int sceneNum)
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Load new scene "+sceneNum);
        SceneManager.LoadScene(sceneNum);
        yield return null;
    }
}
