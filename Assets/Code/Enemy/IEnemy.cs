using UnityEngine;

namespace Code.Enemy
{
    public interface IEnemy
    {
        public void Move();
        //public void ChangeHP(int damage);
        public void Reload();
        public void Dispose();
    }
}