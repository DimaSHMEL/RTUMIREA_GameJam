using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 speedXY = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        rb.velocity = new Vector2(speedXY.x, rb.velocity.y);
    }
    private void Update()
    {
        if (playerInputActions.Player.Jump.triggered)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            Debug.Log("jump");
        }
    }
}
