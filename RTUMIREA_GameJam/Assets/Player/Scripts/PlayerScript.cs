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
    public float maxJumpPower;
    public float timeEntercalCharge;
    public float maxChargeTime;
    public float speedCharge;
    //Checkers values
    public LayerMask whatIsGround, whatIsWall;
    public BoxCollider2D groundCheck, wallCheck;
    private bool onGround = false, jumping = false;
    //Control values
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    public GameObject scoreText;
    public GameObject jumpText;
    //Score values
    private int collectables = 0;
    public String scenename;
    public bool RESET;
    //Add values
    public AudioSource jumpSound;
    // Start is called before the first frame update
    void Start()
    {
        SLsys = GameObject.Find("Savings").GetComponent<SaveLoadSys>();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        if(RESET)
        {
            SLsys.ResetData();
        }
        
        LoadME();
        
    }
    public void LoadME()
    {
        if (SLsys.LoadGame())
        {
            
            collectables = SLsys.score;
            scoreText.GetComponent<TextMeshProUGUI>().SetText(collectables.ToString());
            int numb = SLsys.sceneNumber;
            if(SLsys.playerPosX.Count == 0)
            {
                List<List<String>> temp1 = new List<List<string>>();
                SLsys.sceneNumber = 0;
                numb = SLsys.sceneNumber;
                SLsys.itemsInScene = temp1;
            }
            if (numb == SLsys.playerPosX.Count)
            {
                SLsys.playerPosX.Add(gameObject.transform.position.x);
                SLsys.playerPosY.Add(gameObject.transform.position.y);
                List<String> temp = new List<String>();
                SLsys.itemsInScene.Add(temp);
                SLsys.SaveGame();
            }
            else
            {
                gameObject.transform.position = new Vector2(SLsys.playerPosX[SLsys.sceneNumber], SLsys.playerPosY[SLsys.sceneNumber]);
                for(int i = 0; i < SLsys.itemsInScene[SLsys.sceneNumber].Count; i++)
                {
                    if(GameObject.Find(SLsys.itemsInScene[SLsys.sceneNumber][i]))
                    {
                        Destroy(GameObject.Find(SLsys.itemsInScene[SLsys.sceneNumber][i]));
                    }
                }
            }
            
        }
        else
        {
            List<List<String>> temp1 = new List<List<string>>();
            SLsys.itemsInScene = temp1;
            List<String> temp = new List<String>();
            SLsys.itemsInScene.Add(temp);
            SLsys.playerPosX.Add(gameObject.transform.position.x);
            SLsys.playerPosY.Add(gameObject.transform.position.y);
            SLsys.sceneNumber = 0;
            SLsys.sceneName = scenename;
            SLsys.SaveGame();
        }
    }
    public void SaveME()
    {
        SLsys.playerPosX[SLsys.sceneNumber] = (gameObject.transform.position.x);
        SLsys.playerPosY[SLsys.sceneNumber] = (gameObject.transform.position.y);
        SLsys.score = collectables;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        speedXY = playerInputActions.Player.Move.ReadValue<Vector2>();
        if(Math.Abs(speedXY.x) > 0.15f && onGround && !jumping)
            rb.velocity = new Vector2(speedXY.x *  speedMovement, rb.velocity.y);
        else if(Math.Abs(speedXY.x) == 0.0f && rb.velocity.y == 0.0f && !jumping)
            rb.velocity = new Vector2(0, rb.velocity.y);

        SaveME();
        SLsys.SaveGame();
    }
    IEnumerator holdJump()
    {
        float jumpPower = 0;
        float numberOfSeconds = 0;
        jumping = true;
        
        while (playerInputActions.Player.Jump.IsPressed())
        {
            if (jumpPower < maxJumpPower)
            {
                jumpPower += speedCharge;
                numberOfSeconds += timeEntercalCharge;
            }
            jumpText.GetComponent<TextMeshProUGUI>().SetText((jumpPower * jumpForce).ToString());
            yield return new WaitForSeconds(timeEntercalCharge);
        }
        if (onGround)
        {
            rb.velocity = new Vector2(Math.Sign(speedXY.x) * speedMovement, jumpPower * jumpForce); //для того чтобы просто вверх прыгнуть
            jumpSound.Play();
        }
        jumping = false;
        jumpText.GetComponent<TextMeshProUGUI>().SetText((0).ToString());


    }
    private void Update()
    {
        if (speedXY.x == 0 && playerInputActions.Player.Jump.triggered && onGround)
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
