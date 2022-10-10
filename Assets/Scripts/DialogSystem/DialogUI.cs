using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[Serializable]
public struct SpeakerComps
{
    public GameObject GO;
    public Image image;
    public TMP_Text name;
}

public class DialogUI : MonoBehaviour
{
    public bool isMonolog = false;

    [SerializeField] private GameObject dialogBox;
    [SerializeField] private SpeakerComps leftSpeaker;
    [SerializeField] private SpeakerComps rightSpeaker;
    [SerializeField] private TMP_Text textLabel;

    public bool IsOpen { get; private set; }
    
    private ResponseHandler responseHandler;
    private TypeWriterEffect typeWriterEffect;
    
    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
    }
    
    public void ShowDialog(DialogObject dialogObject)
    {
        IsOpen = true;
        
        dialogBox.SetActive(true);
        StartCoroutine(StepThroughDialog(dialogObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }
    
    private IEnumerator StepThroughDialog(DialogObject dialogObject)
    {
        for (int i = 0; i < dialogObject.Dialog.Length; i++)
        {
            if (!isMonolog)
            {
                ChangeSpeaker(dialogObject.Dialog[i]);
            }
            else
            {
                if(!leftSpeaker.GO.activeSelf)
                    leftSpeaker.GO.SetActive(true);
            }
            
            string dialog = dialogObject.Dialog[i].text;

            yield return RunTypingEffect(dialog);

            textLabel.text = dialog;
            
            if (i == dialogObject.Dialog.Length - 1 && dialogObject.HasResponses) 
                break;

            // wait for 1 more frame
            yield return null;
            // Space basılana kadar bekle.
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogObject.Responses);
        }
        else
        {
            CloseSpeaker(leftSpeaker.GO);
            if (!isMonolog)
                CloseSpeaker(rightSpeaker.GO);
            CloseDialogBox();
        }
    }

    private void ChangeSpeaker(Line line)
    {
        if (line.speaker == Speaker.Left)
        {
            leftSpeaker.GO.SetActive(true);

            if (rightSpeaker.GO.activeSelf)
                rightSpeaker.GO.SetActive(false);
        }
        else
        {
            rightSpeaker.GO.SetActive(true);

            if (leftSpeaker.GO.activeSelf)
                leftSpeaker.GO.SetActive(false);
        }
    }

    private IEnumerator RunTypingEffect(string dialog)
    {
        typeWriterEffect.Run(dialog, textLabel);

        while (typeWriterEffect.isRunning)
        {
            // wait for 1 more frame
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typeWriterEffect.Stop();
            }
        }
    }
    
    public void CloseDialogBox()
    {
        IsOpen = false;
        dialogBox.SetActive(false);
        textLabel.text = String.Empty;
    }
    
    private void CloseSpeaker(GameObject speaker)
    {
        speaker.SetActive(false);
    }
    
    public void InitializeSpeakers(DialogObject dialogObject)
    {
        leftSpeaker.image.overrideSprite = dialogObject.leftSpeaker.portrait;
        leftSpeaker.name.text = dialogObject.leftSpeaker.name;

        if (!isMonolog)
        {
            rightSpeaker.image.overrideSprite = dialogObject.RightSpeaker.portrait;
            rightSpeaker.name.text = dialogObject.RightSpeaker.name;
        }
    }
}
