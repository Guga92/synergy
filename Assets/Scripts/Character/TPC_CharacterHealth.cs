using TMPro;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using System;

namespace ThirdPersonController
{
    public class TPC_CharacterHealth : MonoBehaviour, IDamageable, IHeal
    {
        [Header("HEALTH PARAMS")]
        [SerializeField] private int maxHealth = 100;
        private int _currentHealth;

        [Header("TEXT PARAMS")]
        [SerializeField] private TextMeshProUGUI healthText;

        [Header("BLOOD SCREEN PARAMS")]
        [SerializeField] private CanvasGroup screenGroup;
        [SerializeField] private float showTime;
        private Tween _bloodTween;

        public Action onDeath;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if(damage < 0)
            {
                Debug.LogError("damage < 0");
                return;
            }

            _currentHealth -= damage;

            if(_currentHealth <= 0)
            {
                Die();
            }

            UpdateHealthDisplay();
            ShowBloodScreen();
        }

        public void Heal(int heal)
        {
            if(heal < 0)
            {
                Debug.Log("heal < 0");
                return;
            }

            _currentHealth += heal;

            if(_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
            }

            UpdateHealthDisplay();
        }

        private void ShowBloodScreen()
        {
            _bloodTween?.Kill();

            screenGroup.alpha = 0;

            _bloodTween = screenGroup.DOFade(1, showTime).OnComplete(() => 
            { 
                screenGroup.DOFade(0, showTime); 
            });
        }

        private void UpdateHealthDisplay()
        {
            healthText.text = _currentHealth > 0 ? _currentHealth.ToString() + "+" : "";
        }

        private void Die()
        {
            onDeath?.Invoke();
            Destroy(gameObject);
        }

        public bool HealthFully() => _currentHealth >= maxHealth;
    }
}
