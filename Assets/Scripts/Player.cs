using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField ]private DialogUI dialogUI;
    [SerializeField ]private float moveSpeed = 10f;

    public DialogUI DialogUI => dialogUI;
    
    public IInteractable Interactable { get; set; }

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (dialogUI.IsOpen) return;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        rb.MovePosition(rb.position + input.normalized * (moveSpeed * Time.fixedDeltaTime));

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Question mark controls if Interactable is not null
            Interactable?.Interact(this);
        }
    }
}
