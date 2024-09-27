using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outscal.BattleTank
{
    /// <summary>
    /// bullet model class
    /// </summary>
    public class BulletModel
    {
        #region properties
        public float BulletForce { get; private set; }
        public int Damage { get; private set; }
        public BulletType Type { get; private set; }
        public BulletController bulletController { get; private set; }
        #endregion

        public BulletModel(BulletScriptableObject bulletScriptableObject)
        {
            BulletForce = bulletScriptableObject.bulletForce;           
            Damage = bulletScriptableObject.bulletDamage;
            Type = bulletScriptableObject.bulletType;
        }

        public void SetBulletController(BulletController _bulletController)
        {
            bulletController = _bulletController;
        }
    }
}