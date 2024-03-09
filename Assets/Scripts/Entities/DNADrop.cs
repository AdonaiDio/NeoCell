using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNADrop : MonoBehaviour
{
   private Transform player;
    [SerializeField] private float distanceToMove;
    [SerializeField] private float distanceToDestroy;
    [SerializeField] private float dnaValue = 1;
    [SerializeField] private float speed;
   void Awake(){
   player = GameObject.FindWithTag("Cell").transform;
   }
   private void Update(){
    if (Vector3.Distance(transform.position, player.transform.position) < distanceToMove){
        moveTowardsPlayer();
    }
   }
   private void moveTowardsPlayer(){
    Vector3 target;
    target = player.transform.position;
    target.y = transform.position.y;
    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    
     if (Vector3.Distance (transform.position, player.transform.position) < distanceToDestroy){
        GameObject.Destroy (gameObject);
        Events.onDNAGained.Invoke(dnaValue);
   }




}
}