using System.Collections;
using UnityEngine;

namespace Outscal.BattleTank
{
    public class AchievementModel
    {
        /// <summary>
        /// achievement model class
        /// </summary>
        public BulletsFiredAchievementSO BulletsFiredAchievementSO { get; private set; }
        public EnemyKilledAchievementSO EnemyKilledAchievementSO { get; private set; }

        public AchievementModel(BulletsFiredAchievementSO bulletFiredSO,EnemyKilledAchievementSO enemyKilledSO)
        {
            this.BulletsFiredAchievementSO = bulletFiredSO;
            this.EnemyKilledAchievementSO = enemyKilledSO;
        }
    }
}