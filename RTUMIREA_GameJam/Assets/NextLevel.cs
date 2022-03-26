using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    public string nextScene, prevScene;

    public GameObject lastPlaceInLevel;
    public GameObject cameraLasPos;
    private SaveLoadSys SAVE;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SaveLoadSys SAVE = GameObject.Find("Savings").GetComponent<SaveLoadSys>();
            SAVE.LoadGame();
            if(SceneManager.GetActiveScene().name != "Runner")
            {
                int number = SAVE.sceneNumber;
                SAVE.playerPosX[number] = lastPlaceInLevel.transform.position.x;
                SAVE.playerPosY[number] = lastPlaceInLevel.transform.position.y;
                SAVE.cameraPosX[number] = cameraLasPos.transform.position.x;
                SAVE.cameraPosY[number] = cameraLasPos.transform.position.y;
                SAVE.SaveGame();
            }
            if(nextScene == "Level2")
            {
                SAVE.sceneNumber = 1;
                SAVE.sceneName = nextScene;
                SAVE.SaveGame();
            }
            if (nextScene == "Level1")
            {
                SAVE.sceneNumber = 0;
                SAVE.sceneName = nextScene;
                SAVE.SaveGame();
            }
            SceneManager.LoadScene(nextScene);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SAVE = GameObject.Find("Savings").GetComponent<SaveLoadSys>();
        SAVE.LoadGame();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
