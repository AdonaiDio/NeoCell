using System;
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
    
   /*/     private void OnEnable()
    {
        Events.onInventoryKeyPressed.AddListener(ShowInventoryMenu);
        Events.onInventoryKeyPressed.AddListener(HideInventoryMenu);    
    }
    private void OnDisable()
    {
        Events.onInventoryKeyPressed.RemoveListener(ShowInventoryMenu);
        Events.onInventoryKeyPressed.RemoveListener(HideInventoryMenu);
    }
    // Update is called once per frame
    /*/    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
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
   void ShowInventoryMenu()
    {
        if (_isActive == false)
        {
        _isActive = true;
        inventoryMenu.SetActive(_isActive);
        Time.timeScale = 0f;
        

        Debug.Log("Menu aberto");
        }
    }
    void HideInventoryMenu()
    {       
        if (_isActive == true){
        _isActive = false;
        Time.timeScale = 1f;
        
        inventoryMenu.SetActive(_isActive);

        Debug.Log("Menu fechado");
        }
    }
}


