using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
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
    private PlayerInput playerInput;
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
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        if (SLsys.LoadGame())
        {
            gameObject.transform.position = new Vector2(SLsys.playerPosX[SLsys.sceneNumber], SLsys.playerPosY[SLsys.sceneNumber]);
        }
        else
        {
            if(SceneManager.GetActiveScene().name == scenename)
            {
                SLsys.playerPosX.Add(gameObject.transform.position.x);
                SLsys.playerPosY.Add(gameObject.transform.position.y);
                SLsys.sceneNumber = 0;
            }
            SLsys.SaveGame();
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        speedXY = playerInputActions.Player.Move.ReadValue<Vector2>();
        if(Math.Abs(speedXY.x) > 0.15f && onGround /*&& !jumping*/)
            rb.velocity = new Vector2(speedXY.x *  speedMovement, rb.velocity.y);
        else if(Math.Abs(speedXY.x) == 0.0f && rb.velocity.y == 0.0f /*&& !jumping*/)
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
                numberOfSeconds += 0.1f;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
        if(facingRight)
            rb.velocity = new Vector2(/*Math.Sign(speedXY.x) */ speedMovement, jumpPower * jumpForce); //для того чтобы просто вверх прыгнуть
        else
            rb.velocity = new Vector2(-speedMovement, jumpPower * jumpForce);

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
        if (speedXY.x < 0f && facingRight)
            flip();
        else if (speedXY.x > 0f && !facingRight)
            flip();
        if (wallCheck.IsTouchingLayers(whatIsWall))
            rb.AddForce(new Vector2(0, -9.8f));
    }
    private void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void addColect()
    {
        collectables++;
        scoreText.GetComponent<TextMeshProUGUI>().SetText(collectables.ToString());
    }
}
