using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform firePosition;
    [SerializeField] protected Transform currentTransform;

    protected Transform target;
    protected Vector3 directionToEnemy;
    protected string currentAnimName;
    protected bool isCanAttack = false;

    public GameObject aim;
    public Transform rightHand;
    public Renderer meshRenderer;

    public WeaponData weaponData;
    public Weapon currentWeapon;
    public ColorData colorData;
    public ColorType currentColor;
    public BulletData bulletData;
    public Bullet currentBulletPrefabs;
    public Bullet bullet;

    //-----------------------CHANGE-WEAPON-AND-COLOR-----------------------//
    public void ChangeWeapon(WeaponType newWeaponType)
    {
        // get weapon from Weapon Data
        Weapon newWeapon = weaponData.GetWeapon(newWeaponType);

        // check if weapon exist in Weapon Data
        if (newWeapon != null)
        {
            //check current weapon exist 
            if (currentWeapon != null)
            {
                // destroy current weapon
                Destroy(currentWeapon.gameObject);
            }

            // add new weapon
            currentWeapon = Instantiate(newWeapon);
            currentWeapon.transform.SetParent(rightHand);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;

            // get bulletType with this weapon
            BulletType newBulletType = weaponData.GetBulletType(newWeaponType);

            // get bullet with bulletType
            Bullet newBullet = bulletData.GetBullet(newBulletType);

            // add new bullet to currentBullet
            currentBulletPrefabs = Instantiate(newBullet);
            currentBulletPrefabs.SetActive(false);
        }
        else
        {
            // if the weapon does not exist
            Debug.LogError("Weapon " + newWeaponType + " does not exist");
        }


    }

    public void ChangeColor(ColorType newColorType)
    {
        // get new material from Color Data
        Material newMaterial = colorData.GetMaterial(newColorType);

        // check new material exist in Color Data
        if (newMaterial != null)
        {
            // add new material 
            meshRenderer.material = newMaterial;
            currentColor = newColorType;
        }
        else
        {
            // if new material does not exist in Color Data
            Debug.LogError("Material not found for ColorType: " + newColorType);
        }
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
    }

    public void ChangeAnimIdle()
    {
        ChangeAnim("idle");
    }

    public void ChangeAnimAttack()
    {
        ChangeAnim("attack");
    }
    /**********************************************************/


    //-----------------------SET-ATTACK-----------------------//
    public void Attack()
    {
        if (target != null && isCanAttack)
        {
            SetDirectionWhenAttack(this.target);
            ChangeAnimAttack();

            StartCoroutine(AttackSequence());
        }
        else
        {
            ChangeAnimIdle();
        }
    }

    protected IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(0.5f);
        if (isCanAttack)
        {
            isCanAttack = false;
            currentWeapon.SetActive(false);

            Fire();

            yield return new WaitForSeconds(0.4f);
            currentWeapon.SetActive(true);
        }
    }

    public void Fire()
    {
        bullet = Instantiate(currentBulletPrefabs);
        bullet.SetActive(true);
        bullet.FireBullet(firePosition, target);
    }

    public void SetDirectionWhenAttack(Transform target)
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }


    /**********************************************************/

    //-----------------------CHECK-ENEMY-----------------------//
    public void FoundEnemy(Transform target)
    {
        if (this.target != null)
        {
            DefoundEnemy();
        }

        this.target = target;
        isCanAttack = true;
    }

    public void DefoundEnemy()
    {
        this.target = null;
    }

    protected virtual void SetAim()
    {

    }
    /**********************************************************/


}
