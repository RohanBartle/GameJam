using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{

    [SerializeField] private GameObject weaponPivot;

    private Vector2 aimPoint;

    void OnAim(InputValue input)
    {
        aimPoint = input.Get<Vector2>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(aimPoint) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponPivot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
