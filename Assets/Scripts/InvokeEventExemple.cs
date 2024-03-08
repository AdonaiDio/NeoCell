using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeEventExemple : MonoBehaviour
{
    void Start()
    {
        Events.onEvent1.Invoke();
    }
    void Update()
    {
        
    }
}
