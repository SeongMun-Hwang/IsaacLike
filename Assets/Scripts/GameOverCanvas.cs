using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
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
