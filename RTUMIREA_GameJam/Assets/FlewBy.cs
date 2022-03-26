using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlewBy : MonoBehaviour
{
    private Transform pos;
    private Transform start_pos;
    public float x_restart, x_start;
    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform;
        start_pos = pos;
    }

    // Update is called once per frame
    void Update()
    {
        pos.position = new Vector3(pos.position.x - 0.1f, pos.position.y, pos.position.z);
        if(pos.position.x <= x_restart)
        {
            pos.position = new Vector3(x_start, pos.position.y, pos.position.z);
        }
    }
}
