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

    private void Update()
    {
        playTime += Time.deltaTime;
        timeText.text = ((int)playTime / 60).ToString() + ":" + ((int)playTime % 60).ToString();

        if (Player == null)
        {
            Time.timeScale = 0;
            gameOverCanvas.SetActive(true);
        }
    }
}