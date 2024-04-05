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
        currentRemedy = selectedSlot.remedySO;
        itemUIName.text = selectedSlot.remedySO._name;
        itemUIDescription.text = selectedSlot.remedySO._description;
        itemUIEffects.text = selectedSlot.remedySO._nextDescription;
        itemUIPrice.text = selectedSlot.remedySO._cost.ToString();

        void ChangeButtonText(List<RemedySO> list) {
            if (currentRemedy.GetType() == list[0].GetType()) {
                buyUpButton.gameObject.SetActive(true);
                if (list.IndexOf(currentRemedy) == 0)
                {
                    itemUIButtonText.text = "Ativar";
                    if (IsHotbarFull())
                    {
                        buyUpButton.gameObject.SetActive(false);
                    }
                }
                else if (list.IndexOf(currentRemedy) > 0)
                {
                    itemUIButtonText.text = "Upgrade";
                    if (IsCurrentRemedyMaximized(GetCurrentRemedyListType(currentRemedy)))
                    {
                        buyUpButton.gameObject.SetActive(false);
                    }
                }
            }
        }
        ChangeButtonText(GetCurrentRemedyListType(currentRemedy));
    }
    private bool IsHotbarFull()
    {
        foreach (MedicineSlot slot in medicineHotbarSlots)
        {
            if(slot.remedySO == null)
            {
                return false;
            }
        }
        return true;
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
                    slot.itemInSlot.enabled = true;
                    slot.itemInSlot.sprite = _remedySO._icon;
                    break; //impede que continue adicionando o mesmo ao outros slots
                }
            }
        }
    }
    private void buyOrUpgradeMedicine()
    {
        void CheckRemedyType(List<RemedySO> list, RemedySO changedRemedy)
        {
            //aumentar o nivel do remédio baseado na posição na lista do tipo
            if (currentRemedy.GetType() == list[0].GetType())
            {
                //se ele não for o ultimo nível
                if (list.IndexOf(currentRemedy) < list.Count-1)
                {
                    //aumenter pro próximo nível
                    changedRemedy = list[list.IndexOf(currentRemedy)+1];
                }
            }
        }
        void RefreshRemedylevel(List<RemedySO> list, MedicineSlot slot)
        {
            if (list[0].GetType() == currentRemedy.GetType())
            {
                //o slot recebe o seu equivalente na proxima posição da list
                //MAS se não houver upgrade, desativar o botão de upgrade
                if (list.IndexOf(currentRemedy) < list.Count - 1)
                {
                    //PODE DAR UPGRADE
                    slot.remedySO = list[list.IndexOf(currentRemedy) + 1];
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
                        slot.itemInSlot.enabled = true;
                        slot.itemInSlot.sprite = currentRemedy._icon;
                        Events.onRemedyActive.Invoke(currentRemedy);
                        //corrige os DNAPoints
                        DNAPointsManager.Instance.useDNAPoints(currentRemedy._cost);
                        break;
                    }
                }
            }
            else
            {
                if (!IsCurrentRemedyMaximized(GetCurrentRemedyListType(currentRemedy)))
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
                                        CheckRemedyType(GetCurrentRemedyListType(ms.remedySO), ms.remedySO);
                                    }
                                }
                                //corrige os DNAPoints
                                DNAPointsManager.Instance.useDNAPoints(currentRemedy._cost);
                                break;
                            }
                        }
                    }
                }
            }
            //apos a comprar ou upgrade com sucesso
            //substituir o remedio equivalente no slot do inventário
            foreach (MedicineSlot slot in medicineInventorySlots)
            {
                if (slot.remedySO != null)
                {
                    if (currentRemedy.GetType() == slot.remedySO.GetType())
                    {
                        RefreshRemedylevel(GetCurrentRemedyListType(slot.remedySO), slot);
                    }
                }
            }
        }
        Debug.Log("oi?");
        //refresh inventory
        ShowSelectedSlotInfo(GetCurrentSelectedSlotInventory());
    }

    private bool IsCurrentRemedyMaximized(List<RemedySO> list)
    {
        foreach (MedicineSlot slot in medicineHotbarSlots)
        {
            if (slot.remedySO != null)
            {
                if (slot.remedySO.GetType() == currentRemedy.GetType())
                {
                    if (list.Count - 1 == list.IndexOf(slot.remedySO))
                    {
                        //sim está no nivel maximo
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private bool HasThatRemedyType(RemedySO remedySO, List<MedicineSlot> medicineSlots)
    {
        foreach (MedicineSlot slot in medicineSlots)
        {
            if (slot.remedySO != null)
            {
                if (slot.remedySO.GetType() == remedySO.GetType())
                {
                    //Debug.Log("REMEDY TYPE IS: "+ remedySO.GetType()+" "+ slot.remedySO.GetType());
                    return true;
                }
            }
        }
        return false;
    }

    private MedicineSlot GetCurrentSelectedSlotInventory()
    {
        foreach (MedicineSlot slot in medicineInventorySlots)
        {
            if (slot.remedySO.GetType() == currentRemedy.GetType())
            {
                return slot;
            }
        }
        return medicineInventorySlots[0];
    }
    private List<RemedySO> GetCurrentRemedyListType(RemedySO remedy)
    {
        if (remedy.GetType() == Area_remedy_SO[0].GetType())
        {
            return Area_remedy_SO;
        }
        if (remedy.GetType() == Critical_remedy_SO[0].GetType())
        {
            return Critical_remedy_SO;
        }
        if (remedy.GetType() == Decay_remedy_SO[0].GetType())
        {
            return Decay_remedy_SO;
        }
        if (remedy.GetType() == Explosion_remedy_SO[0].GetType())
        {
            return Explosion_remedy_SO;
        }
        if (remedy.GetType() == Mines_remedy_SO[0].GetType())
        {
            return Mines_remedy_SO;
        }
        if (remedy.GetType() == Multiplicator_remedy_SO[0].GetType())
        {
            return Multiplicator_remedy_SO;
        }
        if (remedy.GetType() == Quantity_remedy_SO[0].GetType())
        {
            return Quantity_remedy_SO;
        }
        if (remedy.GetType() == Projectile_remedy_SO[0].GetType())
        {
            return Projectile_remedy_SO;
        }
        if (remedy.GetType() == Slowdown_remedy_SO[0].GetType())
        {
            return Slowdown_remedy_SO;
        }
        return Area_remedy_SO;
    }
}

