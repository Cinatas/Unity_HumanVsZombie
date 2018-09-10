using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TDzombie.Manager
{
    /// <summary>
    /// 资源管理类，用于管理游戏资源
    /// </summary>
    public class GameResourceManager : MonoBehaviour
    {
        public static GameResourceManager _Instance = null;

        public int coins;
        
        private void Awake()
        {
            _Instance = this;
        }

        public void GainCoins(int mount)
        {
            coins += mount;
        }

        public bool CostCoins(int mount)
        {
            if (coins >= mount)
            {
                coins -= mount;
                return true;
            }
            else
            {
                return false;
            }
        }


    }

}
