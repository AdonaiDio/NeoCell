using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using System.Xml.XPath;
using Unity.VisualScripting;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform tip; //bullet spawn point
    public Bullet bullet; //projectile prefab
    public float msBetweenShots = 100; //cooldown between shots
    public float tipVelocity = 500; //speed of the projectile
    float nextShotTime = 0; //calc next shot time based on cooldown



    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000; //calc next shot
            Vector3 spawnPosition = tip.position; //bullet spawn position
            Quaternion spawnRotation = tip.rotation; //bullet spawn rotation

            Bullet newBullet = Instantiate(bullet, spawnPosition, spawnRotation) as Bullet;


            newBullet.SetSpeed(tipVelocity);//set bullet speed
        }
    }
}
