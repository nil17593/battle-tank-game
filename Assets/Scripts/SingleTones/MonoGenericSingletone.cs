using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outscal.BattleTank
{
    /// <summary>
    ///Generic singletone class 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoGenericSingletone<T> : MonoBehaviour where T : MonoGenericSingletone<T>
    {
        private static T instance;

        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// player class taht inherited from base singletone class 
    /// </summary>
    public class PlayerTank : MonoGenericSingletone<PlayerTank>
    {
        protected override void Awake()
        {
            base.Awake();
            //game logic
        }
    }
    /// <summary>
    /// enemy class that is inherited from base generic singletone
    /// </summary>
    public class EnemyTank : MonoGenericSingletone<EnemyTank>
    {
        protected override void Awake()
        {
            base.Awake();
            //game logic
        }
    }
}
  
