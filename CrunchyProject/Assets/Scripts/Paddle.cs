using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Configuration vars
    [SerializeField] float screenWidthInUnits = 16f;
    float minX = 2f;
    float maxX = 14f;
    [SerializeField] float paddleWidthInUnits = 4f;

    // Cached references
    GameSession myGameSession;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        myGameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
        minX = paddleWidthInUnits / 2;
        maxX = screenWidthInUnits - paddleWidthInUnits / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos;
        if (myGameSession.isAutoPlayEnabled() == false)
        {
            float gameWidthInPixels = 4 * (float)Screen.height / 3;
            float xB = ((float)Screen.width - gameWidthInPixels) / 2;
            float mousePosInUnits = (Input.mousePosition.x - xB) / gameWidthInPixels * screenWidthInUnits;
            paddlePos = new Vector2(mousePosInUnits, transform.position.y);
            transform.position = paddlePos;
        }
        else
        {
            Vector2 ballPos = ball.transform.position;
            paddlePos = new Vector2(ballPos.x, transform.position.y);
            paddlePos.x += 0.5f * Mathf.Sin(5*Time.time);
        }
        paddlePos.x = Mathf.Clamp(paddlePos.x, minX, maxX);
        transform.position = paddlePos;
    }
}
