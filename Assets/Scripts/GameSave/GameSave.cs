using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSave : MonoBehaviour

{
    public Save save;
    public static GameSave _instance { get; private set; }
    private string SaveFileName;
    public static GameSave GetInstance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindAnyObjectByType<GameSave>();
        }
        return _instance;
    }
    private void Awake()
    {
        SaveFileName = "GameSave.bin";
    }
    private Save CreateSaveFile()
    {
        Player player = Player.GetInstance();
        if (player == null)
        {
            Debug.LogError("Player instance is null!");
            return null;
        }

        save = new Save
        {
            Hp = (int)player.HP,
            bulletType = player.bulletId,
            posX = player.transform.position.x,
            posY = player.transform.position.y,
            posZ = player.transform.position.z
        };

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveFile();
        if (save == null) return;

        string directory = "Saves";
        string filePath = Path.Combine(directory, SaveFileName);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using (FileStream fileStream = File.Create(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fileStream, save);
        }

        Debug.Log($"Game saved successfully at {Path.GetFullPath(filePath)}");
    }

    public void LoadGame()
    {
        string filePath = Path.Combine("Saves", SaveFileName);

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found!");
            return;
        }
        using (FileStream fileStream = File.Open(filePath, FileMode.Open))
        {
            BinaryFormatter bf = new BinaryFormatter();
            Save save = bf.Deserialize(fileStream) as Save;

            if (save != null)
            {
                Player player = Player.GetInstance();
                if (player != null)
                {
                    player.HP = save.Hp;
                    player.bulletId = save.bulletType;
                    player.transform.position = new Vector3(save.posX, save.posY, save.posZ);
                    Debug.Log("Game loaded successfully!");
                }
                else
                {
                    Debug.LogError("Player instance is null!");
                }
            }
        }
    }

}
