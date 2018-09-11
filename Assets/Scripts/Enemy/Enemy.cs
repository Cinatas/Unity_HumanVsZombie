using CreativeSpore.RpgMapEditor;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TDzombie.Manager;
using TDzombie.Model;
using UnityEngine;
namespace TDzombie
{
    /// <summary>
    /// 敌人类，基类
    /// </summary>
    public class Enemy : MonoBehaviour,ILive
    {
        /// <summary>
        /// 警戒半径，当士兵处于这个范围内会被敌人感知
        /// </summary>
        public float DetectRadius;
        /// <summary>
        /// 攻击半径，当士兵处于这个范围内会被攻击
        /// </summary>
        public float AttackRaduis;

        protected Vector2 Destnation;
        private CircleCollider2D DetectTrigger;
        protected List<Soldier> InSightList;
        AIDestinationSetter mapPathFinding;
        DirectionalAnimation aniCon;

        EnemyDataModel data;
        Coroutine AttackCoroutine;
        private float attackSpeed;
        private float attack;
        public float Health
        {
            get
            {
                return data.Health;
            }

            set
            {
                data.Health = value;
            }
        }

        /// <summary>
        /// 被攻击的特效
        /// </summary>
        public GameObject HurtFX
        {
            get
            {
                return Resources.Load<GameObject>("FX/FxHurtBlood");
            }
        }


        private void Awake()
        {
            InSightList = new List<Soldier>();
            mapPathFinding = this.GetComponent<AIDestinationSetter>();
            data = this.GetComponent<EnemyDataModel>();
            aniCon = this.GetComponent<DirectionalAnimation>();
        }

        private void OnDestroy()
        {
            EnemyManager._Instance.enemyList.Remove(this);
        }

        // Use this for initialization
        void Start()
        {
            EnemyManager._Instance.enemyList.Add(this);
            ChangeMoveSpeed(data.MoveSpeed);
            attackSpeed = data.AttackSpeed;
            attack = data.Attack;
            Destnation = EnemyManager._Instance.GetDestnation();
            AttackCoroutine = StartCoroutine(Attack());
        }

        // Update is called once per frame
        void Update()
        {
            //进出视野范围内的逻辑处理
            if (SoldierManager._Instance.soldierList.Count > 0)
            {                   
                foreach (var i in SoldierManager._Instance.soldierList)
                {
                    if (Vector2.Distance(this.transform.position, i.transform.position) <= DetectRadius)
                    {
                        if (!InSightList.Contains(i))
                        {
                            InSightList.Add(i);
                        }
                    }
                    else
                    {
                        if (InSightList.Contains(i))
                        {
                            InSightList.Remove(i);
                        }
                    }
                }
            }

            if (InSightList.Count > 0)
            {
                Soldier soldier = InSightList[0];
                if (soldier == null)
                    InSightList.Remove(soldier);
                else
                {
                    float deltaX = Random.Range(-0.9f * AttackRaduis, 0.9f * AttackRaduis);
                    float deltaY = Random.Range(-0.9f * AttackRaduis, 0.9f * AttackRaduis);
                    mapPathFinding.targetPos = soldier.transform.position + new Vector3(deltaX, deltaY, 0);
                }   
            }
            else
            {
                mapPathFinding.targetPos = new Vector3(Destnation.x,Destnation.y,this.transform.position.z);
            }



            ChangeDirection();
        }

        public void Hurt(float damage)
        {
            Instantiate(HurtFX, this.transform).transform.localPosition = Vector3.zero;

            if (this.Health > 0)
                this.Health -= damage;
            else
                Die();
        }

        public void Die()
        {
            int bouns = (int)(data.bouns * Random.Range(0.5f, 1.5f));
            GameResourceManager._Instance.GainCoins(bouns);
            Destroy(this.gameObject);
        }

        void ChangeMoveSpeed(float speed)
        {
              this.GetComponent<AILerp>().speed = speed;
        }

        /// <summary>
        /// 外部调用，用于道具效果，更改目的地
        /// </summary>
        /// <param name="target"></param>
        public void ChangeMovemapDestnation(Vector2 targetPos)
        {
            this.Destnation = targetPos;
        }

        void OnDrawGizmosSelected()
        {            
            EditorCompatibilityUtils.CircleCap(0, transform.position, transform.rotation, DetectRadius);
            EditorCompatibilityUtils.CircleCap(0, transform.position, transform.rotation, AttackRaduis);
        }

        void ChangeDirection()
        {
            if (mapPathFinding.GetDestination().x < this.transform.position.x)
            {
                aniCon.SetAnimDirection(Vector2.left);
            }
            else
            {
                aniCon.SetAnimDirection(Vector2.right);
            }           
        }

        IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / attackSpeed);
                if (InSightList.Count > 0)                {
                    
                    foreach(var i in InSightList)
                    {
                        if (Vector2.Distance(this.transform.position, i.transform.position) <= AttackRaduis)
                        {
                            i.Hurt(attack);
                            break;
                        }
                    }
                }
            }            

        }

    }

}
