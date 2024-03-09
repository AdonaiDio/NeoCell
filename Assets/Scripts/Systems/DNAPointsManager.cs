using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DNAPointsManager : MonoBehaviour
{

    public static DNAPointsManager Instance;
    
    private float currentDNAPoints = 0;
    [SerializeField] TextMeshProUGUI dnaValueText;


    


    private void Awake(){
        if (Instance != null && Instance != this){
            Destroy(this); 
        }
        else{
            Instance = this;
        } //Applying Singleton
         dnaValueText.text = currentDNAPoints.ToString();
        
    }
            private void OnEnable()
    {
        Events.onDNAGained.AddListener(earnDNA);
      
        
    }
    private void OnDisable()
    {
        Events.onDNAGained.RemoveListener(earnDNA);

      
    }

    public void earnDNA(float amount){
        //send to Cell
        currentDNAPoints += amount;
        dnaValueText.text = currentDNAPoints.ToString();

    }

}
