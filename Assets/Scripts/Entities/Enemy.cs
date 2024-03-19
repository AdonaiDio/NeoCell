
using UnityEngine;
using UnityEngine.AI;
using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using Unity.VisualScripting;



public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;
    [SerializeField] protected string enemyName;
    [SerializeField] protected string type;

    [SerializeField] protected float HPMax; //Max HP

    protected float HP; //Used to store current HP
    public HPBarEnemy hpBarEnemy;
    public UnityEngine.UI.Image hpBarImage;

    [SerializeField] protected float damage;
    //private Material material;

    [SerializeField] protected LayerMask collisionMask;
    [SerializeField] protected NavMeshAgent enemyAgent;



    [SerializeField] protected Transform playerTarget;
    [SerializeField] protected float interactDistance;


    [SerializeField] protected float damageCooldown; //Damage cooldown
    [SerializeField] protected float lastDamage; //Store last damage Time.time
    [SerializeField] protected GameObject body;
    [SerializeField] protected DNADrop dnaDrop;


    protected Rigidbody rb;

    public void OnEnable()
    {
        enemyName = enemySO.enemyName;
        type = enemySO.enemyType;
        HPMax = enemySO.health;
        HP = HPMax; //Start with max HP
        damage = enemySO.damage;

        body.GetComponent<Renderer>().material = enemySO.material;

        playerTarget = GameObject.FindWithTag("Cell").transform; //Find Cell target to follow

        rb = GetComponent<Rigidbody>();
        scaleWithType();


    }
    public void Update()
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
        enemyAgent.SetDestination(playerTarget.position);
    }
    public virtual void LoseHP()
    {
        HP--;
        float hpToFillBar = HP / HPMax;
        hpBarImage.fillAmount = hpToFillBar;

        //EventManager.Instance.OnHPLost?.Invoke(this, new OnHPLostEventArgs{
        //hpToFillBar = HP/HPMax //calc hp bar fill 
        //});

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
            if (Time.time - lastDamage < damageCooldown)
            { //Calc damage cooldown

            }
            else
            {
                hit.collider.GetComponent<Cell>().LoseHP(); //Damage player
                lastDamage = Time.time;
            }
        }
    }
    public virtual void Die()
    {
        Vector3 lootSpawnPoint = transform.position;
        lootSpawnPoint.y = dnaDrop.transform.position.y;

        GameObject.Instantiate(dnaDrop, lootSpawnPoint, dnaDrop.transform.rotation);
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        GameObject.Destroy(gameObject);




        Events.onEnemyDeath.Invoke(this);
    }



}

