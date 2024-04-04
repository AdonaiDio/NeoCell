using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleIcon : MonoBehaviour
{
    public Sprite icon1;
    public Sprite icon2;
    private Image cur_icon;

    private void Start()
    {
        cur_icon = GetComponent<Image>();
    }
    public void OnHover()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_ui_menuquick_hover, transform.position);
    }
    public void ToggleImages() {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_ui_menuquick_click, transform.position);
        if (icon1 == cur_icon.sprite)
        {
            cur_icon.sprite = icon2;
        }
        else
        {
            cur_icon.sprite = icon1;
        }
    }
}
