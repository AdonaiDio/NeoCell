using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MedicineDrop : MonoBehaviour
{
    //é a instancia no mapa do item dropado
    private Transform player;
    [SerializeField] private RemedySO remedy;
    [SerializeField] private float distanceToDestroy = 3f;
    [SerializeField] private float speed = 15f;


    void Awake()
    {
        player = FindFirstObjectByType<Player>().transform;
        remedy = InventoryManager.Instance.DrawSOFromPool();
        GetComponent<SpriteRenderer>().sprite = remedy._icon;
    }

    private void Update()
    {
        if (remedy == null)
        {
            Debug.LogWarning("remedySO nulo!");
            Destroy(gameObject);
        }
        if (Vector3.Distance(transform.position, player.position) < player.GetComponent<PlayerSkills>().CollectDropAtDistance)
        {
            moveTowardsPlayer();
        }
    }
    private void moveTowardsPlayer()
    {
        Vector3 target = player.position;
        target.y = 2.5f;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < distanceToDestroy)
        {
            if (remedy != null)
            {
                Debug.Log("coletou: "+remedy._name);
                Events.onMedicineCollected.Invoke(remedy);
                Destroy(gameObject);
            }
        }
    }
}