using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour
{
    [SerializeField] float timeToCanCallHelicopter = 20f;
    

    private float timeSinceLastTrigger;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        timeSinceLastTrigger += Time.deltaTime;
        if (Input.GetButtonDown("CallHeli"))
        {
            if (gameManager.GetIsHelicopterAvailable())
            {
                if (timeSinceLastTrigger > timeToCanCallHelicopter)
                {
                    SendMessageUpwards("OnFindClearArea");
                    gameManager.SetMessage("Helicopter is comming !!! Defend yourself");
                }
                else
                {
                    gameManager.SetMessage("You need to stand in a clear area to call the Helicopter & Wait for about few seconds");
                }
            }
            else
            {
                gameManager.SetMessage("You need wait for the timer count down to call Helicopter");
            }
        }
    }

    private void OnTriggerStay()
    {
        timeSinceLastTrigger = 0f;
    }
}
