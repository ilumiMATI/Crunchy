using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBall : MonoBehaviour
{
    // Configuration variables
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 180f;
    [SerializeField] AudioClip[] ballClips;
    [SerializeField] float randomBounceFactor = 0f;

    // Cached Component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        myRigidBody2D.velocity = randomDirection.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        myRigidBody2D.angularVelocity = rotationSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityRandomizedAddon = new Vector2(Random.Range(-randomBounceFactor, randomBounceFactor), Random.Range(-randomBounceFactor, randomBounceFactor));

        AudioClip clip = ballClips[Random.Range(0, ballClips.Length)];
        myAudioSource.PlayOneShot(clip);
        myRigidBody2D.velocity += velocityRandomizedAddon;
    }

}
