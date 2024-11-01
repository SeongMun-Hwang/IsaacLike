using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public BulletPool bulletPool;
    public TextMeshProUGUI timeText;
    float playTime = 0;
    private void Update()
    {
        playTime += Time.deltaTime;
        timeText.text = ((int)playTime / 60).ToString() + ":" + ((int)playTime % 60).ToString();
    }
}