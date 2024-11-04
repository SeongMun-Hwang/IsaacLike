using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Cursor.visible = true;
            Time.timeScale = 1;
            SceneManager.LoadScene("SampleScene");
        }
    }
}
