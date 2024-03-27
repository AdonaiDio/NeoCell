    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    public class MedicineDrop : MonoBehaviour
    {
        private Transform player;
        public Medicine medicine;
        [SerializeField] private float distanceToMove;
        [SerializeField] private float distanceToDestroy;    
        [SerializeField] private float speed;
        

        void Awake(){
            
            medicine = Instantiate (medicine, transform);
            medicine.medicineSO = InventoryManager.Instance.DrawSOFromPool();       
            if (medicine.medicineSO != null){
            medicine.medicineName = medicine.medicineSO.medicineName;
            medicine.medicineDescription = medicine.medicineSO.medicineDescription;
            medicine.medicineEffects = medicine.medicineSO.medicineEffects;
            medicine.icon = medicine.medicineSO.icon;
            medicine.medicinePrice = medicine.medicineSO.medicinePrice;
            
            
            }
            else{
                Destroy(gameObject);
            }
            //GetComponentInChildren<SpriteRenderer>().sprite = medicine.icon;
            medicine.GetComponentInChildren<SpriteRenderer>().sprite = medicine.icon;
            player = FindFirstObjectByType<Player>().transform;
            
        }




        void Start()
        {
        
            
        }
        private void Update()
        {
            if (medicine.medicineSO == null){
                Debug.Log("SO nulo!");
                Destroy(gameObject);
            }
            if (Vector3.Distance (transform.position, player.transform.position) < distanceToMove)
            {
                moveTowardsPlayer();
            }
        }
        private void moveTowardsPlayer()
        {
            Vector3 target;
            target = player.transform.position;
            target.y = 2.5f;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < distanceToDestroy)
            {
                
                
                if (medicine != null){
            
                Debug.Log(medicine.medicineName);
                Events.onMedicineCollected.Invoke(medicine);
                GameObject.Destroy(gameObject);
                
                }
                
            }
            
        }
    }