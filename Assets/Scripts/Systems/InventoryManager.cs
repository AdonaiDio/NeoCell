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
    [SerializeField] public List<MedicineSlot> medicineInventorySlots;
    [SerializeField] public List<MedicineSlot> medicineHotbarSlots;
    //public int currentSlot = 0;
    [SerializeField] private Medicine currentMedicine;
    [SerializeField] private GameObject inventoryUI;
    public static InventoryManager Instance;

    [SerializeField] TextMeshProUGUI itemUIName;
    [SerializeField] TextMeshProUGUI itemUIDescription;
    [SerializeField] TextMeshProUGUI itemUIEffects;
    [SerializeField] TextMeshProUGUI itemUIPrice;
    [SerializeField] Button buyButton;
    [SerializeField] private List<MedicineSO> medicinesSO;





    //[SerializeField] private MedicineItem medicineItem;
    //public Medicine medicineReceived; 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        } //Applying Singleton
    }

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
    public MedicineSO DrawSOFromPool()
    {
        if (medicinesSO.Count > 0)
        {
            int i = UnityEngine.Random.Range(0, medicinesSO.Count);
            MedicineSO medicineDrawn = medicinesSO[i];
            medicinesSO.Remove(medicinesSO[i]);
            return medicineDrawn;
        }
        else
        {
            return null;
        }
    }
    private void Update()
    {

    }
    public void AddMedicine(Medicine medicine, List<MedicineSlot> medicineSlots)
    {
        for (int i = 0; i < medicineSlots.Count; i++)
        {
            MedicineSlot slot = medicineSlots[i];

            if (medicineSlots[i].itemInSlot.medicine == null)
            {

                medicineSlots[i].itemInSlot.medicine = Instantiate(medicine, transform);
                medicineSlots[i].slotPosition = i;
                return;
            }

        }

    }
    public void ShowSelectedSlotInfo(int selectedSlot)
    {
        for (int i = 0; i < medicineInventorySlots.Count; i++)
        {
            MedicineSlot slot = medicineInventorySlots[i];
            if (slot.slotPosition == selectedSlot && slot.itemInSlot.medicine != null)
            {

                //slot.itemInSlot.medicine.medicineSO = medicineSO;
                itemUIName.text = slot.itemInSlot.medicine.medicineName;
                itemUIDescription.text = slot.itemInSlot.medicine.medicineDescription;
                itemUIEffects.text = slot.itemInSlot.medicine.medicineEffects;
                itemUIPrice.text = slot.itemInSlot.medicine.medicinePrice.ToString();
                currentMedicine = slot.itemInSlot.medicine;

            }
        }
    }


    private void receiveDrop(Medicine medicineReceived)
    {


        // medicineItemReceived.medicine = medicine;
        if (medicineReceived != null)
        {
            AddMedicine(medicineReceived, medicineInventorySlots);
        }
        else
        {
            Debug.Log("Remédio nulo!");
        }



    }
    private void buyMedicine()
    {

        if (DNAPointsManager.Instance.currentDNAPoints >= currentMedicine.medicinePrice)
        {
            if (!checkHasMedicine(currentMedicine, medicineHotbarSlots))
            {
                AddMedicine(currentMedicine, medicineHotbarSlots);
                DNAPointsManager.Instance.useDNAPoints(currentMedicine.medicinePrice);
                return;
            }



        }
        return;
    }
    private bool checkHasMedicine(Medicine medicine, List<MedicineSlot> medicineSlots)
    {
        for (int i = 0; i < medicineSlots.Count; i++)
        {
            if (medicineSlots[i].itemInSlot.medicine != null)
            {
                if (medicineSlots[i].itemInSlot.medicine.medicineID == medicine.medicineID)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

