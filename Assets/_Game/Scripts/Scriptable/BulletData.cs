using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    None = 0,
    Hammer = 1,
    Axe0 = 2,
    Axe1 = 3,
    Bummerang = 4
}

[CreateAssetMenu(menuName = "Bullet Data")]
public class BulletData : ScriptableObject
{
    [SerializeField] Bullet[] bullets;

    public Bullet GetBullet(BulletType bulletType)
    {
        return bullets[(int)bulletType];
    }

}
