using UnityEngine;

namespace ThirdPersonController
{
    public class Wpn_Particles : MonoBehaviour
    {
        [Header("IMPACT PARAMS")]
        [SerializeField] private ParticleSystem impact;
        [SerializeField] private float destroyTime = 0.3f;

        [Header("MUZZLEFLASH PARAMS")]
        [SerializeField] private ParticleSystem muzzleFlash;

        private Wpn_Shoot _wpnShoot;

        private void Awake()
        {
            _wpnShoot = GetComponent<Wpn_Shoot>();
        }

        private void OnEnable()
        {
            _wpnShoot.onNormalHit += SpawnHitImpact;
            _wpnShoot.onWeaponShoot += PlayMuzzleFlash;
        }
        
        private void OnDisable()
        {
            _wpnShoot.onNormalHit -= SpawnHitImpact;
            _wpnShoot.onWeaponShoot -= PlayMuzzleFlash;
        }

        private void PlayMuzzleFlash()
        {
            muzzleFlash?.Play();
        }

        private void SpawnHitImpact(Vector3 point, Vector3 normal)
        {
            if (!impact) return;

            var spawnedImpact = Instantiate(impact.gameObject, point, Quaternion.identity);
            spawnedImpact.transform.rotation = Quaternion.LookRotation(normal);

            Destroy(spawnedImpact, destroyTime);
        }
    }
}
