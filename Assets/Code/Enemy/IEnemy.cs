using System;
using UnityEngine;

namespace Code.Enemy
{
    public interface IEnemy
    {
        public void Run();
        public void Dispose();
        public event Action<int> HitClose;
        public event Action<int> Died;
    }
}