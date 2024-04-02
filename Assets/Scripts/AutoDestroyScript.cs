using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyScript : MonoBehaviour
{
    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
