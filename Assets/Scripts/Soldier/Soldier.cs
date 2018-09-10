using CreativeSpore.RpgMapEditor;
using System.Collections;
using System.Collections.Generic;
using TDzombie.Manager;
using TDzombie.Model;
using UnityEngine;
using UnityEngine.Events;

namespace TDzombie
{
    public class Soldier : MonoBehaviour,ILive
    {
        SoldierDataModel data;
        MapPathFindingBehaviour mapPathFinding;
        DirectionalAnimation aniCon;
        protected List<Enemy> InSightList;

        private SoldierAnimationState aniState = SoldierAnimationState.Normal;
        private SoldierAnimationState preAniState;
        private UnityAction OnStateChanged;

        public float DetectRadius;
        private float attackSpeed;
        private float attackTimeCount = 0;
        private float attack;
        Coroutine AttackCoroutine;
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
               
        public GameObject HurtFX
        {
            get
            {
                return null;
            }
        }

        private void OnDestroy()
        {
            SoldierManager._Instance.soldierList.Remove(this);
        }

        private void Awake()
        {
            data = this.GetComponent<SoldierDataModel>();
            mapPathFinding = this.GetComponent<MapPathFindingBehaviour>();
            InSightList = new List<Enemy>();
            aniCon = this.GetComponent<DirectionalAnimation>();
        }

        // Use this for initialization
        void Start()
        {
            attackSpeed = data.AttackSpeed;
            attack = data.Attack;
            SoldierManager._Instance.soldierList.Add(this);
            AttackCoroutine = StartCoroutine(Attack());
        }

        private void OnEnable()
        {
            this.OnStateChanged += StateChangedEvent;
        }

        private void OnDisable()
        {
            this.OnStateChanged -= StateChangedEvent;
        }

        // Update is called once per frame
        void Update()
        {
            //进出视野范围内的逻辑处理
            if (EnemyManager._Instance.enemyList.Count > 0)
            {
                foreach(var i in EnemyManager._Instance.enemyList)
                {
                    if (Vector2.Distance(this.transform.position, i.transform.position)<= DetectRadius)
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
                Enemy enemy = InSightList[0];
                if (enemy == null)
                {
                    InSightList.Remove(enemy);
                    return;
                }
                if (attackTimeCount >= 1f / attackSpeed)
                {
                    //攻击敌人
                    enemy.Hurt(attack);
                    attackTimeCount = 0;
                    this.aniState = SoldierAnimationState.Pistol;
                    ChangeDirection(enemy.transform.position);

                }
                else
                    attackTimeCount += Time.deltaTime;
            }
            else
            {
                //执行警戒
                this.aniState = SoldierAnimationState.Normal;
            }

            if(preAniState != aniState)
            {
                if (OnStateChanged != null)
                    OnStateChanged();
            }
            preAniState = aniState;
        }

        /// <summary>
        /// 外部调用，用于道具效果，更改目的地
        /// </summary>
        /// <param name="target"></param>
        public void ChangeMovemapDestnation(Vector2 targetPos)
        {
            mapPathFinding.TargetPos = targetPos;
        }

        void OnDrawGizmosSelected()
        {
            EditorCompatibilityUtils.CircleCap(0, transform.position, transform.rotation,DetectRadius);
        }

        void StateChangedEvent()
        {
            aniCon.SetSoldierAniState(this.aniState);
        }

        void ChangeDirection(Vector2 faceTo)
        {
            Vector2 left = new Vector2(-1, 0);
            Vector2 right = new Vector2(1, 0);
            Vector2 up = new Vector2(0, 1);
            Vector2 down = new Vector2(0, -1);

            float deltaX = faceTo.x - this.transform.position.x;
            float deltaY = faceTo.y - this.transform.position.y;

            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                //左右
                if (deltaX > 0)
                    aniCon.SetAnimDirection(right);
                else
                    aniCon.SetAnimDirection(left);
            }
            else
            {
                //上下
                if (deltaY > 0)
                {
                    aniCon.SetAnimDirection(up);
                }
                else
                {
                    aniCon.SetAnimDirection(down);
                }
            }

            
        }

        public void Die()
        {
            Destroy(this.gameObject);
        }

        public void Hurt(float damage)
        {

            if (this.Health > 0)
                this.Health -= damage;
            else
                Die();
        }

        IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / attackSpeed);
                if (InSightList.Count > 0)
                {
                    Enemy i = InSightList[0];
                    i.Hurt(1);
                }
            }

        }
    }

}
