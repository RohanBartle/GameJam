using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig2D", menuName = "Combat/EnemyConfig 2D")]
public class EnemyConfig2D : ScriptableObject
{
    [Header("Stats")]
    public int meleDamage = 10;
    public float hitsPerSecond = 4f;
    public float meleSpeed = 20f;
    public float meleLifetime = 3f;

    [Tooltip("Random spread cone in degrees.")]
    public float spreadDegrees = 0f;

    [Header("Burst")]
    public int burstCount = 1;
    public float burstGap = 0.08f;

    [Header("Prefabs / Tags")]
    public string ignoreTag = "Enemy";
}