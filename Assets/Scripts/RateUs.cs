using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUs : MonoBehaviour
{

private var Link;

#if UNITY_ANDROID
    private const string Link = "https://answers.unity.com/questions/1116891/onclick-open-url.html"; 
#elif UNITY_IPHONE
    private const string Link = "https://answers.unity.com/questions/1116891/onclick-open-url.html";
#else
    private const string Link = "unexpected_platform";
#endif

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RateUs() {

Application.OpenURL(Link);

    }
}
