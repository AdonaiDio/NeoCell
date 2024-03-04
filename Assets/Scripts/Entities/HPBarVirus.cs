using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class HPBarVirus : MonoBehaviour
{
    [SerializeField] private Image barImage;

    [SerializeField] private Virus enemy;
    [SerializeField] private Canvas HPBarCanvas;
        private void OnEnable()
    {
        EventManager.onHpLostVirus.AddListener(loseHP);
        
    }
    private void OnDisable()
    {
        EventManager.onHpLostVirus.RemoveListener(loseHP);
      
    }
    private void Start(){
        
    }
    private void Update(){
      HPBarCanvas.transform.rotation = Quaternion.identity; //Stay in place when virus rotates
    }



    private void loseHP(Virus enemy, float hpToFillBar){
         enemy.hpBarImage.fillAmount = hpToFillBar; //calc hp fill

    }
}
