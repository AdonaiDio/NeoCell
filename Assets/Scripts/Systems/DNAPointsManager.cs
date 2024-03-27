    using System.Collections;
    using System.Collections.Generic;

    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    public class DNAPointsManager : MonoBehaviour
    {

        public static DNAPointsManager Instance;

        public float currentDNAPoints = 0;
        public TextMeshProUGUI dnaValueText;


        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            } //Applying Singleton
            updateDNATextUI();

        }
        private void OnEnable()
        {
            Events.onDNAGained.AddListener(earnDNA);


        }
        private void OnDisable()
        {
            Events.onDNAGained.RemoveListener(earnDNA);


        }

        public void earnDNA(float amount)
        {
            //send to Cell
            currentDNAPoints += amount;
            updateDNATextUI();

        }
        public void updateDNATextUI(){
        dnaValueText.text = currentDNAPoints.ToString();
        }

    }
