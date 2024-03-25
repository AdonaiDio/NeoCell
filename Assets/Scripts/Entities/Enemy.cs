
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using Unity.VisualScripting;



public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;
    [SerializeField] protected string enemyName;
    [SerializeField] protected string type;

    [SerializeField] protected float HPMax; //Max HP
    [SerializeField] protected float HP; //Used to store current HP
    private HPBarEnemy hpBarEnemy;
    [SerializeField] protected float damage;

    protected LayerMask collisionMask;
    protected Transform playerTarget;
    [SerializeField] protected float interactDistance;


    [SerializeField] protected float damageCooldown; //Damage cooldown
    protected float LastDamageTime; //Store last damage Time.time
    protected GameObject body;
    [SerializeField] protected GameObject dnaDrop;

    private List<StatusEffectData> _effects;//recebe os efeitos do golpe do player

    protected Rigidbody rb;
    public virtual void Awake()
    {
        playerTarget = FindFirstObjectByType<Player>().transform; //Find player target to follow
        body = transform.Find("Body").gameObject;
        collisionMask = (1 << 3); //player layer is 3
        rb = GetComponent<Rigidbody>();
        if (this.GetType() != typeof(BossEnemy))
        {
            hpBarEnemy = transform.Find("HPBarUI").GetComponent<HPBarEnemy>();
        }
    }

    public virtual void Start()
    {
        enemyName = enemySO.enemyName;
        type = enemySO.enemyType;
        HPMax = enemySO.health;
        HP = HPMax; //Start with max HP
        damage = enemySO.damage;

        body.GetComponent<Renderer>().material = enemySO.material;

        scaleWithType();
    }

    private void Update()
    {
        ChasePlayer(); //follow player
        CheckCollisions(interactDistance);//check collisions to do damage
                                          //hpBarEnemy.transform.rotation = Quaternion.identity;
        if (HP <= 0) //Die
            Die();
    }
    public void scaleWithType()
    {
        if (type == "Strong")
        {
            transform.localScale += new Vector3(1, 1, 1);
        }
        if (type == "Boss")
        {
            transform.localScale += new Vector3(5, 5, 5);
        }
    }
    public void ChasePlayer()
    {
        GetComponent<NavMeshAgent>().SetDestination(playerTarget.position);
    }
    public virtual void LoseHP(float damage = 1)
    {
        HP -= damage;
        float hpToFillBar = HP / HPMax;
        hpBarEnemy.barImage.fillAmount = hpToFillBar;
    }

    public void CheckCollisions(float interactDistance)
    {

        Ray ray = new Ray(body.transform.position, body.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    public void OnHitObject(RaycastHit hit)
    {
        if (hit.collider.GetComponent<Cell>())
        {
            if (Time.time - LastDamageTime < damageCooldown)
            { //Calc damage cooldown

            }
            else
            {
                hit.collider.GetComponent<Cell>().LoseHP(); //Damage player
                LastDamageTime = Time.time;
            }
        }
    }

    public virtual void Die()
    {
        Vector3 lootSpawnPoint = transform.position;
        lootSpawnPoint.y = 1f;
        //lootSpawnPoint.y = dnaDrop.transform.position.y;

        Instantiate(dnaDrop, lootSpawnPoint, dnaDrop.transform.rotation);

        Destroy(gameObject);

        Events.onEnemyDeath.Invoke(this);
    }



}

