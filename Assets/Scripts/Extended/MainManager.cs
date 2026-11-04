using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    // References instance of the MainManager script
    public static MainManager Instance { get; private set; }

    // References color change
    public Color TeamColor;
    
    // References resource database
    public ResourceDatabase ResourceDatabase;
    
    private void Awake()
    {
        // If Instance is not equal to null, destroy game objects and return
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Current instance of MainManager
        Instance = this;
        // Does not destroy MainManager upon loading Menu
        DontDestroyOnLoad(gameObject);
        
        // Sets TeamColor to white upon awake
        TeamColor = Color.white;
       
        // Initializes ResourceDatabase event members
        ResourceDatabase.Init();

        // Loads file if a color has been saved previously
        LoadColor();
    }

    // Allows JsonUtility to be used to transform objects to JSON
    [System.Serializable]
    class SaveData
    {
        // Stores color
        public Color TeamColor;
    }
    
    public void SaveColor()
    {
        // Creates an instance of the save data
        SaveData data = new SaveData();
        // Fills team color class member with TeamColor variable save in MainManager
        data.TeamColor = TeamColor;

        // Transforms instance to JSON
        string json = JsonUtility.ToJson(data);
        
        // Writes a string to a file
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        // Checks if a .json file exists
        string path = Application.persistentDataPath + "/savefile.json";
        // If there isn't one, color hasn't been saved. If file does exist, read content
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            // Gives resulting text to JsonUtility.FromJson to transform it back into a SaveData instance
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            // Sets the TeamColor to the color saved
            TeamColor = data.TeamColor;
        }
    }
}
