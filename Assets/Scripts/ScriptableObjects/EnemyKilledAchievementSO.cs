﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Outscal.BattleTank
{
    [CreateAssetMenu(menuName = "EnemyKilledAchievementSO", fileName = "ScriptableObject/NewEnemyKilledAchievementSO")]
    public class EnemyKilledAchievementSO : ScriptableObject
    {
        public EnemyKilledAchievements[] enemyKilledArray;

        [Serializable]
        public class EnemyKilledAchievements
        {

            public EnemyKilledAchievementsType EnemyKilledAchievementType;
            public int requirement;

        }
    }

    public enum EnemyKilledAchievementsType
    {
        None,
        Commando,
        Prediator,
        DeathBringer
    }
}