using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fadeout_GameOver : StateMachineBehaviour
{
    private RespawnScript respawnScript;
    public RawImage fadeoutImage;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fadeoutImage = FindObjectOfType<RawImage>();
        respawnScript = FindObjectOfType<RespawnScript>();
        fadeoutImage.StartCoroutine(Fadeout());
    }

    IEnumerator Fadeout()
    {
        while (fadeoutImage.color.a < 0.99f)
        {
            fadeoutImage.color = new Color(fadeoutImage.color.r, fadeoutImage.color.g, fadeoutImage.color.b, fadeoutImage.color.a + Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("Game over!");
        respawnScript.StartCoroutine(respawnScript.Delay());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
