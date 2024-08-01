using UnityEngine;

namespace ThirdPersonController
{
    public class HealthPoint : MonoBehaviour
    {
        [Header("BONUS PARAMS")]
        [SerializeField] private int heal = 25;
        [SerializeField] private ParticleSystem healNova;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHeal heal))
            {
                if (heal.HealthFully()) return;

                var nova = Instantiate(healNova, transform.position, healNova.transform.rotation);
                Destroy(nova, 3f);

                heal.Heal(this.heal);
                Destroy(gameObject);
            }
        }
    }
}
