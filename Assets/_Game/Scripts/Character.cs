using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string currentAnimName;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform firePosition;
    [SerializeField] private Renderer meshRenderer;

    public Transform target;
    public Vector3 directionToEnemy;
    public bool isAttack = false;

    public WeaponData weaponData;
    public WeaponType weapon;
    public GameObject handleWeapon;

    public ColorData colorData;
    public ColorType color;
    public Material newMaterial;

    public GameObject bullet;


    //-----------------------CHANGE-WEAPON-AND-COLOR-----------------------//
    public void ChangeWeapon(WeaponType weaponType)
    {
        if (handleWeapon != null)
        {
            Destroy(handleWeapon);
            handleWeapon = null;
        }

        weapon = weaponType;
        handleWeapon = Instantiate(weaponData.GetWeapon(weaponType));
        handleWeapon.transform.SetParent(rightHand);
        handleWeapon.transform.localPosition = Vector3.zero;
        handleWeapon.transform.localRotation = Quaternion.identity;
    }

    public void ChangeColor(ColorType colorType)
    {
        color = colorType;
        newMaterial = colorData.GetMaterial(color);
        meshRenderer.material = newMaterial;
    }
    /**********************************************************/


    //-----------------------ANIMATION-----------------------//
    public void ChangeAnim(string animName)
    {
        if (animName != currentAnimName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(animName);
        }
    }

    public void ChangeAnimRun()
    {
        ChangeAnim("run");
        isAttack = false;
    }

    public void ChangeAnimIdle()
    {
        ChangeAnim("idle");
    }

    public void ChangeAnimAttack()
    {
        if (!isAttack)
        {
            ChangeAnim("attack");
            Invoke(nameof(SetStateOfWeaponFalse), 0.55f);
            isAttack = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            CancelInvoke(nameof(SetStateOfWeaponFalse));
        }
    }
    /**********************************************************/


    //-----------------------SET-ATTACK-ENEMY-----------------------//
    public void SetDirectionWhenAttack(Transform target)
    {
        Vector3 newDirection = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void SetStateOfWeaponFalse()
    {
        Fire();
        handleWeapon.SetActive(false);
        Invoke(nameof(SetStateOfWeaponTrue), 1.55f);
    }

    public void SetStateOfWeaponTrue()
    {
        handleWeapon.SetActive(true);
    }

    public void SetBullet()
    {
        bullet = Instantiate(handleWeapon, Vector3.zero, Quaternion.identity);
        bullet.SetActive(false);
    }

    public void ResetBullet()
    {
        bullet.transform.position = Vector3.zero;
        bullet.SetActive(false);
    }

    public void Fire()
    {
        bullet.SetActive(true);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.FireBullet(firePosition, target);
        Invoke(nameof(ResetBullet), 1.55f);
    }
    /**********************************************************/

    //-----------------------CHECK-ENEMY-----------------------//
    public void FoundEnemy(Transform target)
    {
        this.target = target;
    }

    public void DefoundEnemy()
    {
        this.target = null;
    }
    /**********************************************************/


}
