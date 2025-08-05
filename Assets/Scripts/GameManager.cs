using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string playerName;
    public string playerScore;
    public string highScore;
    public string highScoreName;
    public int level = 0;

    private void Awake()
    {
        if (instance != null)
        {

            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
 [System.Serializable]
     class SaveData
     {
         public string hsName;
         public string hsScore;
     }

     public void SaveScore()
     {
        SaveData data = new SaveData();
        data.hsName = playerName;
        data.hsScore = playerScore;
        //only save if this is the highest score, so will have to read first, and compare.
        string json = JsonUtility.ToJson(data);
        Debug.Log("writing: " + playerName + " " + playerScore);
        Debug.Log("Path: " + Application.persistentDataPath);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
     }
    public void LoadScore()//reads the highest score
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreName = data.hsName;
            highScore = data.hsScore;
        }
    }
    public bool CheckScore()
    {
        int.TryParse(playerScore, out int score);
        int.TryParse(highScore, out int hscore);
        if (score > hscore)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ClearScore()
    {
        playerScore = "";
    }

}
