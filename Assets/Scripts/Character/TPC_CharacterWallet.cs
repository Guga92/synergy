using TMPro;
using UnityEngine;

namespace ThirdPersonController
{
    public class TPC_CharacterWallet : MonoBehaviour, IWallet
    {
        [Header("TEXT PARAMS")]
        [SerializeField] private TextMeshProUGUI text;
        private int _coinCount;

        private TPC_CharacterHealth _health;

        private void Awake()
        {
            _health = GetComponent<TPC_CharacterHealth>();
        }

        private void Start()
        {
            UpdateUIDisplay();

            _health.onDeath += () => { text.text = ""; };
        }

        public void Add(int count)
        {
            if(count < 0)
            {
                Debug.LogError("count < 0");
                return;
            }

            _coinCount += count;

            UpdateUIDisplay();
        }

        public void Remove(int count)
        {
            if(count < 0)
            {
                Debug.LogError("count < 0");
                return;
            }

            _coinCount -= count;

            if(_coinCount < 0)
            {
                _coinCount = 0;
            }

            UpdateUIDisplay();
        }

        private void UpdateUIDisplay()
        {
            text.text = "Coins: " + _coinCount.ToString();
        }
    }
}
