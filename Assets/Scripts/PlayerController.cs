using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 7f;

    //player input movement
    SpriteRenderer playerSpriteRenderer;
    Rigidbody2D characterRb;
    InputActionAsset inputActions;

    InputAction moveAction;
    InputAction attackAction;

    public BulletPool bulletPool;

    //attack
    public float attackDelay = 0.5f;
    bool canAttack = true;

    //�����ð�
    public float invincibleTime = 0.5f;
    public Collider2D hitCollider;
    public Collider2D hitTriggerCollider;

    //player animation
    Animator playerAnimator;
    enum State
    {
        Idle,
        Run,
        Death,
    }
    State state;

    //stat
    public int playerAttackStat = 2;
    int playerHp;
    public List<GameObject> hpImages;


    void Start()
    {
        inputActions = GetComponent<PlayerInput>().actions;

        characterRb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();

        moveAction = inputActions.FindAction("Move");
        attackAction = inputActions.FindAction("Attack");
    }

    void Update()
    {
        HandleAnimation();
        HandleAttack();
        ShowHpRemain();
    }
    private void FixedUpdate()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.magnitude > 1)
        {
            moveVector.Normalize();
        }
        characterRb.linearVelocity = moveVector * walkingSpeed;

    }
    void HandleAnimation()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.x > 0) transform.eulerAngles = new Vector3(0, 0, 0);
        else if (moveVector.x < 0) transform.eulerAngles = new Vector3(0, 180, 0);

        switch (state)
        {
            case State.Idle:
                playerHp = gameObject.GetComponent<HpController>().Hp;
                if (Mathf.Abs(moveVector.x) > 0 || Mathf.Abs(moveVector.y) > 0)
                {
                    playerAnimator.SetTrigger("Run");
                    state = State.Run;
                }
                else if (playerHp < 1)
                {
                    playerAnimator.SetTrigger("Death");
                    state = State.Death;
                }
                break;

            case State.Run:
                if (Mathf.Abs(moveVector.x) == 0 && Mathf.Abs(moveVector.y) == 0)
                {
                    playerAnimator.SetTrigger("Idle");
                    state = State.Idle;
                }
                break;

            case State.Death:
                return;
        }
    }

    void HandleAttack()
    {
        if (!canAttack) return;

        Vector2 attackVector = attackAction.ReadValue<Vector2>();
        if (Mathf.Abs(attackVector.x) > 0 || Mathf.Abs(attackVector.y) > 0)
        {
            GameObject bullet = bulletPool.GetObject();
            bullet.transform.position = transform.position;

            float angle = Mathf.Atan2(attackVector.y, attackVector.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            canAttack = false;
            Invoke("ResetAttackDelay", attackDelay);
        }
    }
    void ResetAttackDelay()
    {
        canAttack = true;
    }
    void ShowHpRemain()
    {
        if (hpImages.Count > playerHp && hpImages.Count > 0)
        {
            hpImages[hpImages.Count - 1].SetActive(false);
            hpImages.Remove(hpImages[hpImages.Count - 1]);
        }
    }
    public IEnumerator PlayerInvincible()
    {
        hitCollider.enabled = false;
        hitTriggerCollider.enabled = false;

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;

        for (float i = 0; i < invincibleTime; i += Time.deltaTime)
        {
            color.a = (color.a == 1f) ? 0f : 1f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.1f);
        }
        color.a = 1f;
        spriteRenderer.color = color;
        hitCollider.enabled = true;
        hitTriggerCollider.enabled = true;
    }
}
