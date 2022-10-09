using System;
using UnityEngine;

public enum CharacterEnum { 
    Mert, 
    Alois, 
    Manav    
}

// Custom serializable class
[Serializable]
public class Characters
{
    public CharacterEnum name;
}

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public Characters character;
    public Sprite portrait;
}
