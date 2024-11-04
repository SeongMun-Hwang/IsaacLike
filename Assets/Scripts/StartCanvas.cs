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
        Cursor.visible = false; //���콺 Ŀ�� ��
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
