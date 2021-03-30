using UnityEngine;
using System.Collections.Generic;
using System;
public class EventManager : MonoBehaviour
{
    /*
Class to make a central non class dependent single paramter eventmanagement system.

A Dictionary of 'name' and corresponding eventobject represent the semantic name of the action and the object which 
holds the action variable and the Type variable.
    */

    private Dictionary<string, EventObject> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, EventObject>();
        }

    }

    /*
    listener and t: action<object> - it is wrapped together with the type and stored in an eventobject.
    key: name by which action should be stored. 
    */
    public static void StartListening(string name, Action<object> listener)
    {
        EventObject eventObject;
        if (instance.eventDictionary.TryGetValue(name, out eventObject))
        {
            if (eventObject != null)
            {
                eventObject.action += listener;
            }

        }
        else
        {
            instance.eventDictionary.Add(name, new EventObject(listener));
        }



    }

    /*
    Stops listening to the event.
    */

    public static void StopListening(string name, Action<object> listener)
    {
        EventObject eventObject;
        if (instance.eventDictionary.TryGetValue(name, out eventObject))
        {
            if (eventObject != null)
            {
                eventObject.action -= listener;
            }

        }
        else
        {
            Debug.Log("Didn't find the action, can't remove listener");
        }

    }


    //Trigger the event based on name and the value to send on trigger. has to be the same datatype otherwise casexception

    public static void TriggerEvent<T>(string name, T value)
    {

        EventObject eventObject;
        if (instance.eventDictionary.TryGetValue(name, out eventObject))
        {
            if (eventObject != null && eventObject.action != null)
            {
                try
                {
                    eventObject.action.Invoke(value);
                }
                catch (System.InvalidCastException e)
                {
                    Debug.Log("Not sending the correct cast value");
                }
            }

        }
        else
        {
            Debug.Log("Action not found in triggering");
        }
    }

    //Convert converts a generic action of type T to an object action. this makes sure we can keep all types of actions inside
    // the same dictionary.
    public static Action<object> Convert<T>(Action<T> action)
    {

        return delegate (object o) { action((T)o); };
    }




    public class EventObject
    {

        /*
        Class that wraps an action object and its original type before Convert().
*/
        public Action<object> action;
        public Type type;

        public EventObject(Action<object> action)
        {
            this.action = action;
            // this.type = t;
        }

        // public override bool Equals(object obj)
        // {


        //     if (obj == null || GetType() != obj.GetType())
        //     {
        //         return false;
        //     }
        //     if (type == ((EventObject)obj).GetType())
        //     {
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }


        // }

        // // override object.GetHashCode
        // public override int GetHashCode()
        // {
        //     // TODO: write your implementation of GetHashCode() here
        //     int hash = 13;
        //     hash = (hash * 7) + action.GetHashCode();
        //     hash = (hash * 7) + type.GetHashCode();

        //     return hash;
        // }
    }
}