using UnityEngine;

namespace SpaceInvader
{
    public class SFXManager : MonoBehaviour {
        public static SFXManager Instance { get; private set; }
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _loopedAudioSource;

        [Header("Player Related SFX")]
        [SerializeField] private AudioClip _playerShootSFX;
        [SerializeField] private AudioClip _playerDeathSFX;

        [Header("Enemy Related SFX")]
        [SerializeField] private AudioClip _enemyDeathSFX;

        [Header("Projectile Related SFX")]
        [SerializeField] private AudioClip _projectileHitSFX;

        [Header("UI SFX")]
        [SerializeField] private AudioClip _buttonClickSFX;
        [SerializeField] private AudioClip _buttonHoverSFX;

        [Header("Other SFX")]
        [SerializeField] private AudioClip _scoreIncrementsSFX;
        

        private void Awake() {
            // Enforce the Singleton pattern
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySFX(SFX sound)
        {
            switch (sound)
            {
                case SFX.ButtonClick:
                    _audioSource.PlayOneShot(_buttonClickSFX);
                    break;
                case SFX.ButtonHover:
                    _audioSource.PlayOneShot(_buttonHoverSFX);
                    break;
                case SFX.PlayerDeath:
                    _audioSource.PlayOneShot(_playerDeathSFX);
                    break;
                case SFX.PlayerShoot:
                    _audioSource.PlayOneShot(_playerShootSFX);
                    break;
                case SFX.EnemyDeath:
                    _audioSource.PlayOneShot(_enemyDeathSFX);
                    break;
                case SFX.ProjectileHit:
                    _audioSource.PlayOneShot(_projectileHitSFX);
                    break;
            }
        }

        public void PlayLoopedSFX(SFX sound)
        {
            _loopedAudioSource.mute = false;

            switch (sound)
            {
                case SFX.ScoreIncrements:
                    _loopedAudioSource.clip = _scoreIncrementsSFX;
                    _loopedAudioSource.Play();
                    break;
            }
        }

        public void MuteLoopedSFX()
        {
            _loopedAudioSource.mute = true;
        }
    }

}
