using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float HPMax; //Max HP

    public float HP;

    public GameObject floatingDamage;
    
    private float cur_time = 0;
    private float frequency = 1f;

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
            PlaySoundAtFrequency();
        }
        else
        {
            PlaySoundAtFrequency();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_gameplay_char_damage, transform.position);
        }
    }
    private void PlaySoundAtFrequency()
    {
        cur_time += Time.time;
        if (Time.time - cur_time >= frequency)
        {
            if (HP<=0)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_gameplay_char_die, transform.position);
            }
            else
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_gameplay_char_damage, transform.position);
            }
            cur_time = Time.time;
        }
    }
}
