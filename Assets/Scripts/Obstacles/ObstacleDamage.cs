using UnityEngine;

namespace ThirdPersonController
{
    public class ObstacleDamage : MonoBehaviour
    {
        [Header("DAMAGE PARAMS")]
        [SerializeField, Range(1, 100)] private int damage = 25;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out CharacterController controller))
            {
                Vector3 pushDirection = other.transform.position - transform.position;
                pushDirection.y = 0;
                pushDirection.Normalize();

                controller.Move(pushDirection * 10 * Time.deltaTime);
            }
        }
    }
}
