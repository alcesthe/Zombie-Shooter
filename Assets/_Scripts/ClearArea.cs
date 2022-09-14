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
            if (timeSinceLastTrigger > timeToCanCallHelicopter && gameManager.GetIsHelicopterAvailable())
            {
                SendMessageUpwards("OnFindClearArea");
            } else
            {
                Debug.Log("TODO NEED MORE SPACE DIALOG");
            }
            
        }
    }

    private void OnTriggerStay()
    {
        timeSinceLastTrigger = 0f;
    }
}
