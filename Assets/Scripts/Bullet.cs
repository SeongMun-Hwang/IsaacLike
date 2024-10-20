using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Velocity;

    void Update()
    {
        if (!GetComponent<SpriteRenderer>().isVisible)
        {
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(Velocity * Time.deltaTime);
    }
}
