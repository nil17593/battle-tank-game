using System;
using UnityEngine;


namespace Outscal.BattleTank
{

    [CreateAssetMenu(menuName = "BulletFireAchievenetSO", fileName = "ScriptableObject/NewfireBulletSO")]
    public class BulletsFiredAchievementSO : ScriptableObject
    {
        public BulletAchievement[] bulletsFiredArray;

        [Serializable]
        public class BulletAchievement
        {
            public BulletAchievementType bulletAchievementType;
            public int requirement;
        }


    }

    public enum BulletAchievementType
    {
        None,
        SharpShooter,
        WeaponMaster,
        ScullCrusher
    }
}