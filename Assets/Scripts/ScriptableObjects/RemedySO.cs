using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemedySO : ScriptableObject
{
    public string _name;
    [TextAreaAttribute]
    public string _description;
    public string _cost;
}
//public class Remedy_Effect : RemedySO
//{
//    public List<StatusEffectData> _upgradesOS;
//}

[CreateAssetMenu(fileName = "Slowdown", menuName = "RemedySO/Slowdown")]
public class Remedy_Slowdown : RemedySO
{
    public StatusEffectData _effect;
}
[CreateAssetMenu(fileName = "Decay", menuName = "RemedySO/Decay")]
public class Remedy_Decay : RemedySO
{
    public StatusEffectData _effect;
    //[Tooltip("Critical chance in %")]
    //public float _chanceOfDecay;

}
[CreateAssetMenu(fileName = "Explosion", menuName = "RemedySO/Explosion")]
public class Remedy_Explosion : RemedySO
{
    public StatusEffectData _effect;
}
//public class Remedy_Area : RemedySO
//{

//}
[CreateAssetMenu(fileName = "Projectile", menuName = "RemedySO/Projectile")]
public class Remedy_Projectile : RemedySO
{
    public float _projectileThickness;
}
[CreateAssetMenu(fileName = "Area", menuName = "RemedySO/Area")]
public class Remedy_Area : RemedySO
{
    public float _areaRadius;
}
[CreateAssetMenu(fileName = "Mines", menuName = "RemedySO/Mines")]
public class Remedy_Mines : RemedySO
{
    public int _numberOfMines;
    public float _mineRadius;
}
//public class Remedy_Modifier : RemedySO
//{

//}
[CreateAssetMenu(fileName = "Quantity", menuName = "RemedySO/Quantity")]
public class Remedy_Quantity : RemedySO
{
    [Tooltip("Enemies At Once value >= 1.")]
    public int _enemiesAtOnce;
}
[CreateAssetMenu(fileName = "Multiplicator", menuName = "RemedySO/Multiplicator")]
public class Remedy_Multiplicator : RemedySO
{
    public float _damage;
}
[CreateAssetMenu(fileName = "Critical", menuName = "RemedySO/Critical")]
public class Remedy_Critical : RemedySO
{
    [Tooltip("Critical chance in %")]
    public float _criticalChance;
}
