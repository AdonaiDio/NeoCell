using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FMOD.Studio;

public class StartGameButtonSFX : MonoBehaviour
{
    // Start is called before the first frame update
void OnEnable(){
    GetComponent<Button>().onClick.AddListener(clickSFX);  
  }
    void OnDisable(){
    GetComponent<Button>().onClick.RemoveListener(clickSFX);  
  }
  public void clickSFX(){
    AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_ui_menumain_click_startgame, transform.position);
  }
  public void hoverSFX(){
    AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_ui_menumain_hover, transform.position);
  }
}
