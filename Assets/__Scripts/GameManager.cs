using System;
using System.Collections;
using UnityEngine;

public class InvaderGroupBound
{
    public float leftX;
    public float rightX;
    public float topY;
    public float bottomY;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] InvaderData[] invaderDatas;
    [SerializeField] InvaderPool invaderPool;
    [SerializeField] float startPosX = 16.5f;
    [SerializeField] float startPosY = 7.5f;
    [SerializeField] float stepX = -2f;
    [SerializeField] float stepY = -1.5f;
    [SerializeField] float tick = 0.2f;
    [SerializeField] float screenEdge = 15f;

    int invaderDirection = 1;
    int descentSteps = 2;
    int descentCurrentSteps;
    bool invaderDescent;

    public static Action<InvaderGroupBound, Vector3> InvaderMove;
    InvaderGroupBound info;

    int level = 0;
    bool isGameRunning = true;

    void Start()
    {
        info = new InvaderGroupBound();
        info.leftX = startPosX;
        info.rightX = startPosX;
        info.topY = startPosY;
        info.bottomY = startPosY;

        LevelStart();
    }

    void LevelStart()
    {
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
                invader.transform.position = new Vector3(startPosX + stepX * x, startPosY + stepY * y, 0);
            }
        }
    }

    IEnumerator InvaderProcess()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(tick);
            InvaderProgress();
            //InvaderMove!.Invoke(info, Vector3.zero);
        }
    }

    void InvaderProgress()
    {
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
        }
        else
        {
            InvaderMove!.Invoke(info, Vector3.down * 0.25f);
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
        }
        else
        {
            InvaderMove!.Invoke(info, Vector3.right * 0.25f * invaderDirection);
        }
    }


    void Update()
    {
        
    }
}
