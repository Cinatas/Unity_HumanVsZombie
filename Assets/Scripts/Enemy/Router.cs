using CreativeSpore.RpgMapEditor;
using System.Collections;
using System.Collections.Generic;
using TDzombie.Manager;
using UnityEngine;
namespace TDzombie
{
    /// <summary>
    /// 路径节点类，用于指引下一个路径点
    /// </summary>
    public class Router : MonoBehaviour
    {
        public Transform nextTransform;
        public Vector2 nextPosition;
        public InterActiveObject type;
        public int touchCount;
        public bool touchToDestroy = false;
        public float touchRadius;
        // Use this for initialization
        void Start()
        {
            if (nextTransform != null)
                nextPosition = nextTransform.position;
        }

        private void Update()
        {
            switch (type)
            {
                case InterActiveObject.Null:
                    break;
                case InterActiveObject.Enemy:
                    RouteEnemy();
                    break;
                case InterActiveObject.Soldier:
                    RouteSoldier();
                    break;
                default:
                    break;

            }
        }

        void RouteEnemy()
        {
            var targetList = EnemyManager._Instance.enemyList;
            foreach(var i in targetList)
            {
                float distance = Vector2.Distance(this.transform.position, i.transform.position);
                if (distance <= touchRadius)
                {
                    if (touchCount > 0)
                    {
                        touchCount--;
                        i.ChangeMovemapDestnation(nextPosition);
                    }
                    else
                    {
                        if (touchToDestroy)
                            Destroy(this.gameObject);
                    }                   
                }
            }
        }

        void RouteSoldier()
        {
            var targetList = SoldierManager._Instance.soldierList;
            foreach (var i in targetList)
            {
                float distance = Vector2.Distance(this.transform.position, i.transform.position);
                if (distance <= touchRadius)
                {
                    if (touchCount > 0)
                    {
                        touchCount--;
                        i.ChangeMovemapDestnation(nextPosition);
                    }
                    else
                    {
                        if (touchToDestroy)
                            Destroy(this.gameObject);
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            EditorCompatibilityUtils.CircleCap(0, this.transform.position, this.transform.rotation, touchRadius);
        }
    }

    public enum InterActiveObject
    {
        Null,
        Enemy,
        Soldier
    }

}
