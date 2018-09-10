using UnityEngine;

namespace TDzombie
{
    public interface ILive
    {
        float Health { set; get; }
        GameObject HurtFX { get; }
        
        void Hurt(float damage);
        void Die();
    }

}
