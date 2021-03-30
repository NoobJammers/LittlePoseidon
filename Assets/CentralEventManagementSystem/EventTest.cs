using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;
public class EventTest : MonoBehaviour
{

    private UnityAction someListener;

    void Awake()
    {
        // someListener = new UnityAction(SomeFunction);
    }

    //Example of how to make the action object with int as parameter
    Action<object> SomeThirdAction = EventManager.Convert((int a) =>
     {


         Debug.Log("Some Third Function was called!" + a);
     }
     );

    /*
       poriginal function

      a (int a){Debug.Log(a);}

      b(object b){a((int)b))}
    */

    //Example of how to make the action object with string as parameter
    Action<object> SomeStringAction = EventManager.Convert(
        (string b) =>
        {
            Debug.Log("string function called" + b);
        }
    );
    void OnEnable()
    {


        EventManager.StartListening("int", SomeThirdAction);
        EventManager.StartListening("stringtest", SomeStringAction);
        StartCoroutine(stopit());


    }

    void OnDisable()
    {
    }
    IEnumerator stopit()
    {
        yield return new WaitForSecondsRealtime(3);

        // EventManager.StopListening("int", SomeThirdAction);
        // EventManager.StopListening("stringtest", SomeStringAction);

    }


}
