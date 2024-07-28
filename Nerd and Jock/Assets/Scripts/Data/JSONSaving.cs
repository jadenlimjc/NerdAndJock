using System.IO;
using UnityEngine;

public class JSONSaving : MonoBehaviour
{
    public static JSONSaving Instance;
    public StageManager stageManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private GameData gameData = new GameData();

    private string filePath;

    // Start is called before the first frame update
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SaveData();

        if (Input.GetKeyDown(KeyCode.L))
            LoadData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Saving Data at " + filePath);
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Loading Data from " + filePath);
        }
        else
        {
            Debug.Log("Save file does not exist");
        }
    }

    public void ClearGameData()
    {
        gameData.stages.Clear();
        Debug.Log("Game data cleared");
    }

    public GameData GetGameData()
    {
        return gameData;
    }

    public void InitializeGameData()
    {
        stageManager.InitializeGameDataFromSO();
    }
}