using UnityEngine;
using System;

namespace SpaceInvader {
    public class EventManager : MonoBehaviour {
        //public event Action<float> OnPlayerHit;

        public static EventManager Instance { get; private set; }

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //public void InvokeOnPlayerHit(float dmg) => OnPlayerHit.Invoke(dmg);
    }
}
