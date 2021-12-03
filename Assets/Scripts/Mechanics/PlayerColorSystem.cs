using UnityEngine;

public class PlayerColorSystem : MonoBehaviour
{
    public bool colorUp, colorDown;
    public Material[] material;
    public int x_mat;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        x_mat = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[x_mat];
        
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
            if (x_mat < 2)
            {
                x_mat++;
            } else {
                x_mat = 0;
            }
        } else if (colorDown)
        {   
            if (x_mat > 0 )
            {
                x_mat--;
            } else {
                x_mat = 2;
            } 
        } 
        
    }
}
