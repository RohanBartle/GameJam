using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour

{

    public float speed;

    private float horizontal;

    public Rigidbody2D rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(speed * horizontal , rb.linearVelocity.y);


        if (horizontal > 0)
        {
            gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0);
        }
        if (horizontal < 0)
        {
            gameObject.transform.localScale = new Vector3(-0.25f, 0.25f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {

        }
    }

        private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }
}
