using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnScript : MonoBehaviour
{
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Scene Restarted");
            //Application.LoadLevel(Application.loadedLevel);

            //string currentScene = SceneManager.GetActiveScene().name;
            //Debug.Log(currentScene);

            //SceneManager.LoadScene(currentScene);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == 9)
    //    {
    //        SceneManager.LoadScene("Main Scene");
    //    }
    //}
}
