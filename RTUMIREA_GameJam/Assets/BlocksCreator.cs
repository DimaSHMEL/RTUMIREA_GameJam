using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksCreator : MonoBehaviour
{
    public GameObject tromb;
    public GameObject spawnUp, spawnMiddle, spawnDown;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateWall());
    }
    IEnumerator CreateWall()
    {
        while (true)
        {
            GameObject tromb1 = Instantiate(tromb);
            GameObject tromb2 = Instantiate(tromb);
            tromb1.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            tromb2.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            tromb1.transform.position = spawnDown.transform.position;
            tromb2.transform.position = spawnMiddle.transform.position;
            yield return new WaitForSeconds(10);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
