using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject hotbarUI;
    [SerializeField] private GameObject hotbarUIButton;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject gamePauseMenu;
    private bool _isInventoryActive = false;
    private bool _isPauseActive = false;
    void Start()
    {
        hotbarUI.SetActive(true);
        inGameMenu.SetActive(true);
        inventoryMenu.SetActive(false);
        gamePauseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        Events.onBossDeath.AddListener(winGame);
        Events.onPlayerDeath.AddListener(loseGame);
    }
    private void OnDisable()
    {
        Events.onBossDeath.RemoveListener(winGame);
        Events.onPlayerDeath.RemoveListener(loseGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //Debug.Log("Apertou o botão!");
            if (!_isInventoryActive && !_isPauseActive)
            {
                ShowInventoryMenu();
            }
            else
            {
                HideInventoryMenu();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Apertou o botão!");
            if (!_isInventoryActive && !_isPauseActive)
            {
                ShowPauseMenu();
            }
            else
            {
                if (_isPauseActive)
                HidePauseMenu();
                if (_isInventoryActive)
                HideInventoryMenu();
            }
        }

        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    Debug.Log("Apertou o botão!");
        //    HideOrShowInventory();
        //}
    }
    public void HideOrShowInventory()
    {
        if (!_isInventoryActive)
            ShowInventoryMenu();
        else
            HideInventoryMenu();
    }
    public void ShowInventoryMenu()
    {
        _isInventoryActive = true;
        inventoryMenu.SetActive(_isInventoryActive);
        Time.timeScale = 0f;
        hotbarUIButton.GetComponent<ToggleIcon>().ToggleImages();
        
    }
    public void HideInventoryMenu()
    {
        
        _isInventoryActive = false;
        Time.timeScale = 1f;
        inventoryMenu.SetActive(_isInventoryActive);
        hotbarUIButton.GetComponent<ToggleIcon>().ToggleImages();
        
    }
    void ShowPauseMenu()
    {
        _isPauseActive = true;
        gamePauseMenu.SetActive(_isPauseActive);
        Time.timeScale = 0f;
        //Debug.Log("Menu aberto");
        hotbarUIButton.GetComponent<ToggleIcon>().ToggleImages();
    }
    void HidePauseMenu()
    {
        _isPauseActive = false;        
        gamePauseMenu.SetActive(_isPauseActive);
        Time.timeScale = 1f;
        //Debug.Log("Menu fechado");
        hotbarUIButton.GetComponent<ToggleIcon>().ToggleImages();
    }
    void winGame()
    {
        SceneManager.LoadSceneAsync(2);
        Events.onSceneChange.Invoke(2);
    }
    void loseGame()
    {
        SceneManager.LoadSceneAsync(3);
        Events.onSceneChange.Invoke(3);
    }
    public void returnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Events.onSceneChange.Invoke(0);
    }

    public void reloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;

    }


}


