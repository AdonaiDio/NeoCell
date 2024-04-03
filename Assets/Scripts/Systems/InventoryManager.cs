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
    public static InventoryManager Instance { get; private set; }//Singleton

    public Transform _slotsContainer;
    private List<MedicineSlot> medicineInventorySlots;
    public Transform _hotbarSlotsContainer;
    private List<MedicineSlot> medicineHotbarSlots;

    [SerializeField] private GameObject inventoryUI;

    //Remedy/Medicine
    [SerializeField] private RemedySO currentRemedy;
    [SerializeField] private List<RemedySO> remedySO_pool;

    //infos text
    [SerializeField] private GameObject infoUI;
    [SerializeField] private TextMeshProUGUI itemUIName;
    [SerializeField] private TextMeshProUGUI itemUIDescription;
    [SerializeField] private TextMeshProUGUI itemUIEffects;
    [SerializeField] private TextMeshProUGUI itemUIPrice;
    [SerializeField] private TextMeshProUGUI itemUIButtonText;
    [SerializeField] private Button buyUpButton;

    //lista de cada grupo de Upgrades de remedios
    [SerializeField] private List<RemedySO> Area_remedy_SO;
    [SerializeField] private List<RemedySO> Critical_remedy_SO;
    [SerializeField] private List<RemedySO> Decay_remedy_SO;
    [SerializeField] private List<RemedySO> Explosion_remedy_SO;
    [SerializeField] private List<RemedySO> Mines_remedy_SO;
    [SerializeField] private List<RemedySO> Multiplicator_remedy_SO;
    [SerializeField] private List<RemedySO> Quantity_remedy_SO;
    [SerializeField] private List<RemedySO> Projectile_remedy_SO;
    [SerializeField] private List<RemedySO> Slowdown_remedy_SO;

    //[SerializeField] private MedicineItem medicineItem;
    //public Medicine medicineReceived; 
    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("Found more than one Inventory Manager in the scene.");
        }
        Instance = this; //Applying Singleton

        //iniciar a lista de slots no invetario
        medicineInventorySlots = new List<MedicineSlot>();
        //iniciar a lista de slots no hotbar
        medicineHotbarSlots = new List<MedicineSlot>();
    }
    private void Start()
    {
        //adicionar slots eles a lista
        foreach (Transform mSlot in _slotsContainer)
        {
            medicineInventorySlots.Add(mSlot.GetComponent<MedicineSlot>());
        }
        //adicionar slots eles a lista
        foreach (Transform mSlot in _hotbarSlotsContainer)
        {
            medicineHotbarSlots.Add(mSlot.GetComponent<MedicineSlot>());
        }
    }
    private void OnEnable()
    {
        Events.onMedicineCollected.AddListener(AddMedicineToInvetory);
        Events.onSlotClicked.AddListener(ShowSelectedSlotInfo);
        buyUpButton.onClick.AddListener(buyOrUpgradeMedicine);
    }

    private void OnDisable()
    {
        Events.onMedicineCollected.RemoveListener(AddMedicineToInvetory);
        Events.onSlotClicked.RemoveListener(ShowSelectedSlotInfo);
        buyUpButton.onClick.RemoveListener(buyOrUpgradeMedicine);
    }
    public bool HasRemedyPool()
    {
        if (remedySO_pool.Count > 0)
        {
            return true;
        }
        return false;
    }
    public RemedySO DrawSOFromPool()
    {
        if (remedySO_pool.Count > 0) {
            int i = UnityEngine.Random.Range(0, remedySO_pool.Count);
            RemedySO remedyDrawn = remedySO_pool[i];
            remedySO_pool.Remove(remedySO_pool[i]);
            return remedyDrawn;
        }
        else {
            return null;
        }
    }

    /////
    public void ShowSelectedSlotInfo(MedicineSlot selectedSlot)
    {
        if (selectedSlot.remedySO == null)
        {
            return;
        }
        if (!infoUI.activeInHierarchy) {
            infoUI.SetActive(true);
        }
        itemUIName.text = selectedSlot.remedySO._name;
        itemUIDescription.text = selectedSlot.remedySO._description;
        itemUIEffects.text = selectedSlot.remedySO._nextDescription;
        itemUIPrice.text = selectedSlot.remedySO._cost.ToString();
        currentRemedy = selectedSlot.remedySO;
        //trocar o texto do botão 
        //if (currentRemedy is Remedy_Area)
        //{
        //    if (Area_remedy_SO.IndexOf(currentRemedy) > 0)
        //        itemUIButtonText.text = "Upgrade";
        //    else
        //        itemUIButtonText.text = "Ativar";
        //}
        //if (currentRemedy is Remedy_Critical)
        //{
        //    if (Critical_remedy_SO.IndexOf(currentRemedy) > 0)
        //        itemUIButtonText.text = "Upgrade";
        //    else
        //        itemUIButtonText.text = "Ativar";
        //}
        //if (currentRemedy is Remedy_Decay)
        //{
        //    if (Decay_remedy_SO.IndexOf(currentRemedy) > 0)
        //        itemUIButtonText.text = "Upgrade";
        //    else
        //        itemUIButtonText.text = "Ativar";
        //}
        //if (currentRemedy is Remedy_Explosion)
        //{
        //    if (Explosion_remedy_SO.IndexOf(currentRemedy) > 0)
        //        itemUIButtonText.text = "Upgrade";
        //    else
        //        itemUIButtonText.text = "Ativar";
        //}
        //if (currentRemedy is Remedy_Mines)
        //{
        //    if (Mines_remedy_SO.IndexOf(currentRemedy) > 0)
        //        itemUIButtonText.text = "Upgrade";
        //    else
        //        itemUIButtonText.text = "Ativar";
        //}
        //if (currentRemedy is Remedy_Multiplicator)
        //{
        //    if (Multiplicator_remedy_SO.IndexOf(currentRemedy) > 0)
        //        itemUIButtonText.text = "Upgrade";
        //    else
        //        itemUIButtonText.text = "Ativar";
        //}
        //if (currentRemedy is Remedy_Projectile)
        //{
        //    if (Projectile_remedy_SO.IndexOf(currentRemedy) > 0)
        //        itemUIButtonText.text = "Upgrade";
        //    else
        //        itemUIButtonText.text = "Ativar";
        //}
        //if (currentRemedy is Remedy_Quantity)
        //{
        //    if (Quantity_remedy_SO.IndexOf(currentRemedy) > 0)
        //        itemUIButtonText.text = "Upgrade";
        //    else
        //        itemUIButtonText.text = "Ativar";
        //}
        //if (currentRemedy is Remedy_Slowdown)
        //{
        //    if (Slowdown_remedy_SO.IndexOf(currentRemedy) > 0)
        //        itemUIButtonText.text = "Upgrade";
        //    else
        //        itemUIButtonText.text = "Ativar";
        //}
        void ChangeButtonText(Type type, List<RemedySO> list) {
            if (currentRemedy.GetType() == type) {
                if (list.IndexOf(currentRemedy) > 0) {
                    itemUIButtonText.text = "Upgrade";
                }
                else {
                    itemUIButtonText.text = "Ativar";
                }
            }
        }
        ChangeButtonText(typeof(Remedy_Area),Area_remedy_SO);
        ChangeButtonText(typeof(Remedy_Critical),Critical_remedy_SO);
        ChangeButtonText(typeof(Remedy_Decay),Decay_remedy_SO);
        ChangeButtonText(typeof(Remedy_Explosion),Explosion_remedy_SO);
        ChangeButtonText(typeof(Remedy_Mines),Mines_remedy_SO);
        ChangeButtonText(typeof(Remedy_Multiplicator),Multiplicator_remedy_SO);
        ChangeButtonText(typeof(Remedy_Projectile),Projectile_remedy_SO);
        ChangeButtonText(typeof(Remedy_Quantity),Quantity_remedy_SO);
        ChangeButtonText(typeof(Remedy_Slowdown),Slowdown_remedy_SO);
    }
    
    /////

    private void AddMedicineToInvetory(RemedySO _remedySO)
    {
        if (_remedySO == null) {
            Debug.LogWarning("Remédio nulo!");
            return;
        } else {
            foreach (MedicineSlot slot in medicineInventorySlots)
            {
                //o primeiro slot vazio que aparecer recebe o novo remédio
                if (slot.remedySO == null)
                {
                    slot.remedySO = _remedySO;
                    slot.itemInSlot.sprite = _remedySO._icon;
                    break; //impede que continue adicionando o mesmo ao outros slots
                }
            }
        }
    }
    private void buyOrUpgradeMedicine()
    {
        void CheckRemedyType(Type type, List<RemedySO> list, RemedySO changedRemedy)
        {
            //aumentar o nivel do remédio baseado na posição na lista do tipo
            if (currentRemedy.GetType() == type)
            {
                //se ele não for o ultimo nível
                if (list.IndexOf(currentRemedy) < list.Count-1)
                {
                    //aumenter pro próximo nível
                    changedRemedy = list[list.IndexOf(currentRemedy)+1];
                }
            }
        }
        //Tem dinheiro suficiente?
        if (DNAPointsManager.Instance.currentDNAPoints >= currentRemedy._cost)
        {
            //é upgrade ou compra? esse tipo de remédio, já existe no hotbar?
            if (!HasThatRemedyType(currentRemedy, medicineHotbarSlots))
            {
                //se é compra, add na lista de hotbar
                foreach (MedicineSlot slot in medicineHotbarSlots)
                {
                    if (slot.remedySO == null)
                    {
                        slot.remedySO = currentRemedy;
                        slot.itemInSlot.sprite = currentRemedy._icon;
                        Events.onRemedyActive.Invoke(currentRemedy);
                        break;
                    }
                }
            }
            else
            {
                //se é upgrade, atualiza o equivalente na hotbar e no player skill
                foreach (MedicineSlot slot in medicineHotbarSlots)
                {
                    if (slot.remedySO != null)
                    {
                        if(slot.remedySO.GetType() == currentRemedy.GetType())
                        {
                            slot.remedySO = currentRemedy;
                            slot.itemInSlot.sprite = currentRemedy._icon;
                            Events.onRemedyUpgrade.Invoke(currentRemedy);
                            //agora atualizar o nivel do remédio no inventário
                            foreach (MedicineSlot ms in medicineInventorySlots)
                            {
                                //se esse remedio no slot for o selecionado
                                if (ms.remedySO == currentRemedy)
                                {
                                    //aumentar o nivel do remédio baseado na posição na lista do tipo
                                    CheckRemedyType(typeof(Remedy_Area), Area_remedy_SO, ms.remedySO);
                                    CheckRemedyType(typeof(Remedy_Critical), Critical_remedy_SO, ms.remedySO);
                                    CheckRemedyType(typeof(Remedy_Decay), Decay_remedy_SO, ms.remedySO);
                                    CheckRemedyType(typeof(Remedy_Explosion), Explosion_remedy_SO, ms.remedySO);
                                    CheckRemedyType(typeof(Remedy_Mines), Mines_remedy_SO, ms.remedySO);
                                    CheckRemedyType(typeof(Remedy_Multiplicator), Multiplicator_remedy_SO, ms.remedySO);
                                    CheckRemedyType(typeof(Remedy_Projectile), Projectile_remedy_SO, ms.remedySO);
                                    CheckRemedyType(typeof(Remedy_Quantity), Quantity_remedy_SO, ms.remedySO);
                                    CheckRemedyType(typeof(Remedy_Slowdown), Slowdown_remedy_SO, ms.remedySO);
                                }
                            }
                        }
                        break;
                    }
                }
            }
            //corrige os DNAPoints
            DNAPointsManager.Instance.useDNAPoints(currentRemedy._cost);
        }
    }

    private bool HasThatRemedyType(RemedySO remedySO, List<MedicineSlot> medicineSlots)
    {
        foreach (MedicineSlot slot in medicineSlots)
        {
            if (slot.remedySO != null)
            {
                if (slot.remedySO.GetType() == remedySO.GetType())
                {
                    Debug.Log("REMEDY TYPE IS: "+ remedySO.GetType()+" "+ slot.remedySO.GetType());
                    return true;
                }
            }
        }
        return false;
    }
}

