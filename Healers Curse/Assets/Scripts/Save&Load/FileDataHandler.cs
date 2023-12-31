using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "word";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption) 
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load() 
    {
        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath)) 
        {
            try 
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // optionally decrypt the data
                if (useEncryption) 
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // deserialize the data from Json back into the C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e) 
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data) 
    {
        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try 
        {
            // create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object into Json
            string dataToStore = JsonUtility.ToJson(data, true);

            // optionally encrypt the data
            if (useEncryption) 
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e) 
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public void SaveSceneIndex(int sceneIndex)
    {
        // Load the existing GameData
        GameData existingData = Load();

        // If the existing data is null, create a new instance
        if (existingData == null)
        {
            existingData = new GameData();
        }

        // Update the sceneIndex
        existingData.sceneIndex = sceneIndex;

        // Save only the sceneIndex back to the file
        Save(existingData);
    }

    public void SaveEnemyType(int enemyType)
    {
        // Load the existing GameData
        GameData existingData = Load();

        // If the existing data is null, create a new instance
        if (existingData == null)
        {
            existingData = new GameData();
        }

        // Update the enemyType
        existingData.enemyType = enemyType;

        // Save only the sceneIndex back to the file
        Save(existingData);
    }

    public void SavePlayerPosition(Vector2 playerPosition)
    {
        // Load the existing GameData
        GameData existingData = Load();

        // If the existing data is null, create a new instance
        if (existingData == null)
        {
            existingData = new GameData();
        }

        // Update the player's position
        existingData.playerPosition = playerPosition;

        // Save only the sceneIndex back to the file
        Save(existingData);
    }

    public void SaveNumOfBattles(int numOfBattles)
    {
        // Load the existing GameData
        GameData existingData = Load();

        // If the existing data is null, create a new instance
        if (existingData == null)
        {
            existingData = new GameData();
        }

        // Update the player's position
        existingData.numOfBattles = numOfBattles;

        // Save only the sceneIndex back to the file
        Save(existingData);
    }

    public void SavePlayerStat(int playerStat, int stat)
    {
        // Load the existing GameData
        GameData existingData = Load();

        // If the existing data is null, create a new instance
        if (existingData == null)
        {
            existingData = new GameData();
        }

        if(playerStat == 1)
        {
            existingData.armor = stat;
        }
        else if(playerStat == 2)
        {
            existingData.attack = stat; 
        }
        else if(playerStat == 3)
        {
            existingData.crit = stat;  
        }
        else if(playerStat == 4)
        {
            existingData.dodge = stat;  
        }
        else if(playerStat == 5)
        {
            existingData.energy = stat; 
        }
        else if(playerStat == 6)
        {
            existingData.health = stat; 
        }
        else if(playerStat == 7)
        {
            existingData.resist = stat; 
        }
        else if(playerStat == 8)
        {
            existingData.magic = stat; 
        }
        else if(playerStat == 9)
        {
            existingData.mana = stat;
        }
        else if(playerStat == 10)
        {
            existingData.speed = stat; 
        }

        // Save only the sceneIndex back to the file
        Save(existingData);
    }

    // the below is a simple implementation of XOR encryption
    private string EncryptDecrypt(string data) 
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++) 
        {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}