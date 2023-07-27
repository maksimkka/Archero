using UnityEngine;

namespace Code.Enemy
{
    public interface IEnemy
    {
        public void Move();
        public void Attack();
        public void Reload();
        public void Dispose();
    }
}