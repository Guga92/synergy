using UnityEngine;

namespace ThirdPersonController
{
    public class CoinPoint : MonoBehaviour
    {
        [Header("BONUS PARAMS")]
        [SerializeField] private int coins = 25;
        [SerializeField] private ParticleSystem coinNova;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IWallet wallet))
            {
                var nova = Instantiate(coinNova, transform.position, coinNova.transform.rotation);
                Destroy(nova, 3f);

                wallet.Add(coins);
                Destroy(gameObject);
            }
        }
    }
}
