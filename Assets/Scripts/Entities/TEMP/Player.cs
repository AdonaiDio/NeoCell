using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float HPMax; //Max HP

    private float HP;

    private void Start()
    {
        HP = HPMax;
    }

    public void LoseHP()
    {
        HP--;
        float hpToFillBar = HP / HPMax;
        Events.onHpLostCell.Invoke(hpToFillBar);
    }
}
