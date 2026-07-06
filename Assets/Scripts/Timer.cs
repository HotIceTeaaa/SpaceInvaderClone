using UnityEngine;

namespace SpaceInvader
{
    public class Timer
    {
        private float _timer;
        private float _cooldown;

        public Timer(float cooldown) {
            _timer = 0f;
            _cooldown = cooldown;

            ResetTimer();
        }

        public void ResetTimer() {
            _timer = _cooldown;
        }

        public void DecrementTimer() {
            _timer -= Time.deltaTime;
        }

        public bool isDone() {
            return _timer <= 0f;
        }
    }
}
