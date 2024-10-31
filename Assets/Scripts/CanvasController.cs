using UnityEngine;

public class CanvasController : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false; //���콺 Ŀ�� ��
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
