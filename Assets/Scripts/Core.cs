using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public MenuManager menuObj;
    public Game gameObj;

    public static MenuManager menu;
    public static Game game;

    // Start is called before the first frame update
    void Start()
    {
        game = gameObj;
        menu = menuObj;
    }
}
