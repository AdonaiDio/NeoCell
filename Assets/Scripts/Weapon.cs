using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using System.Xml.XPath;
using Unity.VisualScripting;

    public class Weapon : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] Transform tip; //ponta da arma
        public Bullet bullet;
        public float msBetweenShots = 100;
        public float tipVelocity = 500;
        float nextShotTime = 0;

        private InputHandler _input;

        public void Shoot()
        {
            if (Time.time > nextShotTime)
            {
                nextShotTime = Time.time + msBetweenShots / 1000;
                Vector3 spawnPosition = tip.position;
                Quaternion spawnRotation = tip.rotation;

                Bullet newBullet = Instantiate(bullet, spawnPosition, spawnRotation) as Bullet;


                newBullet.SetSpeed(tipVelocity);
            }
        }
    }
