using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Default", menuName = "RemedySO/Default")]
public class RemedySO : ScriptableObject
{
    public Sprite _icon; 
    public string _name;
    [TextAreaAttribute]
    public string _description;
    [TextAreaAttribute]
    public string _nextDescription;
    public int _cost;
    public string _ID;
}