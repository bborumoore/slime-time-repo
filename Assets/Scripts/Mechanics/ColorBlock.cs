using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class ColorBlock : MonoBehaviour
    {
        public Material wantedColor;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collided");
            if (collision.transform.tag == "Player")
            {
                GameObject player = collision.transform.gameObject;
                int x_mat = player.GetComponent<PlayerColorSystem>().x_mat;
                Material currentColor = player.GetComponent<PlayerColorSystem>().materials[x_mat];

                if (currentColor == wantedColor)
                {
                    StartCoroutine(PhaseThrough());
                }
            }
        }

        IEnumerator PhaseThrough()
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(5f);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
