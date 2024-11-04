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

    public GameObject Player;
    public GameObject gameOverCanvas;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, false);
    }
    private void Update()
    {
        playTime += Time.deltaTime;
        timeText.text = ((int)playTime / 60).ToString() + ":" + ((int)playTime % 60).ToString();

        if (Player == null)
        {
            gameOverCanvas.SetActive(true);
        }
    }
}