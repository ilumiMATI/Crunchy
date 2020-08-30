using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBall : MonoBehaviour
{
    // Configuration variables
    [Header("Movement")]
    [SerializeField] float velocity;
    [SerializeField] float rotationSpeed = 180f;
    [SerializeField] float randomBounceFactor = 0f;
    [Header("Audio")]
    [SerializeField] AudioClip[] ballClips;
    

    // Cached Component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        // Setting up refs
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();

        // Pre-action setup
        SetRandomVelocityDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Visual rotate effect
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayRandomAudioClip();
        RandomizeVelocity();
    }

    private void SetRandomVelocityDirection()
    {
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        myRigidBody2D.velocity = randomDirection.normalized * velocity;
    }

    private void RandomizeVelocity()
    {
        Vector2 velocityRandomizedAddon = new Vector2(
            Random.Range(-randomBounceFactor, randomBounceFactor),
            Random.Range(-randomBounceFactor, randomBounceFactor));

        myRigidBody2D.velocity += velocityRandomizedAddon;
    }

    private void PlayRandomAudioClip()
    {
        AudioClip clip = ballClips[Random.Range(0, ballClips.Length)];
        myAudioSource.PlayOneShot(clip);
    }
}
