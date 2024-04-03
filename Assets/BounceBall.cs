using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour
{
    public Color[] Colors;

    public SpriteRenderer spriteRenderer;
    public Vector2 direction;
    public float speed;

    void Start()
    {
        StartBounceBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x) > 15 || Mathf.Abs(transform.position.y) > 15)
            StartBounceBall();

        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        BounceBallOnCollision(collision);
        ChangeColorToRandom();
    }

    void StartBounceBall()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void ChangeColorToRandom()
    {
        spriteRenderer.color = Colors[(int)Random.Range(0, Colors.Length)];
    }

    void BounceBallOnCollision(Collision2D collision)
    {
        var collisionVector = collision.contacts[0].point;
        Debug.Log($"Ball Collision {collisionVector.x} {collisionVector.y}");
        var normal = (Vector2)transform.position - collisionVector;

        direction = Vector2.Reflect(direction, normal.normalized).normalized;
        direction = new Vector2(direction.x + Random.Range(-0.1f, 0.1f), direction.y + Random.Range(-0.1f, 0.1f)).normalized;
    }
}
