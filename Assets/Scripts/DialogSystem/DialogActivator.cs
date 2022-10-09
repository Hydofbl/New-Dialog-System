using System;
using UnityEngine;

public class DialogActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogObject dialogObject;
    
    // Event ile bir konuşmanın tekrar tekrar yapılmasını engellemek amacıyla
    // NPC'nin final diyalog'u güncellenir.
    public void UpdateDialogObject(DialogObject dialogObject)
    {
        this.dialogObject = dialogObject;
    }
    
    public void Interact(Player player)
    {
        foreach (DialogResponseEvents responseEvents in GetComponents<DialogResponseEvents>())
        {
            if (responseEvents.DialogObject == dialogObject)
            {
                player.DialogUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        
        player.DialogUI.InitializeSpeakers(dialogObject);
        player.DialogUI.ShowDialog(dialogObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (player.Interactable is DialogActivator dialogActivator && dialogActivator == this)
            {
                player.Interactable = null;
            }
        }
    }
}
