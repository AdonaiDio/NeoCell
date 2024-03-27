    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class MedicineSlot : MonoBehaviour
    {
    public MedicineItem itemInSlot;
    public int slotPosition = 0;
        void Awake(){
        itemInSlot = GetComponentInChildren<MedicineItem>();
        
        }
        void OnEnable(){
        GetComponent<Button>().onClick.AddListener(() => sendSlotPosition(slotPosition));
        }
        void OnDisable(){
        GetComponent<Button>().onClick.RemoveListener(() => sendSlotPosition(slotPosition));
        }   
        public void sendSlotPosition (int slotPosition){
            Debug.Log("Posição do slot enviada: " +slotPosition);
            Events.onSlotClicked.Invoke(slotPosition);
            
        }

    }
