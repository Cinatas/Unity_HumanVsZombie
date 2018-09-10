using System.Collections;
using System.Collections.Generic;
using TDzombie.Manager;
using UnityEngine;
namespace TDzombie
{
    public class EnemyGenerator : MonoBehaviour
    {
        public float GenerateRate;
        List<GameObject> enemyList;
        List<GameObject> currentEnemyList;
        protected virtual void Awake()
        {
            enemyList = new List<GameObject>();
            currentEnemyList = new List<GameObject>();
        }

        // Use this for initialization
        void Start()
        {
            GameObject[] enemies = Resources.LoadAll<GameObject>("EnemyObject");
            foreach(var i in enemies)
            {
                enemyList.Add(i);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GenerateEnemy()
        {
            GenerateEnemy(this.transform.position);
        }

        public void GenerateEnemy(Vector3 pos,EnemyType index = EnemyType.Null)
        {
            if (index == EnemyType.Null)
                return;
            //EnemyManager._Instance.enemyList.Add()
        }
    }

}
