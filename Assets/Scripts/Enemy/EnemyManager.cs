using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TDzombie.Manager
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager _Instance = null;
        public List<Enemy> enemyList;
        public Transform EnemyDestnation = null;
        public Vector2 EnemyDestnationPosition;
        private void Awake()
        {
            _Instance = this;
            enemyList = new List<Enemy>();
        }

        public Vector2 GetDestnation()
        {
            return (EnemyDestnation == null) ? EnemyDestnationPosition : (Vector2) EnemyDestnation.transform.position;
        }
    }

}
