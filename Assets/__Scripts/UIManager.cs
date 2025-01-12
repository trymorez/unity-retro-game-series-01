using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour, IScoreObserver
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] RectTransform lifePanel;
    [SerializeField] RectTransform levelText;
    [SerializeField] GameManager gameManager;
    public static UIManager instance;

    void Awake()
    {
        instance = this;
        gameManager.ObserverAdd(this);
    }

    void Start()
    {
        
    }

    void OnDestroy()
    {
        gameManager.ObserverRemove(this);
    }

    public void LevelDisplay(int level)
    {
        levelText.DOScale(Vector3.one, 2f).SetEase(Ease.OutExpo);
        levelText.DOScale(Vector3.zero, 1f).SetDelay(2f);
    }

    public void ScoreDisplay(int score) 
    {
        scoreText.text = "SCORE " + score;
    }

    public void HighScoreDisplay(int highScore)
    {
        highScoreText.text = "HIGH " + highScore;
    }

    public void OnScoreChanged(int score)
    {
        ScoreDisplay(score);
    }

    public void OnHighScoreChanged(int highScore)
    {
        HighScoreDisplay(highScore);
    }
}
