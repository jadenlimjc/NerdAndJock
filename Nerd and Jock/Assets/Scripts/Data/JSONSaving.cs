using System.IO;
using UnityEngine;

public class JSONSaving : MonoBehaviour
{
    public static JSONSaving Instance;

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
    }

    public GameData GetGameData()
    {
        return gameData;
    }

    public void InitializeGameData()
    {
        ClearGameData();

        var nj3001 = new StageData("NJ3001", "", 0, false, float.MaxValue, null);
        var nj3012 = new StageData("NJ3012", "", 0, false, float.MaxValue, null);

        var nj2001 = new StageData("NJ2001", "", 0, false, float.MaxValue, new string[] { "NJ3001" });
        var nj2012 = new StageData("NJ2012", "", 0, false, float.MaxValue, new string[] { "NJ3012" });
        var nj2020 = new StageData("NJ2020", "", 0, false, float.MaxValue, null);
        var nj2021 = new StageData("NJ2021", "", 0, false, float.MaxValue, null);

        var nj1001 = new StageData("NJ1001", "", 0, true, float.MaxValue, new string[] { "NJ2001", "NJ2012", "NJ2020", "NJ2021" });

        gameData.stages.Add(nj1001);
        gameData.stages.Add(nj2001);
        gameData.stages.Add(nj2012);
        gameData.stages.Add(nj2020);
        gameData.stages.Add(nj2021);
        gameData.stages.Add(nj3001);
        gameData.stages.Add(nj3012);
        SaveData();
    }
}