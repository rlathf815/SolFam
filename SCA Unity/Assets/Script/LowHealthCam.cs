using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leguar.LowHealth;

public class LowHealthCam : MonoBehaviour
{
    // Start is called before the first frame update
    float HP ;
    public LowHealthController shaderControllerScript;
    public FastGlitch glitch;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HP = PlayerStats.Instance.Health;
        Debug.Log("CurrentHp=" + HP);
        shaderControllerScript.SetPlayerHealthInstantly(HP / 3f);
        if(HP==1) {
            glitch.PixelGlitch = 0.3f;
            glitch.FrameGlitch = 0.1f;
            glitch.ChromaticGlitch = 0.2f;
        }
        else if(HP==2)
        {
            glitch.PixelGlitch = 0.06f;
            //glitch.FrameGlitch = 0.05f;
            glitch.ChromaticGlitch = 0.05f;

        }
       
    }

}
