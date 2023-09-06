using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public Rigidbody rbBullet;
    [SerializeField] public float speedBullet;
    [SerializeField] public float speedRotation;

    public void FireBullet(Transform firePosition, Transform target)
    {
        transform.rotation = Quaternion.Euler(115f, -90f, -90f);
        transform.position = firePosition.position;
        rbBullet.isKinematic = false;

        Vector3 bulletDirection = (target.position - firePosition.position).normalized;
        rbBullet.velocity = new Vector3(bulletDirection.x * speedBullet, rbBullet.velocity.y, bulletDirection.z * speedBullet);
        rbBullet.angularVelocity = Vector3.up * speedRotation;

    }
}
