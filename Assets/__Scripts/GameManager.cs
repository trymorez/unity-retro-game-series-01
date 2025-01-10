using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] InvaderData[] invaderDatas;
    [SerializeField] InvaderPool invaderPool;
    [SerializeField] float startPosX = 16.5f;
    [SerializeField] float startPosY = 7.5f;
    [SerializeField] float stepX = -2f;
    [SerializeField] float stepY = -1.5f;
    int level = 0;
    float tick = 1.0f;

    void Start()
    {
        LevelStart();
    }

    void LevelStart()
    {
        InitializeInvaders();
    }

    void InitializeInvaders()
    {
        int typeIndex = 0;
        for (int y = 0; y < 5; y++)
        {
            if (y == 1 || y == 3)
            {
                typeIndex++;
            }
            for (int x = 0; x < 10; x++)
            {
                GameObject invader = invaderPool.GetInvader(InvaderPool.instance.invaderDatas[typeIndex].prefab);
                invader.transform.position = new Vector3(startPosX + stepX * x, startPosY + stepY * y, 0);
            }
        }
    }


    void Update()
    {
        
    }
}
