using UnityEngine;

public class HpController : MonoBehaviour
{
    public int Hp;
    public void GetDamage(int damage)
    {
        Hp -= damage;
        if (gameObject.tag == "Player")
        {
            StartCoroutine(gameObject.GetComponent<PlayerController>().PlayerInvincible());
        }
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
