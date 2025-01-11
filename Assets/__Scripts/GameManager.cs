using System;
using System.Collections;
using UnityEngine;

public class InvaderGroupBound
{
    public float leftX;
    public float rightX;
    public float topY;
    public float bottomY;
    public int invaders = 50;
    public float tick;
    public int sprite;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] InvaderData[] invaderDatas;
    [SerializeField] InvaderPool invaderPool;
    [SerializeField] float startPosX = -8f;
    [SerializeField] float startPosY = 7.5f;
    [SerializeField] float spacingX = 2f;
    [SerializeField] float spacingY = -1.5f;
    [SerializeField] float moveAmountX = 0.5f;
    [SerializeField] float moveAmountY = 0.5f;
    [SerializeField] float tickInterval = 0.5f;
    [SerializeField] float tickFastest = 0.01f;
    [SerializeField] float screenEdge = 15f;
    [SerializeField] float deadline = -8f;
    [SerializeField] int life = 2;
    [SerializeField] int score = 2;
    [SerializeField] int highScore = 2;

    int invaderDirection = 1;
    int descentSteps = 2;
    int descentCurrentSteps;
    bool invaderDescent;

    string[] tickSounds = { "Tick0", "Tick1", "Tick2", "Tick3" };
    int tickSoundsIndex = 0;

    public static Action<InvaderGroupBound, Vector3> InvaderMove;
    InvaderGroupBound info;

    int level = 0;
    bool isGameRunning = true;

    void Start()
    {
        Invader.OnTickProgress += TickProgress;

        info = new InvaderGroupBound();
        info.leftX = startPosX;
        info.rightX = startPosX;
        info.topY = startPosY;
        info.bottomY = startPosY;
        info.invaders = 50;
        info.tick = tickInterval;

        LevelStart();
    }

    void OnDisable()
    {
        Invader.OnTickProgress -= TickProgress;
    }

    void TickProgress()
    {
        tickInterval = Mathf.Lerp(tickFastest, 0.5f, (float)--info.invaders / 50f);
    }

    void LevelStart()
    {
        UIManager.instance.LevelDisplay(1);
        InitializeInvaders();
        StartCoroutine(InvaderProcess());
    }

    void InitializeInvaders()
    {
        int invaderType = 0;
        for (int y = 0; y < 5; y++)
        {
            if (y == 1 || y == 3)
            {
                invaderType++;
            }
            for (int x = 0; x < 10; x++)
            {
                GameObject invader = invaderPool.GetInvader(InvaderPool.instance.invaderDatas[invaderType].prefab);
                invader.transform.position = new Vector3(startPosX + spacingX * x, startPosY + spacingY * y, 0);
            }
        }
    }

    IEnumerator InvaderProcess()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(tickInterval);
            InvaderProgress();
            GameOverCheck();
        }
        GameOver();
    }

    void InvaderProgress()
    {
        info.sprite = (info.sprite + 1) % 2;
        PlayTickSound();
        if (invaderDescent == true)
        {
            InvaderVerticalMove();
        }
        else
        {
            InvaderHorizontalMove();
        }
    }

    void InvaderVerticalMove()
    {
        if (descentCurrentSteps == descentSteps)
        {
            invaderDescent = false;
            descentCurrentSteps = 0;
            InvaderHorizontalMove();
        }
        else
        {
            InvaderMove!.Invoke(info, Vector3.down * moveAmountY);
            descentCurrentSteps++;
        }
    }

    void InvaderHorizontalMove()
    {
        if ((info.rightX >= screenEdge && invaderDirection == 1) ||
           (info.leftX <= -screenEdge && invaderDirection == -1))
        {
            invaderDirection *= -1;
            invaderDescent = true;
            InvaderVerticalMove();
        }
        else
        {
            InvaderMove!.Invoke(info, Vector3.right * moveAmountX * invaderDirection);
        }
    }

    void PlayTickSound()
    {
        tickSoundsIndex = (tickSoundsIndex + 1) % 4;
        SoundManager.Play(tickSounds[tickSoundsIndex]);
    }

    void GameOverCheck()
    {
        if (info.bottomY <= deadline)
        {
            isGameRunning = false;
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver");
    }

    void Update()
    {
        
    }
}
