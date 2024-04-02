using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_LineProjectile : MonoBehaviour
{
    // atirar um projetil. usar para coletar inimigos atingidos

    public float speed = 1f;
    public float lifeTime = 4f;
    private float _spawnTime;

    //public List<GameObject> enemies;
    private void Awake()
    {
        _spawnTime = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            Events.onProjectileHitEnemy.Invoke(this, other.GetComponent<Enemy>());
        }
    }
    private void FixedUpdate()
    {
        transform.position += transform.forward * speed;

        if (Time.time - _spawnTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }

    }

}
