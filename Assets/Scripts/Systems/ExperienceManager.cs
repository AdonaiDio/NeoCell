using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;
    [SerializeField] private float maxExperience = 10;
    private float currentExperience = 0;
    [SerializeField] TextMeshProUGUI levelText;

    
    [SerializeField] private Cell player;
    [SerializeField] UnityEngine.UI.Image barImage;
    private void Awake(){
        if (Instance != null && Instance != this){
            Destroy(this); 
        }
        else{
            Instance = this;
        } //Applying Singleton
        
    }
            private void OnEnable()
    {
        Events.onXPGained.AddListener(earnXP);
        Events.onLevelUp.AddListener(levelUp);
        
    }
    private void OnDisable()
    {
        Events.onXPGained.RemoveListener(earnXP);
        Events.onLevelUp.RemoveListener(levelUp);
      
    }
    private void Update(){
    }
    public void earnXP(float amount){
        //send to Cell
        currentExperience += amount;
        barImage.fillAmount = currentExperience/maxExperience; //receive experience normalized to calc fill ammount
    }
    public void levelUp(float level){
        levelText.text = level.ToString(); //Change level text value
        currentExperience = 0;
    }
}
