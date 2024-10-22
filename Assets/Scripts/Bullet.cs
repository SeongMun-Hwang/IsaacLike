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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bullet trigger with : "+collision.gameObject.tag);
        if (collision.CompareTag("Wall"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Disable");
        }
    }
    public void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
