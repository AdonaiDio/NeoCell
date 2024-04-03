using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;

public class ButtonSFX : MonoBehaviour
{
    // Start is called before the first frame update
  void OnEnable(){
    GetComponent<Button>().onClick.AddListener(clickSFX);  
  }
    void OnDisable(){
    GetComponent<Button>().onClick.RemoveListener(clickSFX);  
  }
  public void clickSFX(){
    AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_ui_menuquick_click, transform.position);
  }
}
