using UnityEngine;
using System.Collections;

public class Singleton<T
    > : MonoBehaviour where T : MonoBehaviour
{
    protected static T s_instance;

    public static T instance
    {
        get
        {
            //check if instance is already in the scene
            if (s_instance == null)
                s_instance = (T)FindObjectOfType(typeof(T));
            //Create an instance  if it doesent exist
            //if (s_instance == null)
            //{
            //      GameObject newGameObject = newGameObject();
            //      newGameObject.AddComponent(typeof(T));
            //      s_instance = (T)newGameObject.GetComponent(typeof(T));
            //}
            return s_instance;
        }
    }
}
