using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Load the new Scene
    public void LoadScene() {
        SceneManager.LoadScene(1);
    }

    //Quit the Application
    public void quitApplication() {
        Application.Quit();
    }
}
