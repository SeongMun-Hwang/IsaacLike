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
        if (gameObject.GetComponent<CapsuleCollider2D>().enabled)
        {
            transform.Translate(Velocity * Time.deltaTime);
        }
    }
    private void OnEnable()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<HpController>() == null)
            {
                collision.GetComponentInParent<HpController>();
            }
            else
            {
                collision.GetComponent<HpController>().GetDamage(playerAttackStat);
            }
            gameObject.GetComponent<Animator>().SetTrigger("Disable");
        }
        if (!collision.CompareTag("Untagged") && !collision.CompareTag("Player"))
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<Animator>().SetTrigger("Disable");
        }
    }
    public void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
