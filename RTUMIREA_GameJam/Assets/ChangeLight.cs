using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChangeLight : MonoBehaviour
{
    public Light2D lightSource;
    public Light2D lightSource2;
    public Color turnOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            lightSource.color = turnOn;
            lightSource2.color = turnOn;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
