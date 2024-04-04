using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingColectedMedicine : MonoBehaviour
{

    [SerializeField] private GameObject popUpIcon;

    public float lifetime;

    private void Start()
    {
        lifetime = 2f;
    }

    private void OnEnable()
    {
        Events.onMedicineCollected.AddListener(PingPopUp);
    }
    private void OnDisable()
    {
        Events.onMedicineCollected.RemoveListener(PingPopUp);
    }

    void PingPopUp(RemedySO r)
    {
        StartCoroutine(WaitPopUp());
    }

    private IEnumerator WaitPopUp()
    {

        popUpIcon.SetActive(true);

        yield return new WaitForSeconds(lifetime);

        popUpIcon.SetActive(false);
    }
}
