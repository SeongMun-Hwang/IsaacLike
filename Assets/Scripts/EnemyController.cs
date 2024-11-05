using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    public GameObject enemyBullet;
    Animator enemyAnimator;

    public float currentStateTime;
    public float attackDuration = 3f;

    public int statAttack = 5;
    public float attackDistance = 10f;
    enum State
    {
        Idle,
        Attack,
        Death,
    }
    State state;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currentStateTime = attackDuration;
        state = State.Idle;
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player");
        float distance = 0f;
        if (player != null)
        {
            agent.destination = player.transform.position;
            distance = (player.transform.position - gameObject.transform.position).magnitude;
        }
        if (state != State.Death)
        {
            if (enemyBullet != null)
            {
                currentStateTime -= Time.deltaTime;
                if (currentStateTime < 0 && distance < attackDistance)
                {
                    EnemyAttack();
                }
            }
            
            int hp = gameObject.GetComponent<HpController>().Hp;
            if (hp < 1)
            {
                enemyAnimator.SetTrigger("Death");
                state = State.Death;
            }
        }
        return;
    }
    void EnemyAttack()
    {
        agent.isStopped = true;
        enemyAnimator.SetTrigger("Attack");
        currentStateTime = attackDuration;
    }

    public void EnemyStateToIdle()
    {
        GameObject bullet = Instantiate(enemyBullet, gameObject.transform);
        Vector3 direction = agent.destination - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        agent.isStopped = false;
        enemyAnimator.SetTrigger("Idle");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<HpController>().GetDamage(statAttack);
        }
    }
}
