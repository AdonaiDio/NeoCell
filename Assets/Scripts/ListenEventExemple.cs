using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenEventExemple : MonoBehaviour
{
    private void OnEnable()
    {
        Events.onEvent1.AddListener(Method1);
        Events.onEvent2.AddListener(Method2);
    }
    private void OnDisable()
    {
        Events.onEvent1.RemoveListener(Method1);
        Events.onEvent2.RemoveListener(Method2);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    void Method1()
    {
        //do something
    }
    void Method2(int i)
    {
        //do something
    }
}
