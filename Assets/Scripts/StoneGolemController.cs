using UnityEngine;
using UnityEngine.AI;

public class StoneGolemController : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;

    public Transform shootPostion;
    public GameObject stoneBullet;
    public float attackDuration = 5f;
    public float currentTime;

    Animator stoneBossAnimator;

    enum State
    {
        Idle,
        Shoot,
        Laser,
        Death,
    }
    State state;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        stoneBossAnimator = GetComponent<Animator>();
        state = State.Idle;
        gameObject.transform.position = new Vector3(0, 5, 0);
        currentTime = attackDuration;
    }
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        float distance = 0f;
        if (player != null)
        {
            agent.destination = player.transform.position;
            distance = (player.transform.position - gameObject.transform.position).magnitude;
        }
        switch (state)
        {
            case State.Idle:
                Death();
                currentTime -= Time.deltaTime;
                if (currentTime < 0)
                {
                    int rand = Random.Range(0, 2);
                    if (rand == 0)
                    {
                        IdleToShoot();
                    }
                    else if (rand == 1)
                    {

                    }
                    currentTime = attackDuration;
                }

                break;
            case State.Shoot:
                Death();
                break;

            case State.Death:
                return;
        }
    }
    public void IdleToShoot()
    {
        agent.isStopped = true;
        stoneBossAnimator.SetTrigger("Shoot");
        state = State.Shoot;
    }
    public void ArmShoot()
    {
        Debug.Log("stone shoot");
        GameObject go = Instantiate(stoneBullet);

        float angle = Mathf.Atan2(agent.destination.y, agent.destination.x) * Mathf.Rad2Deg;

        go.transform.rotation = Quaternion.Euler(0, 0, angle);
        go.transform.position = shootPostion.position;

        agent.isStopped = false;
        stoneBossAnimator.SetTrigger("Idle");
        state = State.Idle;
    }
    public void Death()
    {
        if (gameObject.GetComponent<HpController>().Hp < 1)
        {
            stoneBossAnimator.SetTrigger("Death");
            agent.isStopped = true;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            foreach (var colldier in gameObject.GetComponentsInChildren<CapsuleCollider2D>())
            {
                colldier.enabled = false;
            }
            state = State.Death;
        }
    }
}
