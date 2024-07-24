using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel, gameOverPanel;
    [SerializeField] TextMeshProUGUI scoreText, winScoreText;

    public static bool isGameOver, isGamePaused;
    private static float _currentDifficultyModifier;
    
    private int _currentScore;
    private int _addScorePerTime = 10;
    private int _addScorePerCoin = 25;

    private float _updateScoreDelay = 3f;
    private float _updateDifficultyDelay = 5f;
    private float _deltaDifficulty = 2.5f;
     
    private void Start()
    {
        InitGameData();
        StartCoroutine(UpdateScoreRoutine());
        StartCoroutine(UpdateDifficultyRoutine());
    }

    private void Update()
    {
        ControlGameState();
    }

    private void ControlGameState()
    {
        if (isGameOver)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);

            if (Input.GetMouseButton(0))
            {
                isGamePaused = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    private void InitGameData()
    {
        scoreText.text = "0";
        _currentDifficultyModifier = 20f;
        isGameOver = false;
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    private IEnumerator UpdateScoreRoutine()
    {
        while (!isGameOver)
        {
            yield return new WaitWhile(() => isGamePaused);
            yield return new WaitForSeconds(_updateScoreDelay);

            _currentScore += _addScorePerTime;
            UpdateScoreText();
        }
    }

    private IEnumerator UpdateDifficultyRoutine()
    {
        while (!isGameOver)
        {
            yield return new WaitWhile(() => isGamePaused);
            yield return new WaitForSeconds(_updateDifficultyDelay);

            _currentDifficultyModifier += _deltaDifficulty;
        }
    }

    public void AddCoinScore()
    {
        _currentScore += _addScorePerCoin;
        UpdateScoreText();
    }

    public static float GetCurrentDifficulty() => _currentDifficultyModifier;

    private void UpdateScoreText() 
    {
        scoreText.text = _currentScore.ToString();
        winScoreText.text = "Score: " + _currentScore.ToString();
    }

}
