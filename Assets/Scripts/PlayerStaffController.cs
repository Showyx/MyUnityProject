using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStaffController : MonoBehaviour
{
    private InputAction _fireAction;
    [SerializeField] private float _fireRate;
    private float _nextFireTime;

    private void Awake()
    {
        _fireAction = InputSystem.actions.FindAction("Attack");
    }
    private void Update()
    {
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
        Debug.Log("Bang!");
    }
}
