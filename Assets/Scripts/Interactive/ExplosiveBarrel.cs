using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController
{
    public class ExplosiveBarrel : MonoBehaviour, IDamageable
    {
        [Header("BARREL PARAMS")]
        [SerializeField] private int maxDamage = 100;
        [SerializeField] private ParticleSystem explosiveParticle;

        private List<Transform> _atZoneObjects = new List<Transform>();
        private int _health;

        private void Start()
        {
            _health = 100;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Debug.LogError("damage < 0");
                return;
            }

            _health -= damage;

            if (_health <= 0)
            {
                Explosive();
            }
        }

        [NaughtyAttributes.Button]
        private void Explosive()
        {
            foreach (var damageable in _atZoneObjects)
            {
                float distance = Vector3.Distance(transform.position, damageable.position);

                int damageToApply = Mathf.RoundToInt(maxDamage / distance);

                if (damageable.TryGetComponent(out IDamageable damage))
                {
                    damage?.TakeDamage(damageToApply);
                }
            }

            var particle = Instantiate(explosiveParticle, transform.position, Quaternion.identity);
            Destroy(particle, 2f);

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            _atZoneObjects.Add(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            _atZoneObjects.Remove(other.transform);
        }
    }
}
