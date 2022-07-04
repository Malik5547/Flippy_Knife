using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
 

public class SaveSystem : MonoBehaviour
{

    static string path;

    //void AddNewItemToList(parameter list)
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    MemoryStream ms = new MemoryStream(Convert.FromBase64String(str));
    //    List<PlayerScore> ps = bf.Deserialize(ms) as List<PlayerScore>;
    //    ps.Add(new PlayerScore(parameter list));
    //    ms = new MemoryStream();
    //    bf.Serialize(ms, ps as List<PlayerScore>);
    //    SaveDocumentOnDrive();
    //}

    public static void SetPath(string savePath)
    {
        path = savePath;
    }

    public static void SaveLevelData(List<LevelData> data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static List<LevelData> GetLevelData()
    {
        Debug.Log("Save path: " + path);

        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            List<LevelData> levelDatas = formatter.Deserialize(stream) as List<LevelData>;
            stream.Close();
            return levelDatas;
        }
        else
        {
            Debug.Log("No level data found in " + path);
            return null;
        }
    }
}
