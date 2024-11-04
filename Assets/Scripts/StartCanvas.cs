using UnityEngine;

public class StartCanvas : MonoBehaviour
{
    public GameObject gameCanvas;
    private void Start()
    {
        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        gameCanvas.SetActive(true);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false; //마우스 커서 끔
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
