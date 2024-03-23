using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineSlot : MonoBehaviour
{
   public MedicineItem itemInSlot;
    void Awake(){
    itemInSlot = GetComponentInChildren<MedicineItem>();
    }
    public void CreateNewItem(Medicine medicine){
        itemInSlot = Instantiate(itemInSlot, transform);
        if (medicine != null) {
        
        itemInSlot.medicine = medicine;
        }
        else {
        Debug.Log ("Remédio nulo!");
        }
        if (itemInSlot.medicine == null){
        Debug.Log ("Remédio nulo!");
        }

    }
}
