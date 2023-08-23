using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrapplingGun : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float RotationSpeedModifier = 0.5f;
    [SerializeField] float MaxRotationAngle = 60f;

    [SerializeField] bool auto = false;

    private float angle = 0;

    float tempOffset = 180;
    void Update()
    {

        Vector3 eulerAngles = transform.eulerAngles;

        float axis = (auto)? (Input.GetAxis("Horizontal")) : SinOscillator(0.5f);

        float zOffset = (eulerAngles.z + axis);

        zOffset = (zOffset > 180) ? zOffset - 360 : zOffset;

        zOffset = Mathf.Clamp(zOffset, -MaxRotationAngle, MaxRotationAngle);

        transform.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, zOffset);

        Fire();

    }

    void Fire()
    {
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), -transform.up * 4, Color.red);
        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) , -transform.up);

        if (hitInfo.collider == null) return;
        if (hitInfo.collider.tag == "Elements")
        {
            GetComponentInParent<Player>().MoveTo(hitInfo.transform);
        }
    }

    private static void DestroyElement(RaycastHit2D hitInfo) // this functionality will be moved to the element script.
    {
        Destroy(hitInfo.transform.gameObject);
    }

    private float SinOscillator(float speed)
    {
        angle += speed;
        return Mathf.Sin(angle * Mathf.Deg2Rad );
    }
}
