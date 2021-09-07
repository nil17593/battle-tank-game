using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outscal.BattleTank
{
    /// <summary>
    /// enemy tank service
    /// </summary>
    public class EnemyTankService : MonoGenericSingletone<EnemyTankService>
    {
        #region serialized fields
        [SerializeField] private EnemyTankScriptableObject enemyTankScriptableObject;
        #endregion

        #region Lists
        public List<Transform> enemyPos;
        public List<EnemyTankController> enemyTanksList = new List<EnemyTankController>();
        #endregion

        #region referances of other classes
        private EnemyTankController enemyTankController;
        #endregion

        #region private variables
        private int count = 0;
        private float spwanTime = 5f;
        #endregion

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            StartCoroutine(SpawnWaiting());
            count++;
            SubScribeEvent();
        }

        private EnemyTankController CreateNewTank(Transform enemyNewPos)
        {
            EnemyTankView enemyTankView = enemyTankScriptableObject.EnemyTankView;
            Vector3 pos = enemyNewPos.position;
            EnemyTankModel enemyTankModel = new EnemyTankModel(enemyTankScriptableObject);
            enemyTankController = new EnemyTankController(enemyTankModel, enemyTankView, pos);
            enemyTanksList.Add(enemyTankController);
            return enemyTankController;
        }

        //subscribe enemy kill event
        private void SubScribeEvent()
        {
            AchievementService.Instance.OnEnemyKilled += UpdateEnemiesKilledCount;
        }

        //updates enemy killed count for unlock achivements
        private void UpdateEnemiesKilledCount()
        {
            TankService.Instance.GetCurrentTankModel().enemyKilled += 1;
            // PlayerPrefs.SetInt("EnemiesKilled", TankService.instance.GetCurrentTankModel().EnemiesKilled);
            // Debug.Log(TankService.instance.GetCurrentTankModel().EnemiesKilled);
            // UIService.instance.UpdateScoreText();
            AchievementService.Instance.GetAchievementController().CheckForEnemyKilledAchievement();
        }

        //enemy spawning randomly 
        void SpawningEnemy()
        {
            int num = Random.Range(0, enemyPos.Count-1);
            CreateNewTank(enemyPos[num]);
            enemyPos.RemoveAt(num);
        }

        //coroutine for spawn enemies  
        IEnumerator SpawnWaiting()
        {
            SpawningEnemy();
            yield return new WaitForSeconds(spwanTime);
            if (count >= 5)
            {
                StopCoroutine(SpawnWaiting());
            }
            else
            {
                StartCoroutine(SpawnWaiting());
            }
            count++;
        }

        //destroy enemy tank after death
        public void DestroyEnemyTank(EnemyTankController enemyTank)
        {
            enemyTank.DestroyEnemyController();
        }

        //unsubscribing from event
        public void UnsubscribeEvents()
        {
            Debug.Log("Unscub");
            AchievementService.Instance.OnEnemyKilled -= UpdateEnemiesKilledCount;
        }

        //returns enemytank controller
        public EnemyTankController GetEnemyTankController()
        {
            return enemyTankController;
        }
    }
}