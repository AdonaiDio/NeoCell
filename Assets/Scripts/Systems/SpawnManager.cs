using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    [SerializeField] Vector3 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] Cell player;
    [SerializeField] float minDistance;
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
        
        float distance = Vector3.Distance(position, player.transform.position);
        
        if (distance > minDistance){
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        }
    }


}
