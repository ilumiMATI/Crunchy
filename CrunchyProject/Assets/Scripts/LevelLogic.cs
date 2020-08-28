using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{

    [SerializeField] int breakableBlocks; // For debugging

    // Cached Reference
    SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void AddBreakableBlock()
    {
        ++breakableBlocks;
    }
    public void SubtractBreakableBlock()
    {
        --breakableBlocks;
        if (breakableBlocks == 0)
            sceneLoader.LoadNextScene();
    }
}
