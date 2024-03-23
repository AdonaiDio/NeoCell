using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private InputHandler InputHandler;

    private bool isActive = false;
    void Awake()
    {
        inventoryMenu.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
             
        /*/if (InputHandler.InventoryMenuInput){
            Debug.Log("Apertou o botão!");
            if(isActive == false){
                ShowInventoryMenu();
            }
            if(isActive == true){
                HideInventoryMenu();
            }
        }
        /*/
    }
    void ShowInventoryMenu(){
        
        inventoryMenu.SetActive(true);
        Time.timeScale = 0f;
        isActive = true;
        
    }
        void HideInventoryMenu(){
        
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        isActive = false;
    }
}
