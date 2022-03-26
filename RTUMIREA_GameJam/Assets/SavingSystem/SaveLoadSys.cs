using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine;
using System.Numerics;

[Serializable]
class currentScene
{
    public int sceneNumber;
    public int score;
    public String sceneName;
}
[Serializable]
class SaveScenes
{
    public List<float> playerPosX;
    public List<float> playerPosY;
    public List<int> itemsInScene;
}

public class SaveLoadSys : MonoBehaviour
{
    public int sceneNumber;
    public int score;
    public String sceneName;
    public List<float> playerPosX;
    public List<float> playerPosY;
    public List<int> itemsInScene;
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Save.dat");
        currentScene current = new currentScene();
        SaveScenes scenes = new SaveScenes();
        scenes.playerPosX = playerPosX;
        scenes.playerPosY = playerPosY;
        current.sceneNumber = sceneNumber;
        current.score = score;
        current.sceneName = sceneName;
        scenes.itemsInScene = itemsInScene;
        bf.Serialize(file, current);
        bf.Serialize(file, scenes);
        file.Close();
    }
    public bool LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
   + "/Save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/Save.dat", FileMode.Open);
            currentScene current = (currentScene)bf.Deserialize(file);
            SaveScenes scenes = (SaveScenes)bf.Deserialize(file);
            file.Close();
            playerPosX = scenes.playerPosX;
            playerPosY = scenes.playerPosY;
            sceneNumber = current.sceneNumber;
            score = current.score;
            sceneName = current.sceneName;
            itemsInScene = scenes.itemsInScene;
            return true;
        }
        return false;
    }
    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/Save.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/Save.dat");
        }

    }



}