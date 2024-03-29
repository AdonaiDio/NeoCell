using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    //Esse script vai inflar o gameobject da explosão
    //e detectar quem entrou em contato com ela.
    //caso seja um inimigo ela vai causar dano a ele.
    //o tamanho da explosão e definido pelo inimigo que explodiu.
    //[SerializeField]//temp
    private float growSpeed = 1.5f;
    private float initialSize = 0.1f;
    private float initialTime;
    [HideInInspector]
    public float finalSize = 10f;
    private float curSize = 0;
    [HideInInspector]
    public float damage = 1f;
    private bool atMaxSize;
    private void Start()
    {
        initialTime = Time.time;
        atMaxSize = false;
    }
    private void FixedUpdate()
    {
        if (!atMaxSize)
        {
            curSize = Mathf.Lerp(initialSize, finalSize, growSpeed * (Time.time - initialTime));
            transform.localScale = new Vector3(curSize, curSize, curSize);
            if (curSize == finalSize)
            {
                atMaxSize = true;
                StartCoroutine(WaitToDestroy());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            List<StatusEffectData> _effects = new List<StatusEffectData>();
            Events.onDamageEnemy.Invoke(other.GetComponent<Enemy>(), damage, _effects);

        }
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
