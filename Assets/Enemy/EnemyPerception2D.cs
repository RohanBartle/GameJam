using UnityEngine;

public class EnemyPerception2D : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private string playerTag = "Player";

    [Header("Vision")]
    [SerializeField] private float detectRange = 12f;

    [SerializeField] private bool useFieldOfView = false;

    [Range(0f, 180f)]
    [SerializeField] private float fovHalfAngle = 70f;

    [SerializeField] private LayerMask obstacleLayers;

    [SerializeField] private float sightMemoryTime = 2f;

    [Header("Hearing")]
    [SerializeField] private float hearingRadius = 8f;

    [SerializeField] private float hearingMemoryTime = 3f;

    [SerializeField] private LayerMask hearingObstacleLayers;

    public Transform Player { get; private set; }

    public bool HasLineOfSight { get; private set; }

    public Vector2 LastKnownPlayerPos { get; private set; }

    public float LastSeenTime { get; private set; } = -999f;

    public Vector2 LastHeardPos { get; private set; }

    public float LastHeardTime { get; private set; } = -999f;


    public bool HasRecentSightMemory => Time.time - LastSeenTime <= sightMemoryTime;

    public bool HasRecentHearingMemory => Time.time - LastHeardTime <= hearingMemoryTime;

    private void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            Player = playerObj.transform;
        }

        if (hearingObstacleLayers.value == 0)
        {
            hearingObstacleLayers = obstacleLayers;
        }
    }

    public bool CheckSight(Vector2 forwardDir)
    {
        HasLineOfSight = false;

        if (Player == null)
            return false;

        Vector2 origin = transform.position;
        Vector2 toPlayer = (Vector2)Player.position - origin;
        float distance = toPlayer.magnitude;

        if (distance > detectRange)
            return false;

        Vector2 dir = toPlayer / Mathf.Max(distance, 0.0001f);

        if (useFieldOfView)
        {
            Vector2 fwd = forwardDir.sqrMagnitude < 0.0001f ? Vector2.up : forwardDir.normalized;
            float angle = Vector2.Angle(fwd, dir);
            if (angle > fovHalfAngle)
                return false;
        }

        RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, obstacleLayers);
        if (hit.collider != null)
        {
            return false;
        }

        HasLineOfSight = true;
        LastKnownPlayerPos = Player.position;
        LastSeenTime = Time.time;
        return true;
    }

    public void RegisterSound(Vector2 soundPosition, float loudness = 1f)
    {
        float maxHearingDist = hearingRadius * Mathf.Max(0.1f, loudness);
        Vector2 origin = transform.position;
        Vector2 toSound = soundPosition - origin;
        float distance = toSound.magnitude;

        if (distance > maxHearingDist)
            return;

        RaycastHit2D hit = Physics2D.Raycast(origin, toSound.normalized, distance, hearingObstacleLayers);
        if (hit.collider != null)
        {
            return;
        }

        LastHeardPos = soundPosition;
        LastHeardTime = Time.time;
    }

    public void ForceInvestigatePoint(Vector2 worldPos)
    {
        LastHeardPos = worldPos;
        LastHeardTime = Time.time;
    }
}