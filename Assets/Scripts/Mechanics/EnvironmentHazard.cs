using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class EnvironmentHazard : MonoBehaviour
    {
        [SerializeField] bool shrinkPlayer;
        [SerializeField] bool destroyOnContact;
        [SerializeField] float countDown = 1.0f;
        [SerializeField] Vector2 playerSizeChange;
        public AudioClip collisionSound;

        private bool acceptCollisions = true;
        private float time;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;

        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (!acceptCollisions) return;
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                acceptCollisions = false;
                var ev = Schedule<PlayerHazardCollision>();
                ev.player = player;
                ev.hazard = this;
                ev.shrinkPlayer = shrinkPlayer;
                ev.destroyOnContact = destroyOnContact;
                ev.scaleChangeEnemy = playerSizeChange;
                if (destroyOnContact)
                    countDown = float.MaxValue;
            }
        }

        private void Update()
        {
            if (!acceptCollisions)
            {
                time += Time.deltaTime;
                if (time >= countDown)
                {
                    time = 0.0f;
                    acceptCollisions = true;
                }
            }
        }
    }
}