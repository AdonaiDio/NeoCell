using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DetectionTrigger : MonoBehaviour
{
    public List<GameObject> enemies;

    private void CheckForNullOnList()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null) {
                enemies.RemoveAt(i);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Virus>())
        {
            CheckForNullOnList();
            enemies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Virus>())
        {
            CheckForNullOnList();
            enemies.Remove(other.gameObject);
        }
    }

}
