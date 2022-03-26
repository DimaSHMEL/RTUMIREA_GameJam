using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OneMinute : MonoBehaviour
{
    public List<GameObject> backGrounds;
    public GameObject creator;
    public float secs;
    public GameObject Granb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SixtySeconds());
    }
    IEnumerator SixtySeconds()
    {
        yield return new WaitForSeconds(secs);
        for(int i = 0; i < backGrounds.Count; i++)
        {
            Destroy(backGrounds[i].GetComponent<FlewBy>());
        }
        Destroy(creator);
        Destroy(Granb);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
