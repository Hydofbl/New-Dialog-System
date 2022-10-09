using System;
using UnityEngine;

public enum Speaker { 
    Left, 
    Right
}

[Serializable]
public struct Line
{
    public Speaker speaker;
    [TextArea] public string text;
}

[CreateAssetMenu(menuName = "Dialog/DialogObject")]
public class DialogObject : ScriptableObject
{
    public Character leftSpeaker;
    public Character RightSpeaker;
    
    [SerializeField] private Line[] dialog;
    [SerializeField] private Response[] responses;

    public Line[] Dialog => dialog;

    public bool HasResponses => Responses != null &&
                                Responses.Length > 0;

    public Response[] Responses => responses;
}
