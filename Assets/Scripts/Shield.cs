using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Animator anim;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Appear()
    {
        active = true;
        anim.Play("appear");
    }
    public void Destroy()
    {
        active = false;
        anim.Play("destroy");
    }
}
