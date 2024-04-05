using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Coin SFX")]
    [field: SerializeField] public EventReference ambience_gameplay { get; private set; }
    [field: SerializeField] public EventReference ambience_menu { get; private set; }
    [field: SerializeField] public EventReference music_gameplay { get; private set; }
    [field: SerializeField] public EventReference tela_de_morte { get; private set; }
    [field: SerializeField] public EventReference tela_de_vitoria { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_atack { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_atack_area { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_atack_critico { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_atack_explose { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_atack_mina { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_atack_slash { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_char_damage { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_char_die { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_char_powerup_1 { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_char_powerup_2 { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_char_step { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_enemy_spawn { get; private set; }
    [field: SerializeField] public EventReference sfx_gameplay_powerup_conquista { get; private set; }
    [field: SerializeField] public EventReference sfx_ui_menumain_click { get; private set; }
    [field: SerializeField] public EventReference sfx_ui_menumain_click_startgame { get; private set; }
    [field: SerializeField] public EventReference sfx_ui_menumain_hover { get; private set; }
    [field: SerializeField] public EventReference sfx_ui_menuquick_click { get; private set; }
    [field: SerializeField] public EventReference sfx_ui_menuquick_hover { get; private set; }


    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
