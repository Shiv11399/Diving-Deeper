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
    }

    void Fire(Vector2 direction)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction);//, shootDist, hittableLayer);
        if (hitInfo != null)
        {
            if (hitInfo.collider.tag == "Player")
            {
                //lineRenderer.enabled = true;
                //StartCoroutine(Shoot1());
            }
        }


}
