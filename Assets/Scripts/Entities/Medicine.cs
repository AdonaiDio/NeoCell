using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public Sprite icon;
    public string medicineName;
    public string medicineDescription;
    public string medicineEffects;
    public float medicinePrice;

    public MedicineSO medicineSO;
    void Awake()
    {
        //medicineName = medicineSO.medicineName;
        //medicineDescription = medicineSO.medicineDescription;
        //medicineEffects = medicineSO.medicineEffects;
        //icon = medicineSO.icon;

    }
    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {

    }
}
