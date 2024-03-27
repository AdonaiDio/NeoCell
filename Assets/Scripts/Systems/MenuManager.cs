using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private InputHandler inputHandler;
    private bool _isActive = false;
    void Start()
    {
        inGameMenu.SetActive(true);
        inventoryMenu.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.InventoryMenuInput)
        {
            Debug.Log("Apertou o botão!");
            if (!_isActive)
            {
                ShowInventoryMenu();

            }
            else
            {
                HideInventoryMenu();
            }
        }
    }


    void OnEnable()
    {

    }


    void ShowInventoryMenu()
    {
        Time.timeScale = 0f;
        _isActive = true;
        inventoryMenu.SetActive(true);

        Debug.Log("Menu aberto");






    }
    void HideInventoryMenu()
    {
        Time.timeScale = 1f;
        _isActive = false;
        inventoryMenu.SetActive(false);

        Debug.Log("Menu fechado");





    }
}

