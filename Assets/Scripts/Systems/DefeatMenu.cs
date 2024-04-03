using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void restartGame(){
        SceneManager.LoadSceneAsync(1);
    }
    public void returnToMainMenu(){
        SceneManager.LoadSceneAsync(0);
    }
}
