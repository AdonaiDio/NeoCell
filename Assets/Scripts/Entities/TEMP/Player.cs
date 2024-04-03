using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float HPMax; //Max HP

    public float HP;

    private void Start()
    {
        HP = HPMax;
    }

    public void LoseHP(float damage = 1)
    {
        HP -= damage;
        float hpToFillBar = HP / HPMax;
        Events.onHpLostCell.Invoke(hpToFillBar);
        if (HP <= 0)
            Events.onPlayerDeath.Invoke();
    }
}
