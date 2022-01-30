using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoBackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DefaultScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
