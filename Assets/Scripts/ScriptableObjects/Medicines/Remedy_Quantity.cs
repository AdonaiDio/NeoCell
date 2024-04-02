using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quantity", menuName = "RemedySO/Quantity")]
public class Remedy_Quantity : RemedySO
{
    [Tooltip("Enemies At Once value >= 1.")]
    public int _enemiesAtOnce;
}