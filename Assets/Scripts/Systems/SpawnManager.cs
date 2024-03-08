using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] Virus enemy;
    [SerializeField] Vector3 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] Player player;
    [SerializeField] float minDistance;
    [SerializeField] float maxEnemies;
    List<Virus> enemies = new List<Virus>();    
     private void OnEnable()
    {
        Events.onEnemyDeath.AddListener(removeEnemy);
        
        
    }
    private void OnDisable()
    {
        Events.onEnemyDeath.RemoveListener(removeEnemy);

        
      
    }
    float timer;

    private void Update(){
        timer -= Time.deltaTime;
        if (timer < 0f){
            SpawnEnemy();
            timer = spawnTimer;
        }
    }

    
    private void SpawnEnemy()
    {   
         
        Vector3 position = new Vector3(
            UnityEngine.Random.Range(-spawnArea.x, spawnArea.x),
            0f,
            UnityEngine.Random.Range(-spawnArea.z, spawnArea.z)
            

        );
        float maxDistance = minDistance+10;
        float distance = Vector3.Distance(position, player.transform.position);
       
        if (distance > minDistance && distance < maxDistance && enemies.Count <= maxEnemies){
        Virus newEnemy = Instantiate(enemy);
        enemies.Add(newEnemy);
        newEnemy.transform.position = position;
        }
    }
    private void removeEnemy(Virus removedEnemy){
        enemies.Remove(removedEnemy);
    }
    


}
