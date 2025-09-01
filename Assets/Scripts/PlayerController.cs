using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Animator playerAnim;
    private bool characterIsWalking;
    private bool flipSprite;
    [SerializeField] private SpriteRenderer characterBody;
    private float moveHorizontal;
    private float moveVertical;
    private InputAction moveAction;
    [SerializeField] float movementSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
    }
    private void HandlePlayerMovement() 
    {
        float moveHorizontal = moveAction.ReadValue<Vector2>().x;
        float moveVertical = moveAction.ReadValue<Vector2>().y;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement = Vector2.ClampMagnitude(movement, 1.0f);
        rb.linearVelocity = movement * movementSpeed;

        characterIsWalking = movement.magnitude > 0.1f;
        playerAnim.SetBool("isWalking", characterIsWalking);
        flipSprite = movement.x < 0.1f;
        characterBody.flipX = flipSprite;
    }
}
