using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour, IScoreObserver
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] RectTransform lifePanel;
    [SerializeField] RectTransform levelText;
    [SerializeField] RectTransform[] rectIntro;
    [SerializeField] GameManager gameManager;
    [SerializeField] InputActionAsset inputAction;
    CanvasGroup pressAnyKeyMessage;
    InputAction action;
    float introOrigX = -340f;
    public static UIManager instance;

    void Awake()
    {
        instance = this;
        action = inputAction.FindAction("Player/AnyKey");
        gameManager.ObserverAdd(this);
        GameManager.OnGameStart += IntroScreenStart;
        pressAnyKeyMessage = rectIntro[5].GetComponent<CanvasGroup>();
    }

    void OnDestroy()
    {
        gameManager.ObserverRemove(this);
        GameManager.OnGameStart -= IntroScreenStart;
    }

    void IntroScreenStart()
    {
        StartCoroutine(StartIntro());
    }

    void IntroScreenEnd()
    {
        foreach (RectTransform intro in rectIntro)
        {
            intro.position = new Vector3(introOrigX, intro.position.y, 0);
        }
    }

    IEnumerator StartIntro()
    {
        float delay = 0.0f;
        foreach (RectTransform intro in rectIntro)
        {
            intro.DOAnchorPosX(0, 1.0f).SetEase(Ease.OutExpo).SetDelay(delay += 0.25f);
        }
        yield return new WaitForSeconds(2.5f);
        yield return StartCoroutine(WaitAnyKey());
    }

    IEnumerator WaitAnyKey()
    {
        action.Enable();
        StartCoroutine(PressAnyKey());
        while (!action.triggered)
        {
            
            yield return null;
        }
        GameManager.isGameRunning = true;
        action.Disable();
        IntroScreenEnd();
        GameManager.instance.LevelStart();
    }

    IEnumerator PressAnyKey()
    {
        while (!GameManager.isGameRunning)
        {
            pressAnyKeyMessage.DOFade(0, 0.5f);
            yield return new WaitForSeconds(0.5f);
            pressAnyKeyMessage.DOFade(1, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void LevelDisplay(int level)
    {
        levelText.DOScale(Vector3.one, 2f).SetEase(Ease.OutExpo);
        levelText.DOScale(Vector3.zero, 1f).SetDelay(2.5f);
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
