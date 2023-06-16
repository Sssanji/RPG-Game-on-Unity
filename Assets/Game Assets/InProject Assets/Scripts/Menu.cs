using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 
  
    public void PlayGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}
