using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour
{
    [SerializeField] float timeToCanCallHelicopter = 20f;
    [SerializeField] float minTimeToCallHelicopter = 180f;
    private float timeSinceLastTrigger;
    
    private void Update()
    {
        timeSinceLastTrigger += Time.deltaTime;
        
        if (timeSinceLastTrigger > timeToCanCallHelicopter && Time.realtimeSinceStartup > minTimeToCallHelicopter)
        {
            Debug.Log("You can call now !!");
            if (Input.GetButtonDown("CallHeli")){
                SendMessageUpwards("OnFindClearArea");
            }
            
        }
    }

    private void OnTriggerStay()
    {
        timeSinceLastTrigger = 0f;
    }
}
