using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] public float damageNumber;
    //[SerializeField] private List<float> valuesMilestones;
    //[SerializeField] private List<float> fontSizeMilestones;

    [SerializeField] private TMP_Text currentColor;
    [SerializeField] private Color color;
    [SerializeField] private Color criticalColor;

    [SerializeField] private float lifeTime = 1f;
    private float startTime = 0f;
    public float floatingSpeed = 0.1f;
    public bool isCritical = false;
    public float sizeMod = 1f;


    private void Awake()
    {
        currentColor = GetComponent<TMP_Text>();
        floatingSpeed = lifeTime / 25;
    }

    void Start()
    {
        startTime = Time.time;
        gameObject.GetComponent<TextMeshProUGUI>().text = damageNumber.ToString();
        color = new Color(1,1,1,1);
        criticalColor = new Color(0.8431373f, 0.454902f, 0.08235294f, 1);
        currentColor.color = isCritical ? criticalColor : color;
        if (damageNumber >= 20 && damageNumber < 50)
        {
            sizeMod = 1.5f;
        }
        else if (damageNumber >= 50 && damageNumber < 70)
        {
            sizeMod = 2f;
        }
        else if (damageNumber >= 70 && damageNumber < 100)
        {
            sizeMod = 2.5f;
        }
        else if (damageNumber >= 100)
        {
            sizeMod = 3.5f;
        }
    }
    private void Update()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        float elapsedTime = Time.time - startTime;
        if (elapsedTime < lifeTime)
        {
            float t = elapsedTime/lifeTime;
            float a = Mathf.Lerp(1, 0, t);
            currentColor.color = new Color(currentColor.color.r, currentColor.color.g, currentColor.color.b, a);
            transform.position += Vector3.up * floatingSpeed;
            transform.localScale = new Vector3( a * sizeMod,
                                                a * sizeMod,
                                                a * sizeMod);
            return;
        }

        Destroy(gameObject);
    }

}
