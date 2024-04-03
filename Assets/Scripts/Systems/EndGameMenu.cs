using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void restartGame(){
        SceneManager.LoadSceneAsync(1);
        Events.onSceneChange.Invoke(1);
    }
    public void returnToMainMenu(){
        SceneManager.LoadSceneAsync(0);
        Events.onSceneChange.Invoke(0);
    }
}
