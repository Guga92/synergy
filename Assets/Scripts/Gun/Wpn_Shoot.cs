using System;
using UnityEngine;

namespace ThirdPersonController
{
    public class Wpn_Shoot : MonoBehaviour
    {
        [Header("RAY PARAMS")]
        [SerializeField] private Transform rayPoint;
        [SerializeField] private float distance = Mathf.Infinity;
        [SerializeField] private LayerMask hitMask;
        private Ray _ray;

        [Header("WEAPON PARAMS")]
        [SerializeField] private int weaponDamage = 25; 

        public Action onWeaponShoot;
        public Action<Vector3, Vector3> onNormalHit;

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            onWeaponShoot?.Invoke();
            RaycastHit hit = GetRaycastHit();

            if(hit.collider != null && !hit.collider.isTrigger)
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(25);
                }

                onNormalHit?.Invoke(hit.point, hit.normal);
            }
        }

        private RaycastHit GetRaycastHit()
        {
            Vector3 direction = rayPoint.forward;

            float spreadAngle = 1.0f;

            float spreadX = UnityEngine.Random.Range(-spreadAngle, spreadAngle);
            float spreadY = UnityEngine.Random.Range(-spreadAngle, spreadAngle);

            Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);
            Vector3 spreadDirection = spreadRotation * direction;

            _ray = new Ray(rayPoint.position, spreadDirection);

            Physics.Raycast(_ray, out RaycastHit hit, distance, hitMask);

            return hit;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(_ray);
        }
    }
}
