using UnityEngine;

namespace Code.Enemy
{
    public class CheckerDistance
    {
        private float _currentDisnace;
        public void CheckDistance(GameObject enemy, GameObject hero)
        {
            if (!enemy.activeSelf) return;
            
            var distance = Vector3.Distance(enemy.transform.position, hero.transform.position);
        }
    }
}