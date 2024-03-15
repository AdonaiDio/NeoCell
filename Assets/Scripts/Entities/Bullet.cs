using UnityEngine;
using System;
using System.Collections;



    public class Bullet : MonoBehaviour
    {
        [SerializeField] public LayerMask collisionMask; //enemy collision mask
        private float speed = 0;//going to receive speed from weapon 
       

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;//receive speed from weapon
        }

        void OnEnable()
        {
        StartCoroutine(destroyProjectile());
        }

        void Update()
        {

            transform.Translate(Vector3.forward * speed * Time.deltaTime);


            CheckCollisions();
        }

        void CheckCollisions()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, speed * Time.deltaTime, collisionMask, QueryTriggerInteraction.Collide))
            {
                
                OnHitObject(hit);
                
            }
        }

        void OnHitObject(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Enemy>())
            {
                hit.collider.GetComponent<Enemy>().LoseHP(); 
            }

           
            Destroy(gameObject);
        }
        private IEnumerator destroyProjectile(){
        yield return new WaitForSeconds(5);
        Bullet.Destroy(this.gameObject);
    }
    }
