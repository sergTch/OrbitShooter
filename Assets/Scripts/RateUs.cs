using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUs : MonoBehaviour
{

#if UNITY_ANDROID
    private const string Link = "https://play.google.com/store/apps/details?id=com.Acrystal.OrbitShooter"; 
#elif UNITY_IPHONE
    private const string Link = "https://apps.apple.com/us/app/orbit-shooter/id1560561239";
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

    public void LoadMarket() {

        Application.OpenURL(Link);

    }
}
