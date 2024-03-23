using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineDrop : MonoBehaviour
{
    private Transform player;
    public Medicine medicine;
    [SerializeField] private float distanceToMove;
    [SerializeField] private float distanceToDestroy;
    
    [SerializeField] private float speed;
    void Start()
    {
        player = FindFirstObjectByType<Player>().transform;
        
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToMove)
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
           
            
            Events.onMedicineCollected.Invoke(medicine);
            GameObject.Destroy(gameObject);
            
            }
            
        }
        
    }
}