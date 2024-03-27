using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Medicine", menuName = "MedicineSO")]
public class MedicineSO : ScriptableObject
{
    // Start is called before the first frame update
    public string medicineName;
    public string medicineDescription;
    public string medicineEffects;
    public float medicinePrice;
    public Sprite icon;



}
