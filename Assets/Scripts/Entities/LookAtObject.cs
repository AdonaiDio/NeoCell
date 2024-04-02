using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    private GameObject _object;
    private void Start()
    {
        _object = Camera.main.gameObject;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + _object.transform.rotation * Vector3.forward, _object.transform.rotation * Vector3.up);
    }
}
