using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] GameObject _object;
    void LateUpdate()
    {
        transform.LookAt(transform.position + _object.transform.rotation * Vector3.forward, _object.transform.rotation * Vector3.up);
    }
}
