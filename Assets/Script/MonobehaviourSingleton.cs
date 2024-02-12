using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonobehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this.GetComponent<T>();
            OnAwake();
        }
        else Destroy(this.gameObject);
    }
    public virtual void OnAwake() {}
}
