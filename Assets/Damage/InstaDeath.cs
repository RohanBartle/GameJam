using UnityEngine;

public class InstaDeath : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private float damageInterval = 100f;
    [SerializeField] private string requiredTag = "Player";

    private float lastDamageTime = -Mathf.Infinity;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryDamage(collision.collider);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryDamage(collision.collider);
    }

    private void TryDamage(Collider2D collider)
    {
        if (!string.IsNullOrEmpty(requiredTag) && !collider.CompareTag(requiredTag))
            return;

        PlayerHealth health = collider.GetComponent<PlayerHealth>();

        if (health == null)
            health = collider.GetComponent<PlayerHealth>();

        if (health == null)
            health = collider.GetComponent<PlayerHealth>();

        if (Time.time >= lastDamageTime + damageInterval)
        {
            health.TakeDamage(damage);
            lastDamageTime = Time.time;

        }

    }


}
