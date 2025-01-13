using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class LevelData : ScriptableObject
{
    float tickInitial = 0.6f;
    float tickFastest = 0.01f;
    float startPosX = -8f;
    float startPosY = 7.5f;
    float screenEdge = 16.5f;
    float[] invaderShootInterval = { 0.5f, 2.0f };
    int descentSteps = 2;
}
