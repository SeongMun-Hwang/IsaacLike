using UnityEngine;

public class HpController : MonoBehaviour
{
    public int Hp;
    public void GetDamage(int damage)
    {
        Hp -= damage;
    }
    public void DestroyThis()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        Destroy(gameObject);
    }
}
