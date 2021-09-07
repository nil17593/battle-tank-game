using System.Collections;
using UnityEngine;

namespace Outscal.BattleTank
{
    /// <summary>
    /// handling bullet services
    /// </summary>
    public class BulletService : MonoGenericSingletone<BulletService>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        //creating bullet
        public void CreateNewBullet(Vector3 position, Quaternion rotation, BulletScriptableObject type)
        {
            BulletScriptableObject bullet = type;
            BulletModel bulletModel = new BulletModel(bullet);
            BulletController bulletController = new BulletController(bullet.BulletView, bulletModel, position, rotation);
        }
    }
}        