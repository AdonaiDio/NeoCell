using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicineSlot : MonoBehaviour
{
    public Image itemInSlot;
    public RemedySO remedySO;

    void Awake()
    {
        itemInSlot = transform.GetChild(0).GetComponent<Image>();
    }
    void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => selectSlot());
    }
    void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(() => selectSlot());
    }
    public void selectSlot()
    {
        Debug.Log("Posição do slot enviada: " + this);
        Events.onSlotClicked.Invoke(this);
    }

}
