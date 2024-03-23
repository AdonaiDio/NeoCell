using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Medicine", menuName = "MedicineSO")]
public class MedicineSO : ScriptableObject
{
    // Start is called before the first frame update
    public string medicineName; //virus, bacteria, fungus
    public string medicineDescription; //weak, strong and boss
    public Sprite icon;
    


}
