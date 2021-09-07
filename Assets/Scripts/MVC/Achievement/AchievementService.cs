using System;
using System.Collections;
using UnityEngine;

namespace Outscal.BattleTank
{
    public class AchievementService : MonoGenericSingletone<AchievementService>
    {
        /// <summary>
        /// generic singleton class for achievement services
        /// </summary>

        #region Events
        public event Action OnPlayerFiredBullet;
        public event Action OnEnemyKilled;
        #endregion

        #region serialized fields
        [SerializeField]private BulletsFiredAchievementSO bulletsFiredSO;
        [SerializeField]private EnemyKilledAchievementSO enemyKilledSO;
        #endregion

        #region other scripts referances
        private AchievementController achievementController;
        private AchievementModel achievementModel;
        #endregion          

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            CreateAchievement();
            InvokeEnemyKilledEvent();
            InvokeOnPlayerFiredBulletEvent();
        }

        //creating achivements
        private void CreateAchievement()
        {
            achievementModel = new AchievementModel(bulletsFiredSO, enemyKilledSO);
            achievementController = new AchievementController(achievementModel);
        }

        //return achievement controller to the caller
        public AchievementController GetAchievementController()
        {
            return achievementController;
        }

        //invoking fire bullet event
        public void InvokeOnPlayerFiredBulletEvent()
        {
            OnPlayerFiredBullet?.Invoke();
        }

        //invoking enemy killed event
        public void InvokeEnemyKilledEvent()
        {
            OnEnemyKilled?.Invoke(); //Its same as below code

            // if (OnEnemyKilled == null)
            // {
            //     OnEnemyKilled.Invoke();
            // }
        }
    }
}