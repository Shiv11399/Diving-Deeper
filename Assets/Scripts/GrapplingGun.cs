using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float RotationSpeedModifier = 0.5f;
    [SerializeField] float MaxRotationAngle = 60f;

    float tempOffset = 180;
    void Update()
    {

        Vector3 eulerAngles = transform.eulerAngles;

        float axis = (Input.GetAxis("Horizontal"));

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
}
