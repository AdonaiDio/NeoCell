using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkillAnimation : MonoBehaviour
{
    public GameObject particleSystem;
    private Transform cur_scale;
    public Vector3 final_scale;
    private float startTime;
    public float duration = 0.8f;
    private float t = 0;
    private void Start()
    {
        cur_scale = GetComponent<Transform>();
        cur_scale.localScale = Vector3.zero;
        startTime = Time.time;
        particleSystem.SetActive(true);
        particleSystem.transform.localScale = final_scale * 2;
        particleSystem.GetComponent<ParticleSystem>().startSize /= final_scale.x * 2;
    }
    private void FixedUpdate()
    {
         t = (Time.time - startTime)/duration;

        cur_scale.localScale = Vector3.Lerp(Vector3.zero, final_scale, t);
        if (t >= 2f)
        {
            Destroy(this.gameObject);
        }
    }

}
