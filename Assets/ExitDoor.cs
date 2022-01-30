using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(playerMovement.Body.transform.position, transform.position) < 2f)
        {
            SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
        }
    }
}
