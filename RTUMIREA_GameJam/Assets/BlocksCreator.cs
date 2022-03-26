using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BlocksCreator : MonoBehaviour
{
    public GameObject tromb;
    public List<GameObject> spawnPoints;
    private List<GameObject> trombs = new List<GameObject>();
    public int minTrombs, maxTrombs;
    public int minSpeed, maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateWall());
    }
    IEnumerator CreateWall()
    {
        System.Random rnd = new System.Random();
        while (true)
        {
            for(int i = 0; i < rnd.Next(minTrombs, maxTrombs); i++)
            {
                GameObject tromb1 = Instantiate(tromb);
                var rot = tromb.transform.rotation; rot.z += rnd.Next(0, 180);
                tromb.transform.rotation = rot;
                tromb.transform.localScale = new Vector3(rnd.Next(1, 3), rnd.Next(1, 3), 0);
                tromb1.transform.position = spawnPoints[rnd.Next(0, spawnPoints.Count)].transform.position;
                yield return new WaitForSeconds(0.1f);
                tromb1.GetComponent<Rigidbody2D>().velocity = new Vector2(rnd.Next(-maxSpeed, -minSpeed), rnd.Next(-maxSpeed, maxSpeed));
                trombs.Add(tromb1);
            }
            yield return new WaitForSeconds(5);
        }
    }
    // Update is called once per frame
    public void Accs(GameObject Trombs)
    {
        System.Random rnd = new System.Random();
        Trombs.GetComponent<Rigidbody2D>().velocity = new Vector2(rnd.Next(-maxSpeed, -minSpeed), rnd.Next(-maxSpeed, maxSpeed));

    }
    void Update()
    {
        
    }
}
