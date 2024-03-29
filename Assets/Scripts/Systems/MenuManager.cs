using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Timers;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject gameClearMenu;
    [SerializeField] private InputHandler inputHandler;
    public float gameTimer;
    public TextMeshProUGUI enemiesDefeatedText;
    public TextMeshProUGUI gameTimerText;
    
    private bool _isActive = false;
    
    void Start()
    {
        inGameMenu.SetActive(true);
        inventoryMenu.SetActive(false);
        gameClearMenu.SetActive(false);
    }
    
        private void OnEnable()
    {
        Events.onBossDeath.AddListener(endGame);
        //Events.onInventoryKeyPressed.AddListener(ShowInventoryMenu);
        //Events.onInventoryKeyPressed.AddListener(HideInventoryMenu);

    }
    private void OnDisable()
    {
        Events.onBossDeath.RemoveListener(endGame);
        //Events.onInventoryKeyPressed.RemoveListener(ShowInventoryMenu);
        //Events.onInventoryKeyPressed.RemoveListener(HideInventoryMenu);
    }
    // Update is called once per frame
        
    void Update()
    {
        gameTimer += Time.deltaTime;
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
    void endGame(){    
        enemiesDefeatedText.text = SpawnManager.Instance.enemiesDefeated.ToString();        
        int minutes = Mathf.FloorToInt(gameTimer / 60);
        int seconds = Mathf.FloorToInt(gameTimer % 60);
        gameTimerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
        inGameMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        gameClearMenu.SetActive(true);
        
        Time.timeScale = 0f;
    }
}


