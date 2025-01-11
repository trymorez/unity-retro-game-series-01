using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] RectTransform rect;
    public static UIManager instance;

    void Awake()
    {
        instance = this;
    }

    public void LevelDisplay(int level)
    {
        rect.DOScale(Vector3.one, 2f).SetEase(Ease.OutExpo);
        rect.DOScale(Vector3.zero, 1f).SetDelay(2f);


    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
