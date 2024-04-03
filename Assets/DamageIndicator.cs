using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] public float damageNumber;
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = damageNumber.ToString();
        gameObject.GetComponent<Animator>().Play("damage indicator");
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
