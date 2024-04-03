using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionMenu;
    private void Awake(){
        optionMenu.SetActive(false);
    }
    public void PlayGame()
    {
        
        SceneManager.LoadSceneAsync(1);
        Events.onSceneChange.Invoke(1);
        
    }
    public void OpenOptions(){
        optionMenu.SetActive(true);
    }
    public void closeOptions(){
        optionMenu.SetActive(false);
    }
    public void closeGame(){
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit(); //Close App

            
    }

}
