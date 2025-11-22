using UnityEngine;
using UnityEngine.WSA;

public class Projectile : MonoBehaviour
{
    private float _travelSpeed = 4;
    private float _damage = 1;
    [SerializeField] private Rigidbody2D _rb;

    public void InitializeProjectile(Vector2 direction)
    {
        Launch(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            DestroyProjectile();
        }
    }

    private void Launch(Vector2 direction)
    {
        Vector2 movement = direction.normalized * _travelSpeed;
        _rb.linearVelocity = movement;
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
