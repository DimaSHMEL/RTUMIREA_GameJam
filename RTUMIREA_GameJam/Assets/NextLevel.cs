using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    public string nextScene, prevScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SaveLoadSys SAVE = GameObject.Find("Savings").GetComponent<SaveLoadSys>();
            SAVE.LoadGame();
            if(nextScene == "Level2")
            {
                SAVE.sceneNumber = 1;
                SAVE.sceneName = nextScene;
                SAVE.SaveGame();
            }
            SceneManager.LoadScene(nextScene);
        }
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
