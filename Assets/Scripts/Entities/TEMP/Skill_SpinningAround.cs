using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SpinningAround : MonoBehaviour
{
    // girar entorno do player

    public float speed = 25f;
    private Transform pivot;

    //public List<GameObject> enemies;
    private void Awake()
    {
        pivot = transform.parent;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Virus>())
        {
            Events.onMineHitEnemy.Invoke(this, other.GetComponent<Virus>());
        }
    }
    private void FixedUpdate()
    {
        pivot.localEulerAngles += new Vector3(0,1,0) * Time.deltaTime * speed;
    }
}
