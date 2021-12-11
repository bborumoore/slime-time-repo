using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class ControlGems : MonoBehaviour
{
    public GameObject redGem;
    public GameObject blueGem;
    public GameObject greenGem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGem(TokenColor gem)
    {
        if(gem == TokenColor.red)
        {
            redGem.SetActive(true);
        }
        else if (gem == TokenColor.blue)
        {
            blueGem.SetActive(true);
        }
        else if (gem == TokenColor.green)
        {
            greenGem.SetActive(true);
        }
    }
}
