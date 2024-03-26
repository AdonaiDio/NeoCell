using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public MedicineSlot[] medicineInventorySlots;
    [SerializeField] public MedicineSlot[] medicineHotbarSlots;
    public int currentSlot = 0;
    [SerializeField] private GameObject inventoryUI;
    
    [SerializeField] TextMeshProUGUI itemUIName;
    [SerializeField] TextMeshProUGUI itemUIDescription;
    [SerializeField] TextMeshProUGUI itemUIEffects;
    [SerializeField] TextMeshProUGUI itemUIPrice;
    [SerializeField] Button buyButton;


    //[SerializeField] private MedicineItem medicineItem;
    //public Medicine medicineReceived; 
   // private void Awake(){
        
    //}

        private void OnEnable()
    {
        Events.onMedicineCollected.AddListener(receiveDrop);
        Events.onSlotClicked.AddListener(ShowSelectedSlotInfo);
        buyButton.onClick.AddListener(buyMedicine);


    }
    

  

    private void OnDisable()
    {
        Events.onMedicineCollected.RemoveListener(receiveDrop);
        Events.onSlotClicked.RemoveListener(ShowSelectedSlotInfo);
        buyButton.onClick.RemoveListener(buyMedicine);

    }
    private void Update(){

    }
    public void AddMedicine(Medicine medicine, MedicineSlot[] medicineSlots){
        for (int i = 0; i < medicineSlots.Length; i++){
            MedicineSlot slot = medicineSlots[i];
            
            if (slot.itemInSlot.medicine == null){
                slot.itemInSlot.medicine = medicine;
                slot.slotPosition = i;
                return;
            }

        }
        
    }
    public void ShowSelectedSlotInfo (int selectedSlot) {         
         for (int i = 0; i < medicineInventorySlots.Length; i++){
            MedicineSlot slot = medicineInventorySlots[i];
            if (slot.slotPosition == selectedSlot && slot.itemInSlot.medicine != null){
               
                //slot.itemInSlot.medicine.medicineSO = medicineSO;
                itemUIName.text = slot.itemInSlot.medicine.medicineName;
                itemUIDescription.text = slot.itemInSlot.medicine.medicineDescription;
                itemUIEffects.text = slot.itemInSlot.medicine.medicineEffects;
                itemUIPrice.text = slot.itemInSlot.medicine.medicinePrice.ToString();
                currentSlot = i;
                
            }
        }
    }

  
    private void receiveDrop(Medicine medicineReceived){

        
       // medicineItemReceived.medicine = medicine;
        if (medicineReceived != null){
        
        AddMedicine(medicineReceived, medicineInventorySlots);
        }
        else{
            Debug.Log("Remédio nulo!");
        }
      

        
    }
      private void buyMedicine()
    {
       for (int i = 0; i < medicineInventorySlots.Length; i++){
       MedicineSlot slot = medicineInventorySlots[i];
       if (slot.slotPosition == currentSlot && DNAPointsManager.Instance.currentDNAPoints >= slot.itemInSlot.medicine.medicinePrice && slot.itemInSlot.medicine != null ){
            AddMedicine(slot.itemInSlot.medicine, medicineHotbarSlots);
            DNAPointsManager.Instance.currentDNAPoints -= slot.itemInSlot.medicine.medicinePrice;
            DNAPointsManager.Instance.updateDNATextUI();
            return;     
            }
       
       }
    }
}
