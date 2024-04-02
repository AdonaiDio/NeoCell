using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Timers;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject gamePauseMenu;
    [SerializeField] private InputHandler inputHandler;
    public float gameTimer;
    public TextMeshProUGUI enemiesDefeatedText;
    public TextMeshProUGUI gameTimerText;
    
    private bool _isActive = false;
    
    
    void Start()
    {
        inGameMenu.SetActive(true);
        inventoryMenu.SetActive(false);
        gamePauseMenu.SetActive(false);
        
    }
    
        private void OnEnable()
    {
        Events.onBossDeath.AddListener(winGame);
        Events.onPlayerDeath.AddListener(loseGame);

        //Events.onInventoryKeyPressed.AddListener(ShowInventoryMenu);
        //Events.onInventoryKeyPressed.AddListener(HideInventoryMenu);

    }
    private void OnDisable()
    {
        Events.onBossDeath.RemoveListener(winGame);
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
                ShowPauseMenu(inventoryMenu);
            }
            else
            {
                HidePauseMenu(inventoryMenu);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Apertou o botão!");
            if (!_isActive)
            {
                ShowPauseMenu(gamePauseMenu);
            }
            else
            {
                HidePauseMenu(gamePauseMenu);
            }
        }
        
    }
   void ShowPauseMenu(GameObject menu)
    {
        
        _isActive = true;
        menu.SetActive(_isActive);
        Time.timeScale = 0f; 
        Debug.Log("Menu aberto");
        
    }
    void HidePauseMenu(GameObject menu)
    {       
       
        _isActive = false;
        Time.timeScale = 1f;        
        menu.SetActive(_isActive);
        Debug.Log("Menu fechado");        
    }
    void winGame(){    
        SceneManager.LoadSceneAsync(2);
    }
    void loseGame(){    
        SceneManager.LoadSceneAsync(3);
    }
    public void returnToMainMenu(){
        SceneManager.LoadSceneAsync(0);
    }
    
    public void reloadScene(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
        
    }
}


