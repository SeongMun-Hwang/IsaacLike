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

    enum State
    {
        Idle,
        Attack,
    }
    State state;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currentStateTime = attackDuration;
    }
    private void Update()
    {
        agent.destination = GameObject.FindWithTag("Player").transform.position;

        currentStateTime -= Time.deltaTime;
        if (currentStateTime < 0 && enemyBullet != null)
        {
            EnemyAttack();
        }
    }
    void EnemyAttack()
    {
        agent.isStopped = true;
        enemyAnimator.SetTrigger("Attack");
        currentStateTime = attackDuration;
    }

    public void EnemyStateToIdle()
    {
        GameObject bullet = Instantiate(enemyBullet, transform);
        bullet.transform.position = transform.position;

        float angle = Mathf.Atan2(agent.destination.y, agent.destination.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        agent.isStopped = false;
        enemyAnimator.SetTrigger("Idle");
    }
}
