﻿using System.Collections;
using UnityEngine;

namespace Outscal.BattleTank
{
    /// <summary>
    /// managing bullet views
    /// </summary>
    public class BulletView : MonoBehaviour
    {
        #region
        public BulletController bulletController { get; private set; }
        #endregion

        private void FixedUpdate()
        {
            bulletController.Movement();
        }

        public void SetBulletController(BulletController _bulletController)
        {
            bulletController = _bulletController;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<EnemyTankView>() != null)
            {
                //EnemyTankService.Instance.GetEnemyTankController().ApplyDamage(bulletController.bulletModel.Damage);

                EnemyTankView enemyTankView = collision.gameObject.GetComponent<EnemyTankView>();
                enemyTankView.enemyTankController.ApplyDamage(bulletController.bulletModel.Damage);
            }
            else if (collision.gameObject.GetComponent<TankView>() != null)
            {   
                TankService.Instance.GetTankController().ApplyDamage(bulletController.bulletModel.Damage);
            }
                Destroy(gameObject);
        }
    }
}