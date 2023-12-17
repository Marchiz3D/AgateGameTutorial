using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuMaager : MonoBehaviour
{
    public void Play() {
        SceneManager.LoadScene("Gameplay");
    }

    public void Exit() {
        Application.Quit();
    }
}
