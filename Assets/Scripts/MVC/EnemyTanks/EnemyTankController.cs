﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outscal.BattleTank 
{
    /// <summary>
    /// enemy tnak controller class
    /// </summary>
    public class EnemyTankController
    {
        #region referances of other class
        private EnemyTankService enemyTankService;
        #endregion

        #region properties
        public EnemyTankModel enemyTankModel { get; private set; }
        public EnemyTankView enemyTankView { get; private set; }
        #endregion

        public EnemyTankController(EnemyTankModel _enemyModel, EnemyTankView _enemyView, Vector3 pos)
        {
            enemyTankModel = _enemyModel;
            enemyTankView = GameObject.Instantiate<EnemyTankView>(_enemyView, pos, Quaternion.identity);
            enemyTankView.SetEnemyTankController(this);
            enemyTankModel.SetEnemyTankController(this);

        }

        //get random position to patroll
        public Vector3 GetRandomPosition()
        {
            float x = Random.Range(enemyTankView.minX, enemyTankView.maxX);
            float z = Random.Range(enemyTankView.minZ, enemyTankView.maxZ);
            Vector3 randDir = new Vector3(x, 0, z);
            return randDir;
        }

        //setting patrolling destination for enemies
        private void SetPatrolingDestination()
        {
            Vector3 newDestination = GetRandomPosition();
            enemyTankView.enemyNavMesh.SetDestination(newDestination);
        }

        //enemy patrolling function
        public void Patrol()
        {
            enemyTankView.timer += Time.deltaTime;
            if (enemyTankView.timer > enemyTankView.patrolTime)
            {
                SetPatrolingDestination();
                enemyTankView.timer = 0;
            }
        }

        //enemy patrolling and will attck on player when player entor into nav mesh range
        public void EnemyPatrollingAI()
        {
            if (TankService.Instance.PlayerPos() != null)
            {
                float distance = Vector3.Distance(TankService.Instance.PlayerPos().position, enemyTankView.transform.position);
                if (distance <= enemyTankView.howClose)
                {
                    enemyTankView.currentState.ChangeState(enemyTankView.chasingState);
                }
            }
                enemyTankView.currentState.ChangeState(enemyTankView.patrollingState);
        }

        //enemy chase player to attack
        public void ChaseToPlayer()
        {
            enemyTankView.transform.LookAt(TankService.Instance.PlayerPos());
            enemyTankView.enemyNavMesh.SetDestination(TankService.Instance.PlayerPos().position);
            ShootBullet();
        }

        //enemy bullet shooting
        public void ShootBullet()
        {
            if (enemyTankView.canFire < Time.time)
            {
                enemyTankView.canFire = enemyTankModel.fireRate + Time.time;
                CreatingBullet();
            }
        }

        //creating bullet for enemy tank
        public void CreatingBullet()
        {
            BulletService.Instance.CreateNewBullet(GetFiringPosition(), GetFiringAngle(), GetBullet());
        }

        //triggers when enemy die
        public void DeadEnemy()
        {
            //UIManager.Instance.PopUpPlayerWinPanel();
            AchievementService.Instance.InvokeEnemyKilledEvent();
            EnemyTankService.Instance.DestroyEnemyTank(this);
            UIManager.Instance.UpdateScoreText();
        }

        //enemy will take damage
        public void ApplyDamage(int damage)
        {
            enemyTankModel.Health -= damage; 
            Debug.Log("Enemy Health : " + enemyTankModel.Health);

            if (enemyTankModel.Health <= 0)
            {
                Debug.Log("Dead called");
                DeadEnemy();
            }
        }

        //after death destroy model and view
        public void DestroyEnemyController()
        {
            enemyTankModel.DestroyModel();
            enemyTankView.DestroyView();
            enemyTankModel = null;
            enemyTankView = null;
        }

        //setting firing poaition for enemy tank
        public Vector3 GetFiringPosition()
        {
            return enemyTankView.BulletShootPoint.position;
        }

        //setting firing angle
        public Quaternion GetFiringAngle()
        {
            return enemyTankView.transform.rotation;
        }

        //enemy tanks get bullet scriptable object
        public BulletScriptableObject GetBullet()
        {
            return enemyTankModel.bulletType;
        }
    }
}