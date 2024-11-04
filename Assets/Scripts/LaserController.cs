using UnityEngine;

public class LaserController : MonoBehaviour
{
    Animator laserAnimator;
    void Start()
    {
        laserAnimator = GetComponent<Animator>();
    }
    public void EndLaser()
    {
        laserAnimator.GetComponent<Animator>().SetTrigger("Idle");
        gameObject.SetActive(false);
    }
}