using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform pos;
    public float gap;
    public BoxCollider2D Up, down, left, right;
    public bool RESET;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Vector2 speed = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            if(Up.IsTouching(collision.gameObject.GetComponent<Collider2D>()) && speed.y > 0)
            {
                pos.position = new Vector3(pos.position.x, pos.position.y + gap * 2, pos.position.z);
            }
            else if (down.IsTouching(collision.gameObject.GetComponent<Collider2D>()) && speed.y < 0)
            {
                pos.position = new Vector3(pos.position.x, pos.position.y - gap * 2, pos.position.z);
            }
            else if(left.IsTouching(collision.gameObject.GetComponent<Collider2D>()) && speed.x < 0)
            {
                pos.position = new Vector3(pos.position.x - gap * 4, pos.position.y , pos.position.z);
            }
            else if(right.IsTouching(collision.gameObject.GetComponent<Collider2D>()) && speed.x > 0)
            {
                pos.position = new Vector3(pos.position.x + gap * 4, pos.position.y, pos.position.z);
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector2 speed = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            if (Up.IsTouching(collision.gameObject.GetComponent<Collider2D>()) && speed.y > 0)
            {
                pos.position = new Vector3(pos.position.x, pos.position.y + gap * 2, pos.position.z);
            }
            else if (down.IsTouching(collision.gameObject.GetComponent<Collider2D>()) && speed.y < 0)
            {
                pos.position = new Vector3(pos.position.x, pos.position.y - gap * 2, pos.position.z);
            }
            else if (left.IsTouching(collision.gameObject.GetComponent<Collider2D>()) && speed.x < 0)
            {
                pos.position = new Vector3(pos.position.x - gap * 4, pos.position.y, pos.position.z);
            }
            else if (right.IsTouching(collision.gameObject.GetComponent<Collider2D>()) && speed.x > 0)
            {
                pos.position = new Vector3(pos.position.x + gap * 4, pos.position.y, pos.position.z);
            }
            Debug.Log(collision.tag);
        }
    }
    void Start()
    {
        pos = GetComponent<Transform>();
        SaveLoadSys SAVE = GameObject.Find("Savings").GetComponent<SaveLoadSys>();
        if(RESET)
        {
            SAVE.ResetData();
        }
        if (SAVE.LoadGame())
        {
            int numb;
            if (SAVE.cameraPosX.Count == 0)
            {
                SAVE.cameraPosX = new List<float>();
                SAVE.cameraPosY = new List<float>();
                SAVE.sceneNumber = 0;
            }
            numb = SAVE.sceneNumber;
            if (numb == SAVE.cameraPosX.Count)
            {
                SAVE.cameraPosX.Add(gameObject.transform.position.x);
                SAVE.cameraPosY.Add(gameObject.transform.position.y);
                SAVE.SaveGame();
            }
            else
            {
                gameObject.transform.position = new Vector3(SAVE.cameraPosX[SAVE.sceneNumber], SAVE.cameraPosY[SAVE.sceneNumber], -10);
            }

        }
        else
        {
            SAVE.cameraPosX = new List<float>();
            SAVE.cameraPosY = new List<float>();
            SAVE.sceneNumber = 0;
            SAVE.cameraPosX.Add(gameObject.transform.position.x);
            SAVE.cameraPosY.Add(gameObject.transform.position.y);
            SAVE.SaveGame();
        }
    }
    void SAVEME()
    {
        SaveLoadSys SAVE = GameObject.Find("Savings").GetComponent<SaveLoadSys>();
        SAVE.cameraPosX[SAVE.sceneNumber] = (gameObject.transform.position.x);
        SAVE.cameraPosY[SAVE.sceneNumber]= (gameObject.transform.position.y);
        SAVE.SaveGame();
    }
    // Update is called once per frame
    void Update()
    {
        SAVEME();
    }
}
