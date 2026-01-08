using UnityEngine;
using UnityEngine.WSA;

public class Projectile : MonoBehaviour
{
    private float _travelSpeed = 4;
    private float _damage = 1;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ParticleSystem _hitParticles;
    [SerializeField] private AudioClip _enemyHitSound;

    public void InitializeProjectile(Vector2 direction)
    {
        Launch(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DealDamage(collision.gameObject);
            DestroyProjectile();
        }
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
        ParticleSystem hitParticles = Instantiate(_hitParticles, transform.position, Quaternion.identity);
        Destroy(hitParticles.gameObject, 1f);
        Destroy(gameObject);
    }

    private void DealDamage(GameObject target)
    {
        if (target.TryGetComponent(out EntityHealth entityHealth))
        {
            entityHealth.LoseHealth(_damage);
            AudioManager.Instance.PlayAudio(_enemyHitSound, AudioManager.SoundType.SFX, 1.0f, false);
        }
    }
}
