using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace ThirdPersonController
{
    public class BonusRotation : MonoBehaviour
    {
        [Header("ANIMATION PARAMS")]
        [SerializeField, Range(0, 3)] private float moveSpeed = 2f;
        [SerializeField, Range(0, 2)] private float rotationSpeed = 1f;
        [SerializeField, Range(0, 1)] private float moveDistance = 1f;

        private List<Tween> _tweens = new List<Tween>();

        private void Start()
        {
            StartAnimation();
        }

        private void OnDestroy()
        {
            foreach(var tweens in _tweens)
            {
                tweens.Kill();
            }
        }

        private void StartAnimation()
        {
            Vector3 initialPosition = transform.position;
            Vector3 upPosition = initialPosition + new Vector3(0, moveDistance, 0);
            Vector3 downPosition = initialPosition - new Vector3(0, moveDistance, 0);

            _tweens.Add(DOTween.Sequence()
                .Append(transform.DOMove(upPosition, moveSpeed).SetEase(Ease.InOutSine))
                .Append(transform.DOMove(downPosition, moveSpeed).SetEase(Ease.InOutSine))
                .SetLoops(-1, LoopType.Yoyo));

            _tweens.Add(transform.DORotate(new Vector3(0, 360, 0), rotationSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1));
        }
    }
}
