using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private AudioClip _footstep;
    [SerializeField] private Animator _playerAnim;
    private bool _characterIsWalking;
    private bool _flipSprite;
    private float _nextFootstepAudio = 0f;

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
        moveHorizontal = moveAction.ReadValue<Vector2>().x;
        moveVertical = moveAction.ReadValue<Vector2>().y;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement = Vector2.ClampMagnitude(movement, 1.0f);
        rb.linearVelocity = movement * movementSpeed;

        _characterIsWalking = movement.magnitude > 0.1f;
        _playerAnim.SetBool("isWalking", _characterIsWalking);

        if (_characterIsWalking)
        {
            HandleWalkingSounds();
        }

        _flipSprite = movement.x < 0.1f;
        characterBody.flipX = _flipSprite;
    }

    private void HandleWalkingSounds()
    {
        if (Time.time >= _nextFootstepAudio)
        {
            AudioManager.Instance.PlayAudio(_footstep, AudioManager.SoundType.SFX, 1f, false);
            float audioFrequency = _playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length / 2f;
            _nextFootstepAudio = Time.time + audioFrequency;
        }
    }
}
