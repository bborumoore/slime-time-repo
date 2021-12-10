using System;
using System.Collections;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        [SerializeField] int NextLevel;
        
        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                var ev = Schedule<PlayerEnteredVictoryZone>();
                ev.victoryZone = this;
                StartCoroutine(GoToNextLevel());
            }
        }

        private IEnumerator GoToNextLevel()
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(NextLevel);
        }
    }
}