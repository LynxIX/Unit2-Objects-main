using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent<int> OnScoreChanged;
    public UnityEvent<int> OnHighestScoreChange;

    [SerializeField] private int totalScore;
    [SerializeField] private int highestScore;

    [Header("Score Values")]
    [SerializeField] private int scorePerEnemy;
    [SerializeField] private int scorePerCoin;
    [SerializeField] private int scorePerPowerUp;

    [SerializeField] private List<ScoreData> allScores = new List<ScoreData>();
    [SerializeField] private ScoreData latestScore;

    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        Player playerObject = FindObjectOfType<Player>();
        if (playerObject == null)
        {
            
            return;
        }

        
        playerObject.healthValue.OnDied.AddListener(RegisterScore);

       
        highestScore = PlayerPrefs.GetInt("HighScore");

        
        string latestScoreInJson = PlayerPrefs.GetString("LatestScore");
        if (!string.IsNullOrEmpty(latestScoreInJson))
        {
            latestScore = JsonUtility.FromJson<ScoreData>(latestScoreInJson);
        }

       
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            
            return;
        }

       
        OnHighestScoreChange.AddListener(uiManager.UpdateHighScoreDisplay);
        OnHighestScoreChange.Invoke(highestScore);
    }

    private void RegisterScore()
    {
        latestScore = new ScoreData("AAA", totalScore);
        string latestScoreInJson = JsonUtility.ToJson(latestScore);
        PlayerPrefs.SetString("LatestScore", latestScoreInJson);

        bool isHighScore = false;

        if (totalScore > highestScore)
        {
            highestScore = totalScore;
            PlayerPrefs.SetInt("HighScore", highestScore);
            isHighScore = true;
        }

        
        UIManager.Instance.ShowHighScorePanel(isHighScore);
    }

    public void IncreaseScoreByValue(int value)
    {
        totalScore += value;
        OnScoreChanged.Invoke(totalScore);
    }

    public void SetHighScoreName(string name)
    {
        latestScore.userName = name;
        string latestScoreInJson = JsonUtility.ToJson(latestScore);
        PlayerPrefs.SetString("LatestScore", latestScoreInJson);
    }
}
