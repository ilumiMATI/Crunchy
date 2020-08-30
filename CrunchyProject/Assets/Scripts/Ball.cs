using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Configuration variables
    [SerializeField] Paddle paddle;
    private Vector2 pushVector = Vector2.up;
    [SerializeField] float velocity;
    [SerializeField] GameObject pushVectorSmudge;
    [SerializeField] float periodicRotateFrequency = 1f;
    [SerializeField] float periodicRotateAmplitude = 1f;
    [SerializeField] Vector2 pushVectorSmudgeOffset;
    [SerializeField] float rotationSpeed = 180f;
    [SerializeField] AudioClip[] ballClips;
    [SerializeField] float randomBounceFactor = 0f;
    [SerializeField] GameObject redirectObject;
    [SerializeField] int wallHitsToSpawnRedirect = 3;
    private int wallHitCount = 0;
    

    // State
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached Component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    GameSession myGameSession;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myGameSession = FindObjectOfType<GameSession>();
        pushVectorSmudge = Instantiate(pushVectorSmudge, transform.position + new Vector3(pushVectorSmudgeOffset.x, pushVectorSmudgeOffset.y, -5f), transform.rotation);
        pushVectorSmudge.transform.parent = transform; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
            MakePushVectorStatic();
        }
    }

    private void MakePushVectorStatic()
    {
        pushVectorSmudge.transform.RotateAround(transform.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (!hasStarted)
        {
            PeriodicPushVectorRotate();
        }
    }

    private void PeriodicPushVectorRotate()
    {
        pushVectorSmudge.transform.RotateAround(transform.position, Vector3.forward, periodicRotateAmplitude * Mathf.Sin(periodicRotateFrequency * Time.timeSinceLevelLoad + Mathf.PI / 2));
        pushVector = Redirect.RotateVector2(pushVector, periodicRotateAmplitude * Mathf.Deg2Rad * Mathf.Sin(periodicRotateFrequency * Time.timeSinceLevelLoad + Mathf.PI / 2));
    }

    private void FixBallVelocity()
    {
        myRigidBody2D.velocity = myRigidBody2D.velocity.normalized * velocity;
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = pushVector * velocity;
            hasStarted = true;
            pushVectorSmudge.SetActive(false);
            Destroy(pushVectorSmudge);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x,paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (hasStarted)
        {
            Vector2 velocityRandomizedAddon = new Vector2(
                Random.Range(-randomBounceFactor, randomBounceFactor),
                Random.Range(-randomBounceFactor, randomBounceFactor));
            AudioClip clip = ballClips[Random.Range(0, ballClips.Length)];

            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityRandomizedAddon;
            FixBallVelocity();
            Invoke("FixBallVelocity", 0.1f); // Making sure wrong velocity doesn't last
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallHitCount++;
            Invoke("CheckBallMovement", 0.1f);
        } 
        else if (collision.gameObject.CompareTag("Untagged")) 
        { 
            // Skip
        }
        else
        {
            wallHitCount = 0;
        }
    }
    private void CheckBallMovement()
    {
        if (wallHitCount >= wallHitsToSpawnRedirect && myRigidBody2D.velocity.y < myGameSession.GetMinimumBallSpeedPerUnit())
        {
            SpawnHorizontalRedirect();
            wallHitCount = 0;
        }
    }

    private void SpawnHorizontalRedirect()
    {
        float Sx = Mathf.Abs(transform.position.x - 8f);
        float Sy = myRigidBody2D.velocity.y * Sx / Mathf.Abs(myRigidBody2D.velocity.x);
        float time = Mathf.Abs(myRigidBody2D.velocity.x) / Sx;

        Vector2 spawnPosition = new Vector2(8f, transform.position.y + Sy);
        GameObject redirectSpawnedObject = Instantiate(redirectObject, spawnPosition, new Quaternion());

        StartCoroutine(StartHidingAnimationForRedirect(redirectSpawnedObject, time));
        Destroy(redirectSpawnedObject, time + 2f);
    }
    private IEnumerator StartHidingAnimationForRedirect(GameObject redirectObject, float time)
    {
        yield return new WaitForSeconds(time);
        var colComponent = redirectObject.GetComponent<CircleCollider2D>();
        redirectObject.GetComponent<Animator>().SetTrigger("isBeingDestroyed");
        Destroy(colComponent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Redirect"))
        {
            StartCoroutine(StartHidingAnimationForRedirect(collision.gameObject, 0f));
            Destroy(collision.gameObject, 2f); // Destroy Redirect ball

            Vector2 ballVelocity = myRigidBody2D.velocity;

            // Random angle
            //float redirectAngle = collision.gameObject.GetComponent<Redirect>().GetRandomRedirectAngleRad(); // Reading the angle from Redirect object
            //Vector2 rotatedBallVelocity = Redirect.RotateVector2(ballVelocity, redirectAngle);

            // Random Up Vector
            Vector2 rotatedBallVelocity = Redirect.RotateVector2Up(ballVelocity, 70f);

            myRigidBody2D.velocity = rotatedBallVelocity;
        }
    }
}
