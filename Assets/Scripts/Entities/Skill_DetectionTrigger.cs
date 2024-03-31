using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DetectionTrigger : MonoBehaviour
{
    public List<GameObject> enemies;
    private float _lastTick = 0f;
    private float _cooldown = 0.5f;

    private void FixedUpdate()
    {
        //a cada X tempos DoAction para que o jogador execute todos os danos e spawns nas áreas detectáveis.
        //
        WaitForTick();
    }

    private void WaitForTick()
    {
        if (Time.time - _lastTick < _cooldown)
        {
            return;
        }
        _lastTick = Time.time;
        CheckForNullOnList();
    }
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
        if (other.GetComponent<Enemy>())
        {
            CheckForNullOnList();
            enemies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            CheckForNullOnList();
            enemies.Remove(other.gameObject);
        }
    }

}
