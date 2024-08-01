using System.Collections;
using UnityEngine;

namespace ThirdPersonController
{
    public class TPC_CharacterRagdoll : MonoBehaviour
    {
        [Header("RAGDOLL TIME PARAMS")]
        [SerializeField] private float ragdollTime = 2f;

        [Header("RAGDOLL POSITION PARAMS")]
        [SerializeField] private Transform ragdollPosition;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetRagdoll(bool action)
        {
            SetRagdoll(action, false);
        }

        public void SetRagdoll(bool action, bool isTime)
        {
            _animator.enabled = action;

            if (isTime && !action)
            {
                StartCoroutine(nameof(ResetRagdoll));
            }
        }

        private IEnumerator ResetRagdoll()
        {
            yield return new WaitForSeconds(ragdollTime);

            Vector3 position = ragdollPosition.transform.position;

            transform.position = new Vector3(position.x, position.y + 3, position.z);
            _animator.enabled = true;
        }
    }
}
