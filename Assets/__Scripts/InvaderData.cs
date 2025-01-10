using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(fileName = "NewInvader", menuName = "Invader")]
public class InvaderData : ScriptableObject
{
    public int score;
    public Color color;
    public GameObject prefab;
}
