using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration variables
    [Header("Audio")]
    [SerializeField] AudioClip breakClip;

    [Header("Breaking")]
    [SerializeField] GameObject blockParticlesVFX;
    [SerializeField] Sprite[] hitSprites;

    // Cached reference
    LevelLogic levelLogic;
    GameSession gameStatus;

    // State vars
    private int currentHits = 0; 

    void Start()
    {
        levelLogic = FindObjectOfType<LevelLogic>();
        if (CompareTag("Breakable"))
        {
            levelLogic.AddBreakableBlock();
        }
        gameStatus = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        currentHits++;
        if (currentHits >= hitSprites.Length)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = currentHits - 1;

        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Hit Sprite missing from the block " + gameObject.name);
        }
    }

    private void DestroyBlock()
    { 
        AudioSource.PlayClipAtPoint(breakClip, Camera.main.transform.position);
        TriggerBlockParticlesVFX();
        Destroy(gameObject);
        levelLogic.SubtractBreakableBlock();
        gameStatus.AddToScore();
    }

    private void TriggerBlockParticlesVFX()
    {
        GameObject particles = Instantiate(blockParticlesVFX, transform.position + new Vector3(0.5f,0.5f,-2), transform.rotation);
        Destroy(particles, 2f);
    }
}
