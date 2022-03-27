using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinalLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(StartCountDown());
        }
    }
    public AudioSource ma;
    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(4);
        GameObject cam = GameObject.Find("Camera");
        Destroy(cam.GetComponent<CameraScript>());
        Destroy(GameObject.Find("UI"));
        cam.transform.position = new Vector3(-11.6f, -1.7f, -14f);
        yield return new WaitForSeconds(1);
        while (cam.GetComponent<Camera>().orthographicSize < 32)
        {
            cam.GetComponent<Camera>().orthographicSize += 0.25f;
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(1f);
        ma.Play();
        yield return new WaitForSeconds(1f);
        cam.transform.position = new Vector3(-11.6f, -1.7f, -16f);
        yield return new WaitForSeconds(2);
        GameObject.Find("Savings").GetComponent<SaveLoadSys>().ResetData();
        SceneManager.LoadScene("Menu");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
