using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public Sprite icon;
    [SerializeField] public MedicineSO medicineSO;
    void Awake()
    {
         icon = medicineSO.icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
