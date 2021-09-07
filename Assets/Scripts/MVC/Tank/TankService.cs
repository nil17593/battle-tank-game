using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outscal.BattleTank
{
    /// <summary>
    /// service class that handles all tank services
    /// and it inherits Monobehaviour class
    /// </summary>
    public class TankService : MonoGenericSingletone<TankService>
    {
        #region serialized fields
        [SerializeField] private TankScriptableObjectList tankList;
        #endregion

        #region properties
        public TankScriptableObject TankScriptableObject { get; private set; }
        public TankView TankView { get; private set; }
        #endregion

        #region lists
        private List<TankController> tanks = new List<TankController>();
        private List<EnemyTankController> enemyControllers;
        #endregion

        #region referances of other scripts
        private TankController tankController;
        private TankModel currentTankModel;
        #endregion

        #region private variables
        private Transform pos;
        private GameObject destroyGround;
        private int randomNo;
        #endregion

        private void Start()
        {
            CreateNewTank();
        }

        private TankController CreateNewTank()
        {
            randomNo = Random.Range(0, tankList.tanks.Length);
            TankScriptableObject tankScriptableObject = tankList.tanks[randomNo];
            TankView = tankScriptableObject.TankView;
            TankModel tankModel = new TankModel(tankScriptableObject);
            currentTankModel = tankModel;
            tankController = new TankController(tankModel, TankView);
            tanks.Add(tankController);
            return tankController;
            
        }

        //return current tank model
        public TankModel GetCurrentTankModel()
        {
            Debug.Log(currentTankModel.ToString());
            return currentTankModel;
        }

        //return tank controller
        public TankController GetTankController()
        {
            return tankController;
        }

        //setting player position
        public void GetPlayerPos(Transform _position)
        {
            pos = _position;
        }

        //return player position to the caller
        public Transform PlayerPos()
        {
            return pos;
        }

        //destroy child components of tank after 
        public void DestroyTank(TankController tank)
        {
            DestroyAllEnemies();
            tank.DestroyController();
            for (int i = 0; i < tanks.Count; i++)
            {
                if (tanks[i] == tank)
                {
                    tanks[i] = null;
                    tanks.Remove(tank);
                }
            }
           // destroyGround.SetActive(false);   
        }

        //destroy all enmies present in scene after players death
        async void DestroyAllEnemies()
        {
            enemyControllers = EnemyTankService.Instance.enemyTanksList;
            EnemyTankService.Instance.UnsubscribeEvents();
            for (int i = 0; i < enemyControllers.Count; i++)
            {
                if (EnemyTankService.Instance.enemyTanksList[i].enemyTankView != null)
                {
                    await new WaitForSeconds(2f);
                    enemyControllers[i].DeadEnemy();
                }
            }

        }
    }
}