
using UnityEngine;
using UnityEngine.AI;
using System;
//using Microsoft.Unity.VisualStudio.Editor;
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
<<<<<<< HEAD
=======
    protected float speed = 3f;
>>>>>>> Adonai

    protected LayerMask collisionMask;
    protected Transform playerTarget;


    [SerializeField] protected float damageCooldown; //Damage cooldown
    protected float LastDamageTime; //Store last damage Time.time
    protected GameObject body;
    [SerializeField] protected GameObject dnaDrop;
    [SerializeField] protected GameObject medicineDrop;
<<<<<<< HEAD
    public int medicineDropRate;
=======
    private float medicineDropRate = 99f;///temp
>>>>>>> Adonai


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

        //body.GetComponent<Renderer>().material = enemySO.material;

        scaleWithType();
    }

    private void Update()
    {
        ChasePlayer(); //follow player
        //CheckCollisions(interactDistance);//check collisions to do damage
                                          //hpBarEnemy.transform.rotation = Quaternion.identity;
<<<<<<< HEAD
        if (HP <= 0) //Die
            Die();
=======
>>>>>>> Adonai
    }
    public void scaleWithType()
    {
        if (type == "Strong")
        {
            float size = UnityEngine.Random.Range(1f,2f);
            transform.localScale += new Vector3(size, size, size);
        }
    }
    public void ChasePlayer()
    {
<<<<<<< HEAD
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
=======
        //persegue jogador enquanto a distancia dele para o jogador for
        //maior que a soma do tamanho de ambos colliders
        float desiredDistance = playerTarget.GetComponent<CapsuleCollider>().radius 
            * playerTarget.transform.localScale.x 
            + (GetComponent<CapsuleCollider>().radius * transform.localScale.x);

        if (Vector3.Distance(transform.position, playerTarget.position) > desiredDistance) {
            GetComponent<NavMeshAgent>().SetDestination(playerTarget.position);
        }
        else {
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
>>>>>>> Adonai
        }
    }

    //public void CheckCollisions(float interactDistance)
    //{
      
    //    Ray ray = new Ray(body.transform.position, body.transform.forward);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit, interactDistance, collisionMask, QueryTriggerInteraction.Collide))
    //    {
    //        OnHitObject(hit);
    //    }
    //}
    void OnTriggerStay(Collider col)
    {
        if(col.GetComponent<Player>())
        {
            if (Time.time - LastDamageTime > damageCooldown) { 
                col.GetComponent<Player>().LoseHP(damage); //Damage player
                LastDamageTime = Time.time;
            }
        }
    }
<<<<<<< HEAD
=======

    //public void OnHitObject(Collider hit)
    //{
    //    if (hit.collider.GetComponent<Player>())
    //    {
    //        if (Time.time - LastDamageTime < damageCooldown)
    //        { //Calc damage cooldown

    //        }
    //        else
    //        {
    //            hit.collider.GetComponent<Player>().LoseHP(damage); //Damage player
    //            LastDamageTime = Time.time;
    //        }
    //    }
    //}
    public virtual void LoseHP(float damage = 1)
    {
        HP -= damage;
        float hpToFillBar = HP / HPMax;
        hpBarEnemy.barImage.fillAmount = hpToFillBar;
>>>>>>> Adonai

    public virtual void Die()
    {
        Vector3 lootSpawnPoint = transform.position;
        lootSpawnPoint.y = 1f;
        //lootSpawnPoint.y = dnaDrop.transform.position.y;

        Instantiate(dnaDrop, lootSpawnPoint, dnaDrop.transform.rotation);
<<<<<<< HEAD
        medicineDropRate = UnityEngine.Random.Range(0, 100);
        if (medicineDropRate <= 50)
=======

        if (medicineDropRate >= UnityEngine.Random.Range(1f,100f) && medicineDropRate != 0)
>>>>>>> Adonai
        {
            Instantiate(medicineDrop, lootSpawnPoint, medicineDrop.transform.rotation);
        }
        Destroy(gameObject);

        Events.onEnemyDeath.Invoke(this);
    }



}

