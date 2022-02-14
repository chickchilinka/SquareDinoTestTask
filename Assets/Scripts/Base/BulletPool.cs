using UnityEngine;
namespace Base
{
    public class BulletPool : Pool<Bullet>
    {
        [SerializeField]
        private GameObject bulletPrefab;
        public override Bullet Create() {
            return Instantiate(bulletPrefab).GetComponent<Bullet>();
        }
    }
}
