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

    public float attackDelay = 0.5f;
    bool canAttack = true;

    //player animation
    Animator playerAnimator;
    void Start()
    {
        Cursor.visible = false; //마우스 커서 끔

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

        if (canAttack)
        {
            HandleAttack();
            canAttack = false;
            Invoke("ResetAttackDelay", attackDelay);
        }
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

        if (Mathf.Abs(moveVector.x) > 0 || Mathf.Abs(moveVector.y) > 0)
        {
            playerAnimator.SetTrigger("Run");
        }
        else
        {
            playerAnimator.SetTrigger("Idle");
        }
    }

    void HandleAttack()
    {
        Vector2 attackVector = attackAction.ReadValue<Vector2>();

        if (Mathf.Abs(attackVector.x) > 0 || Mathf.Abs(attackVector.y) > 0)
        {
            GameObject bullet = bulletPool.GetObject();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().Velocity = new Vector2(10, 10) * attackVector;
        }
    }
    void ResetAttackDelay()
    {
        canAttack = true;
    }
}
