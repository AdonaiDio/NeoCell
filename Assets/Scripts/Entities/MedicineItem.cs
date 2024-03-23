using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class MedicineItem : MonoBehaviour
{
    public Medicine medicine;
    
    public void Start(){
       
        
    }
    public void Update(){
        if (medicine != null){
        var newIcon = GetComponent<UnityEngine.UI.Image>();
        newIcon.sprite = medicine.icon;
        }
    }


}
