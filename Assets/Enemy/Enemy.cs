using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    private Transform target;

    public float speed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
