using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status Effect")]
public class StatusEffectData : ScriptableObject
{
    public string Name;
    public StatusEffectType Type;
    public float Amount;
    public float TickSpeed;
    public float Lifetime;
}

public enum StatusEffectType
{
    None,
    Speed,
    DamageOverTime,
    Explosion,
    Heal
}
