using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public int cherryNum = 0;
    public int gemNum = 0;
    private LevelManager()
    {
    }

    private void Awake()
    {
        instance = new LevelManager();
    }

    public static LevelManager GetInstance()
    {
        return instance;
    }
}
