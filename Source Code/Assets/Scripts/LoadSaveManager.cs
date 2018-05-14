using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LoadSaveManager
{
    public const string extension = ".ngj"; //neuro game jam XD
    public static Game atualGame = new Game();

    public static void SaveGame(Game game)
    {
        string path = Application.persistentDataPath + @"/GameSession_" + extension;

        //if (!File.Exists(path))
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        bf.Serialize(stream, game);
        stream.Close();
    }

    public static Game LoadGame()
    {
        string path = Application.persistentDataPath + @"/GameSession_" + extension;
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Game game;
            game = bf.Deserialize(stream) as Game;

            stream.Close();
            return game;
        }
        else
        {
            Debug.LogError("Game not found");
            return null;
        }
    }
}

[System.Serializable]
public class Game
{
    public string name = "";
    public List<Level> levels = new List<Level>();
}

[System.Serializable]
public class Level
{
    public LevelParams levelParams;
    public List<Vector3T> position_HandR = new List<Vector3T>();
    public List<Vector3T> position_HandL = new List<Vector3T>();
    public List<Vector3T> position_Head = new List<Vector3T>();
    public List<Vector3T> position_Ball = new List<Vector3T>();
    public List<Vector3T> rotation_HandR = new List<Vector3T>();
    public List<Vector3T> rotation_HandL = new List<Vector3T>();
    public List<Vector3T> rotation_Head = new List<Vector3T>();
    public List<Vector3T> rotation_Ball = new List<Vector3T>();
    /// <summary>
    /// time in wich the point was made
    /// </summary>
    public List<float> point = new List<float>();
}

[System.Serializable]
public class LevelParams
{
    public int levelIndex = 0;
    public float timePlayed = 0;
    public float timeGuess = 0;
}

[System.Serializable]
public class Vector3T
{
    public Vector3 vector3;
    public float time;

    public Vector3T()
    {
        vector3 = Vector3.zero;
        time = 0;
    }

    public Vector3T(Vector3 vector, float time)
    {
        vector3 = vector;
        this.time = time;
    }
}

