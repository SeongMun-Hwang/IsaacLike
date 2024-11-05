using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //restart
    float holdTime = 0f;
    float needHolTime = 2.0f;
    bool resetKeyHold = false;

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

        if (Input.GetKey(KeyCode.R))
        {
            holdTime += Time.deltaTime;
            if (holdTime >= needHolTime)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        else
        {
            holdTime = 0f;
        }
    }
}