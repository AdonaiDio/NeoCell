using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float HPMax; //Max HP

    public float HP;

    public GameObject floatingDamage;

    private void Start()
    {
        HP = HPMax;
    }
    public void LoseHP(float damage = 1, bool isCritical = false)
    {
        HP -= damage;
        float hpToFillBar = HP / HPMax;
        Events.onHpLostCell.Invoke(hpToFillBar);
        GameObject floatTxt = Instantiate(floatingDamage,transform.Find("HPBarUI"));
        floatTxt.GetComponent<DamageIndicator>().damageNumber = damage;
        floatTxt.GetComponent<DamageIndicator>().isCritical = isCritical;

        if (HP <= 0)
        {
            Events.onPlayerDeath.Invoke();
        }
    }
}
