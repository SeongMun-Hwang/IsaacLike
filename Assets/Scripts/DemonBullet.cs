using UnityEngine;

public class DemonBullet : MonoBehaviour
{
    public Vector2 Velocity;
    void Start()
    {
        if (!GetComponent<SpriteRenderer>().isVisible)
        {
            //Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(Velocity * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(gameObject);
    }
}
