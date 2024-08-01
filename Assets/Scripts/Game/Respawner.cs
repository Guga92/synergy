using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ThirdPersonController
{
    public class Respawner : MonoBehaviour
    {
        [Header("PLAYER PARAMS")]
        [SerializeField] private TPC_CharacterHealth _health;

        [Header("RESPAWNER SCREEN PARAMS")]
        [SerializeField] private CanvasGroup group;
        [SerializeField] private TextMeshProUGUI respawnText;

        private int _seconds;

        private void Start()
        {
            _health.onDeath += StartRespawn;
        }

        public void StartRespawn()
        {
            _seconds = 5;

            group.gameObject.SetActive(true);
            group.DOFade(1, 0.5f);

            StopCoroutine(nameof(Respawn));
            StartCoroutine(nameof(Respawn));
        }

        private IEnumerator Respawn()
        {
            while(_seconds > 0)
            {
                yield return new WaitForSeconds(1);

                respawnText.text = $"Respawn after {_seconds} seconds!";
                _seconds--;
            }

            group.DOFade(0, 0.5f)
                .OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
    }
}
