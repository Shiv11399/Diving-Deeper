using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class GrapplingGun : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float RotationSpeedModifier = 0.5f;
    [SerializeField] float MaxRotationAngle = 60f;
    [SerializeField] GameObject BulletPrefab;

    [SerializeField] bool auto = false;


    private bool firing = false;

    private float angle = 0;

    float tempOffset = 180;
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Fire();
        }
        if(Player.PlayerState != State.Moving && !firing) GunMovingLogic(); // stop moving the gun if the player is moving or the gun is aiming.

    }

    private void GunMovingLogic()
    {
        Vector3 eulerAngles = transform.eulerAngles;

        float axis = (auto) ? (Input.GetAxis("Horizontal")) : SinOscillator(0.5f);

        float zOffset = (eulerAngles.z + axis);

        zOffset = (zOffset > 180) ? zOffset - 360 : zOffset;

        zOffset = Mathf.Clamp(zOffset, -MaxRotationAngle, MaxRotationAngle);

        transform.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, zOffset);
    }

    void Fire()
    {
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), -transform.up * 4, Color.red);
        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) , -transform.up);
        
        if (hitInfo.collider == null) return;

        if (hitInfo.collider.tag == "Elements")
        {
            var Bullet = Instantiate(BulletPrefab, transform.position,transform.rotation);
            StartCoroutine(ShootBullet(Bullet, hitInfo.transform));
            GetComponentInParent<Player>().MoveTo(hitInfo.transform);
        }
        ScoreManager.ScoreChanged.Invoke(ScoreType.Attempts, -1);
    }

    IEnumerator ShootBullet(GameObject bullet, Transform target)
    {
        firing = true;
        float time = 0;
        while (time < 1)
        {
            yield return new WaitForSeconds(0.05f);
            bullet.transform.position = Vector2.Lerp(transform.position, target.position, time);
            time += 0.05f;
        }
        firing = false; 
    }

    private float SinOscillator(float speed)
    {
        angle += speed;
        return Mathf.Sin(angle * Mathf.Deg2Rad );
    }
}
