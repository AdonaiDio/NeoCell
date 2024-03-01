using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class HPBarVirus : MonoBehaviour
{
    [SerializeField] private Image barImage;

    [SerializeField] private Virus enemy;
    [SerializeField] private Canvas HPBarCanva;
    private void Start(){
        enemy.OnHPLost += Enemy_OnHPLost;
    }
    private void Update(){
      HPBarCanva.transform.rotation = Quaternion.identity; //Stay in place when virus rotates
    }



    private void Enemy_OnHPLost(object sender, Virus.OnHPLostEventArgs e){
      barImage.fillAmount = e.hpToFillBar; //calc hp fill
    }
}
