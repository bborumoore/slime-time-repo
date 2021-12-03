using UnityEngine;
using System.Collections.Generic;


namespace Platformer.Mechanics
{

    public class PlayerColorSystem : MonoBehaviour
    {
        public bool colorUp, colorDown;
        public HashSet<TokenColor> materialTypes = new HashSet<TokenColor>();
        public List<Material> materials = new List<Material>();
        public int x_mat;
        SpriteRenderer rend;

        // Start is called before the first frame update
        void Start()
        {
            x_mat = 0;
            rend = GetComponent<SpriteRenderer>();
            rend.enabled = true;
            rend.material = materials[x_mat];

        }

        public void AddMaterial(TokenColor type, Material material)
        {
            if (materialTypes.Contains(type)) return;
            materialTypes.Add(type);
            materials.Add(material);
        }

        // Update is called once per frame
        void Update()
        {
            // Check for player input to change color up/down
            colorUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
            colorDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

            // Code to determine next color in the case of player input
            if (colorUp)
            {
                x_mat++;
                if (x_mat >= materials.Count)
                {
                    x_mat = 0;
                }
                rend.material = materials[x_mat];
            }
            else if (colorDown)
            {
                x_mat--;
                if (x_mat < 0)
                {
                    x_mat = materials.Count - 1;
                }
                rend.material = materials[x_mat];
            }

        }
    }
}
