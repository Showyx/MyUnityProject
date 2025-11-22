using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStaffController : MonoBehaviour
{
    [SerializeField] Projectile _projectile;
    [SerializeField] AudioClip _shootSound;
    [SerializeField] Transform _tip;
    [SerializeField] private float _fireRate;
    private InputAction _fireAction;
    private float _nextFireTime;
    private Vector2 _lookDirection;

    private void Awake()
    {
        _fireAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        SetLookDirection();
        if (Time.timeScale == 0f)
            return;

        RotateStaff();
        if (_fireAction.IsPressed() && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + 1f / _fireRate;
            Shoot();
        }
    }

    private void RotateStaff()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 lookDirection = (mousePosition - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Shoot()
    {
        AudioManager.Instance.PlayAudio(_shootSound, AudioManager.SoundType.SFX, 0.4f, false);
        Projectile newProjectile = Instantiate(_projectile, _tip.position, Quaternion.identity);
        newProjectile.InitializeProjectile(_lookDirection);
    }

    private void SetLookDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _lookDirection = (mousePosition - (Vector2)transform.position).normalized;
    }
}
