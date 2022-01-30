using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnScript : MonoBehaviour
{
    public GameObject TryAgainPanel;
    public GameObject StaminaBarCanvas;

    public static bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;

        StaminaBarCanvas.SetActive(true);
        TryAgainPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //    Debug.Log("Scene Restarted");
        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            StartCoroutine(Delay());
            canMove = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public IEnumerator Delay()
    {
        canMove = false;
        Cursor.lockState = CursorLockMode.None;

        yield return new WaitForSeconds(1f);

        StaminaBarCanvas.SetActive(false);
        TryAgainPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Scene Restarted");
        TryAgainPanel.SetActive(false);
        
    }
}
