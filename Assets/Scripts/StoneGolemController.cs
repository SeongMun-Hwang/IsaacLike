using UnityEngine;
using UnityEngine.AI;

public class StoneGolemController : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        float distance = 0f;
        if (player != null)
        {
            agent.destination = player.transform.position;
            distance = (player.transform.position - gameObject.transform.position).magnitude;
        }
    }
}
