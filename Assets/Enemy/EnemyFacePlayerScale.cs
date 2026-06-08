using UnityEngine;

public class EnemyFacePlayerScale : MonoBehaviour
{
    public Transform player;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (player != null)
        {
            if (player.position.x < transform.position.x)
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            else
                transform.localScale = originalScale;
        }
    }
}