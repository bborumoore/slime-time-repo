using System.Numerics;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;
        public Vector3 scaleChangeEnemy;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var willHurtEnemy = (player.Bounds.min.y + .05f) >= enemy.Bounds.max.y;
            Debug.Log("Bounds:");
            Debug.Log(player.Bounds.center.y);
            Debug.Log(enemy.Bounds.max.y);
            
            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement();
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                        player.Bounce(2);
                        scaleChangeEnemy = new Vector2(.2f, .2f);
                        
                    }
                    else
                    {
                        player.Bounce(7);
                    }
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                    scaleChangeEnemy = new Vector2(.2f, .2f);
                }
            }
            else
            {
                Schedule<EnemyDeath>().enemy = enemy;
                scaleChangeEnemy = new Vector2(-.2f, -.2f);
                Debug.Log("You should get smaller");
            }
            player.transform.localScale += scaleChangeEnemy;
        }
    }

    /// <summary>
    /// Fired when a Player collides with a Hazard.
    /// </summary>
    /// <typeparam name="HazardCollision"></typeparam>
    public class PlayerHazardCollision : Event<PlayerEnemyCollision>
    {
        public bool destroyOnContact;
        public bool shrinkPlayer = true;
        public EnvironmentHazard hazard;
        public PlayerController player;
        public Vector3 scaleChangeEnemy;
        public override void Execute()
        {
            Debug.Log($"Player Collided with Hazard:{hazard.name}");
            if (shrinkPlayer)
            {
                if (scaleChangeEnemy == Vector3.zero)
                    scaleChangeEnemy = new Vector2(-.2f, -.2f);
                Debug.Log($"Shrinking player by {scaleChangeEnemy}");
            }
            else
            {
                if (scaleChangeEnemy == Vector3.zero)
                    scaleChangeEnemy = new Vector2(.2f, .2f);
                Debug.Log($"Increasing player size by {scaleChangeEnemy}");
            }
            player.transform.localScale += scaleChangeEnemy;
            if (destroyOnContact)
            {
                Debug.Log($"Destroying Hazzard: {hazard.name}");
                Schedule<DestroyHazard>().hazard = hazard;
            }
        }
    }
}