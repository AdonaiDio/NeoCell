using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private MedicineSlot[] medicineSlots;

    //[SerializeField] private MedicineItem medicineItem;
    //public Medicine medicineReceived; 
    private void Awake(){
        
    }

        private void OnEnable()
    {
        Events.onMedicineCollected.AddListener(receiveDrop);


    }
            private void OnDisable()
    {
        Events.onMedicineCollected.RemoveListener(receiveDrop);


    }
    private void Update(){

    }
    public void AddMedicine(Medicine medicine){
        for (int i = 0; i < medicineSlots.Length; i++){
            MedicineSlot slot = medicineSlots[i];
            
            if (slot.itemInSlot.medicine == null){
                slot.itemInSlot.medicine = medicine;
                return;
            }
        }
    }

  
    private void receiveDrop(Medicine medicineReceived){

        
       // medicineItemReceived.medicine = medicine;
        if (medicineReceived != null){
        
        AddMedicine(medicineReceived);
        }
        else{
            Debug.Log("Remédio nulo!");
        }
      

        
    }
}
