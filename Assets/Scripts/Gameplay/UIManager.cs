using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText; 
    [SerializeField] private TextMeshProUGUI scoreText;  
    [SerializeField] private GameObject highScorePanel;  
    
    [SerializeField] private Slider fireRateSlider;      
    [SerializeField] private TextMeshProUGUI highScoreText;


    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject gameOverScreen;

    private Coroutine sliderCoroutine;

    public static UIManager Instance;

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
        var scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.OnScoreChanged.AddListener(UpdateScoreValue);

        Player playerObject = FindObjectOfType<Player>();
        playerObject.healthValue.OnHealthChanged.AddListener(UpdateHealthValue);
        UpdateHealthValue(playerObject.healthValue.GetHealthValue());
        playerObject.healthValue.OnDied.AddListener(ShowGameOverScreen);
    }

    public void UpdateScoreValue(int score)
    {
        scoreText.text = score.ToString();
    }

   
    public void UpdateHealthValue(float health)
    {
        healthText.text = health.ToString();
    }
    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
    public void ShowHighScorePanel()
    {
        highScorePanel.SetActive(true);
    }

    public void ShowHighScorePanel(bool isHighScore)
    {
        if (isHighScore)
        {
            nameInputField.gameObject.SetActive(true); 
        }
        else
        {
            nameInputField.gameObject.SetActive(false); 
        }

        highScorePanel.SetActive(true); 
    }

    public void ShowGameOverScreen(bool isHighScore)
    {
        gameOverScreen.SetActive(true); 
        ShowHighScorePanel(isHighScore); 
    }

    public void SubmitHighScore()
    {
        if (string.IsNullOrWhiteSpace(nameInputField.text) || nameInputField.text.Length <= 3)
        {
           
            return;
        }

        string playerName = nameInputField.text.Substring(0, 3).ToUpper();
        ScoreManager.Instance.SetHighScoreName(playerName);
        highScorePanel.SetActive(false);
    }


    public void UpdateHighScoreDisplay(int score)
    {
        highScoreText.text = $"High Score: {score}";
    }

  
    public void StartFireRateSlider(float duration)
    {
        if (sliderCoroutine != null)
        {
            StopCoroutine(sliderCoroutine);
        }

        sliderCoroutine = StartCoroutine(UpdateFireRateSlider(duration));
    }

    private IEnumerator UpdateFireRateSlider(float duration)
    {
        fireRateSlider.maxValue = duration;
        fireRateSlider.value = duration;
        fireRateSlider.gameObject.SetActive(true);

        float timeRemaining = duration;

        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            fireRateSlider.value = timeRemaining;
            yield return null;
        }

        fireRateSlider.gameObject.SetActive(false);
        sliderCoroutine = null;
    }
}
