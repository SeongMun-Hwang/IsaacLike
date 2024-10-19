using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 7f;

    //player input movement
    SpriteRenderer playerSpriteRenderer;
    CharacterController characterController;
    InputAction moveAction;

    //player animation
    Animator playerAnimator;


    void Start()
    {
        Cursor.visible = false; //마우스 커서 끔

        InputActionAsset inputActions=GetComponent<PlayerInput>().actions;

        characterController= GetComponent<CharacterController>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();

        moveAction = inputActions.FindAction("Move");
    }

    void Update()
    {
        Vector2 moveVector=moveAction.ReadValue<Vector2>();
        //player flip by x axiz movement

        if (moveVector.x > 0) playerSpriteRenderer.flipX = false;
        else if(moveVector.x < 0) playerSpriteRenderer.flipX = true;

        if (Mathf.Abs(moveVector.x) > 0 || Mathf.Abs(moveVector.y) > 0)
        {
            playerAnimator.SetTrigger("Run");
        }
        else
        {
            playerAnimator.SetTrigger("Idle");
        }

        if (moveVector.magnitude > 1)
        {
            moveVector.Normalize();
        }
        moveVector = moveVector * walkingSpeed * Time.deltaTime;
        moveVector = transform.TransformDirection(moveVector);
        characterController.Move(moveVector);
    }
}
