using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outscal.BattleTank
{
    /// <summary>
    /// this class handles the tank model
    /// </summary>
    public class TankModel
    {
        #region properties
        public int Speed { get; private set; }
        public int Health { get; set; }
        public TankType TankType { get; private set; }
        public BulletScriptableObject bulletType { get; private set; }
        public float rotationSpeed { get; private set; }
        public float fireRate { get; private set; }
        public int enemyKilled { get; set; }
        public int bulletsFired { get; set; }
        #endregion

        #region referances of other classes
        private TankController tankController;
        #endregion

        public TankModel(TankScriptableObject tankScriptableObject)
        {
            //this.tankScriptableObject = tankScriptableObject;
            TankType = tankScriptableObject.TankType;
            Speed = (int)tankScriptableObject.Speed;
            Health = tankScriptableObject.Health;
            rotationSpeed = tankScriptableObject.rotationSpeed;
            fireRate = tankScriptableObject.fireRate;
            bulletType = tankScriptableObject.bulletType;
            bulletsFired = 0;
            enemyKilled = 0;
        }

        //setting tenk controller
        public void SetTankController(TankController _tankController)
        {
            tankController = _tankController;
        }
        //destroy model after player death
        public void DestroyModel()
        {
            bulletType = null;
            tankController = null;
        }
    }
}