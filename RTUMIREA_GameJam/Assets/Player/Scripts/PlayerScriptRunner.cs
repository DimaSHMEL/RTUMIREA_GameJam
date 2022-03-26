using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerScriptRunner : MonoBehaviour
{
    //Movement values
    public float speedMovement;
    public float jumpForce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private Vector2 speedXY;
    private SaveLoadSys SLsys;
    //Checkers values
    public LayerMask whatIsGround, whatIsWall;
    public BoxCollider2D groundCheck, wallCheck;
    private bool onGround = false, jumping = false;
    //Control values
    private PlayerInputActions playerInputActions;
    public GameObject scoreText;
    //Score values
    private int collectables = 0;
    public String scenename;
    // Start is called before the first frame update
    void Start()
    {
        SLsys = GameObject.Find("Savings").GetComponent<SaveLoadSys>();
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        

    }
    public void LoadME()
    {
        if (SLsys.LoadGame())
        {
            collectables = SLsys.score;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        speedXY = playerInputActions.Player.Move.ReadValue<Vector2>();
        if (Math.Abs(speedXY.x) > 0.15f)
            rb.velocity = new Vector2(speedXY.x * speedMovement, rb.velocity.y);
        else if(speedXY.x == 0f)
            rb.velocity = new Vector2(0, rb.velocity.y);

    }
    IEnumerator holdJump()
    {
        float jumpPower = 0;
        float numberOfSeconds = 0;
        jumping = true;
        while (playerInputActions.Player.Jump.IsPressed())
        {
            if (jumpPower < 1.2f)
            {
                jumpPower += 0.1f;
                numberOfSeconds += 0.05f;
            }

            yield return new WaitForSeconds(0.1f);
        }
        rb.velocity = new Vector2(speedMovement, jumpPower * jumpForce);

    }
    private void Update()
    {
        if (playerInputActions.Player.Jump.triggered && onGround)
        {
            StartCoroutine(holdJump());
        }
        checkStatuses();

    }
    private void checkStatuses()
    {
        speedXY = playerInputActions.Player.Move.ReadValue<Vector2>();
        onGround = groundCheck.IsTouchingLayers(whatIsGround);
        if (wallCheck.IsTouchingLayers(whatIsWall))
            rb.AddForce(new Vector2(0, -9.8f));
    }
    public void addColect()
    {
        collectables++;
        scoreText.GetComponent<TextMeshProUGUI>().SetText(collectables.ToString());
    }
}
