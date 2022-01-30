using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField]
    private Slider staminaBar;

    int totalStamina = 100;
    int currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.03f);
    private Coroutine regen;

    public static PlayerStamina instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentStamina = totalStamina;
        staminaBar.maxValue = totalStamina;
        staminaBar.value = totalStamina;
        //staminaBar = GetComponent<Slider>();
    }

    public void UseStamina(int amount)
    {
        if (currentStamina - amount > 0 && PlayerMovement.movementInstance.isRunning)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("Not enough stamina!");
        }
    }
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);

        while(staminaBar.value < totalStamina)
        {
            currentStamina += totalStamina / totalStamina;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }

    #region OldCode
    //void LosingStamina()
    //{
    //    staminaReduces = true;
    //}
    //void StopLosingStamina()
    //{
    //    staminaReduces = false;
    //}
    //public void UpdateStaminaBar(float decreaseRate)
    //{
    //    totalStamina += decreaseRate;

    //    if (totalStamina - decreaseRate < 0)
    //    {


    //        totalStamina += decreaseRate;
    //    }
    //}
    #endregion
}
