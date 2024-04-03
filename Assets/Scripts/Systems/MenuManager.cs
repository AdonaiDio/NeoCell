using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject hotbarUI;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject gamePauseMenu;
    private bool _isActive = false;
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

        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    Debug.Log("Apertou o botão!");
        //    HideOrShowInventory();
        //}
    }
    public void HideOrShowInventory()
    {
        if (!_isActive)
            ShowInventoryMenu();
        else
            HideInventoryMenu();
    }
    public void ShowInventoryMenu()
    {
        if (_isActive == false)
        {
            _isActive = true;
            inventoryMenu.SetActive(_isActive);
            Time.timeScale = 0f;
        }
    }
    public void HideInventoryMenu()
    {
        if (_isActive == true)
        {
            _isActive = false;
            Time.timeScale = 1f;

            inventoryMenu.SetActive(_isActive);
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


