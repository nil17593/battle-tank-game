using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outscal.BattleTank
{
    /// <summary>
    /// this class instantiates the tank model in game
    /// </summary>
    public class TankController
    {
        #region components
        private Rigidbody rigidbody;
        private DestroyGround destroyGround;
        #endregion

        #region properties
        public TankModel TankModel { get; private set; }
        public TankView TankView { get; private set; }
        #endregion

        #region referances of other scripts
        private EnemyTankController enemyTankController;
        #endregion

        public TankController(TankModel tankModel, TankView tankPrefab)
        {
            TankModel = tankModel;     
            TankView = GameObject.Instantiate<TankView>(tankPrefab);
            rigidbody = TankView.GetComponent<Rigidbody>();
            TankView.SetTankController(this);
            TankModel.SetTankController(this);
            CameraController.Instance.SetTarget(TankView.transform);
            Debug.Log(TankView);
            SubscribeEvents();
        }

        //subscribing bullets fired event
        private void SubscribeEvents()
        {
            AchievementService.Instance.OnPlayerFiredBullet += UpdateBulletsFiredCounter;
        }

        //counter for fired bullets to unlock achievements
        private void UpdateBulletsFiredCounter()
        {
            TankModel.bulletsFired += 1;          
            // PlayerPrefs.SetInt("BulletsFired", tankModel.bulletFired);
            // Debug.Log(PlayerPrefs.GetInt("BulletsFired"));
            Debug.Log(TankModel.bulletsFired);
            AchievementService.Instance.GetAchievementController().CheckForBulletsfiredAchievement();

        }

        //tank movement
        public void TankMovement(float movement,int speed)
        {
            Vector3 mov = TankView.transform.position;
            mov += movement * speed * Time.deltaTime * TankView.transform.forward;
            rigidbody.MovePosition(mov);
            //TankView.tankMovementVFX.Play();
            TankService.Instance.GetPlayerPos(TankView.transform);
        }

        //tank rotation
        public void TankRotation(float rotation ,float rotationSpeed)
        {
            Vector3 vector = new Vector3(0f, rotation * TankModel.rotationSpeed, 0f);
            Quaternion angle = Quaternion.Euler(vector * Time.fixedDeltaTime);
            rigidbody.MoveRotation(angle * rigidbody.rotation);
        }
        
        //tank shooting
        public void ShootBullet()
        {
            AchievementService.Instance.InvokeOnPlayerFiredBulletEvent();
            BulletService.Instance.CreateNewBullet(GetFiringPosition(), GetFiringAngle(), GetBullet());
        }

        //unsubscribing from event
        public void UnSubscribeEvent()
        {
            AchievementService.Instance.OnPlayerFiredBullet -= UpdateBulletsFiredCounter;
        }

        //player tank will take damage 
        public void ApplyDamage(int damage)
        {
            TankModel.Health-=damage;
            UIManager.Instance.UpdateHealthText(TankModel.Health);
            if (TankModel.Health <= 0)
            {
                Dead();
            }
        }
        //triggers when player dead
        public void Dead()
        {
            TankService.Instance.DestroyTank(this);
        }
        //destroy tank model and view also after player death
        public void DestroyController()
        {
            TankModel.DestroyModel();
            TankView.DestroyView();
            TankModel = null;
            TankView = null;
            rigidbody = null;
            UnSubscribeEvent();
        }

        //returning bullet firing position
        public Vector3 GetFiringPosition()
        {
            return TankView.BulletShootPoint.position;
        }

        //returning firing angle
        public Quaternion GetFiringAngle()
        {
            return TankView.transform.rotation;
        }

        //returning bullet scriptable object
        public BulletScriptableObject GetBullet()
        {
            return TankModel.bulletType;
        }

        //returning current position of tank
        public Vector3 GetCurrentTankPosition()
        {
            return TankView.transform.position;
        }
    }
}