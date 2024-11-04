using UnityEngine;

public class DemonBullet : MonoBehaviour
{
    public Vector2 Velocity;
    public int statAttack = 1;
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

        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<HpController>().GetDamage(statAttack);
            if (!gameObject.CompareTag("Laser"))
            {
                Destroy(gameObject);
            }
        }
        if (!collision.CompareTag("Untagged") && !collision.CompareTag("Enemy") && !gameObject.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }
    }
}
