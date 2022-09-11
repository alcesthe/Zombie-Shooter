using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour
{   
    public float timeSinceLastTrigger = 5f;

    private void Update()
    {
        timeSinceLastTrigger += Time.deltaTime;
        if (timeSinceLastTrigger > 5f && Time.realtimeSinceStartup > 10)
        {
            SendMessageUpwards("OnFindClearArea");
        }
    }

    private void OnTriggerStay()
    {
        timeSinceLastTrigger = 0f;
    }
}
