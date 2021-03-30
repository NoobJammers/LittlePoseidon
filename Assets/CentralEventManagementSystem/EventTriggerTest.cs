using UnityEngine;
using System.Collections;

public class EventTriggerTest : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            EventManager.TriggerEvent("int", 1);

        }

        if (Input.GetKeyDown("w"))
        {
            EventManager.TriggerEvent("stringtest", "sup");
        }

    }
}
