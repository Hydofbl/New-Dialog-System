using System;
using UnityEngine;

[Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogObject dialogObject;

    public string ResponseText => responseText;
    
    public DialogObject DialogObject => dialogObject;
}
