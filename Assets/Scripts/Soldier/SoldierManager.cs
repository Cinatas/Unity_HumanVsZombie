using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TDzombie.Manager
{
    public class SoldierManager : MonoBehaviour
    {
        public static SoldierManager _Instance = null;
        public List<Soldier> soldierList;
        private void Awake()
        {
            _Instance = this;
            soldierList = new List<Soldier>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
