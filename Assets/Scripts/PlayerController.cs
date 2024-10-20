using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 7f;

    //player input movement
    SpriteRenderer playerSpriteRenderer;
    Rigidbody2D characterRb;
    InputActionAsset inputActions;

    InputAction moveAction;
    InputAction attackAction;

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
    }

    void Update()
    {
        HandleAnimation();
        HandleBullet();             
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
        if (moveVector.x > 0) playerSpriteRenderer.flipX = false;
        else if (moveVector.x < 0) playerSpriteRenderer.flipX = true;

        if (Mathf.Abs(moveVector.x) > 0 || Mathf.Abs(moveVector.y) > 0)
        {
            playerAnimator.SetTrigger("Run");
        }
        else
        {
            playerAnimator.SetTrigger("Idle");
        }
    }

    void HandleBullet()
    {

        Vector2 bulletVector2 = new Vector2();

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            Debug.Log("Up");
            bulletVector2 = new Vector2(0, 10);
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            bulletVector2 = new Vector2(0, -10);
            Debug.Log("Down");
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            bulletVector2 = new Vector2(10, 0);
            Debug.Log("Right");
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            bulletVector2 = new Vector2(-10, 0);
            Debug.Log("Left");
        }

        //GameObject bullet = GameManager.Instance.bulletPool.GetObject();
        //bullet.transform.position = transform.position;
        //bullet.GetComponent<Bullet>().Velocity = bulletVector2;

    }
    void OnAttack(InputValue value)
    {
        Debug.Log("Attack value : " + value.Get<Vector2>());
    }
}
