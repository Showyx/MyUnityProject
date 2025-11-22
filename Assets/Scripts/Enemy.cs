using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] AudioClip _deathSound;

    public void DestroyEnemy()
    {
        AudioManager.Instance.PlayAudio(_deathSound, AudioManager.SoundType.SFX, 1.0f, false);
        Destroy(gameObject);
    }
}
