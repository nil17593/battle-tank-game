using System.Collections;
using UnityEngine;

namespace Outscal.BattleTank
{
    public class AchievementController
    {
        /// <summary>
        /// this class handles creating different type of achievement
        /// </summary>
        public AchievementModel AchievementModel { get;  set; }
        private int currentStageOfBulletFiredAchievement;
        private int currentStageOfEnemyKilledAchievement;


        public AchievementController(AchievementModel achievementModel)
        {
            currentStageOfBulletFiredAchievement = 0;
            currentStageOfEnemyKilledAchievement = 0;
            this.AchievementModel = achievementModel;

        }

        //creating enemy kileed achievement
        public void CheckForEnemyKilledAchievement()
        {
            for (int i = 0; i < AchievementModel.EnemyKilledAchievementSO.enemyKilledArray.Length; i++)
            {
                if (TankService.Instance.GetCurrentTankModel().enemyKilled == AchievementModel.EnemyKilledAchievementSO.enemyKilledArray[i].requirement)
                {
                    string achievement = AchievementModel.EnemyKilledAchievementSO.enemyKilledArray[i].EnemyKilledAchievementType.ToString();
                    UnlockedAchievement(achievement);
                    currentStageOfEnemyKilledAchievement = i + 1;
                }
            }
        }

        //creating bullets fired achievement
        public void CheckForBulletsfiredAchievement()
        {
            for (int i = 0; i < AchievementModel.BulletsFiredAchievementSO.bulletsFiredArray.Length; i++)
            {
                if (TankService.Instance.GetCurrentTankModel().bulletsFired == AchievementModel.BulletsFiredAchievementSO.bulletsFiredArray[i].requirement)
                {
                    string achievement = AchievementModel.BulletsFiredAchievementSO.bulletsFiredArray[i].bulletAchievementType.ToString();
                    UnlockedAchievement(achievement);
                    currentStageOfBulletFiredAchievement = i + 1;
                }
            }
        }

        //popup will show which achievement is unlocked
        public void UnlockedAchievement(string achievement)
        {
            Debug.Log("Got :" + achievement);
            UIManager.Instance.PopUpAchievement(achievement);
        }
    }
}