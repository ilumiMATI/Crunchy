using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseCollider : MonoBehaviour
{
    // Cached Reference
    SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sceneLoader.LoadGameOverScene();
    }
}