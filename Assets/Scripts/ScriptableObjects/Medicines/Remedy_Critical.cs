using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Critical", menuName = "RemedySO/Critical")]
public class Remedy_Critical : RemedySO
{
    [Tooltip("Critical chance in %")]
    public float _criticalChance;
}
