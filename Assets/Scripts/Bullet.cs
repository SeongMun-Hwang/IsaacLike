using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Velocity;
    int playerAttackStat = 2;

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
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<HpController>().GetDamage(playerAttackStat);
        }
        if (!collision.CompareTag("Untagged")&&!collision.CompareTag("Player"))
        {
            DisableThis();
        }
    }
    public void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
