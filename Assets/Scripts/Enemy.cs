using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EntityHealth))]
public class Enemy : MonoBehaviour
{
    [SerializeField] AudioClip _deathSound;
    private EntityHealth _entityHealth;
    private NavMeshAgent _agent;
    private GameObject _target;

    private void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
    }

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _entityHealth.OnDeath += DestroyEnemy;
    }

    private void Update()
    {
        _agent.SetDestination(_target.transform.position);
    }

    public void DestroyEnemy()
    {
        AudioManager.Instance.PlayAudio(_deathSound, AudioManager.SoundType.SFX, 1.0f, false);
        OnDisable();
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        _entityHealth.OnDeath -= DestroyEnemy;
    }
}
