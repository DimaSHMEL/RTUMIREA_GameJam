using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().addColect();
            SaveLoadSys SAVE = GameObject.Find("Savings").GetComponent<SaveLoadSys>();
            SAVE.itemsInScene[SAVE.sceneNumber].Add(gameObject.name);
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(Floatint());
    }
    IEnumerator Floatint()
    {
        while(true)
        {
            rb.velocity = new Vector2(0, 0.2f);
            yield return new WaitForSeconds(0.5f);
            rb.velocity = new Vector2(0, -0.2f);
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
