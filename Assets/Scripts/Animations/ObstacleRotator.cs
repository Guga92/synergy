using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController
{
    public class ObstacleRotator : MonoBehaviour
    {
        [Header("ANIMATION PARAMS")]
        [SerializeField, Range(0, 2)] private float rotationSpeed = 1f;
        private List<Tween> _tweens = new List<Tween>();

        private void Start()
        {
            StartAnimation();
        }

        private void OnDestroy()
        {
            foreach (var tweens in _tweens)
            {
                tweens.Kill();
            }
        }

        private void StartAnimation()
        {
            _tweens.Add(transform.DORotate(new Vector3(90, 0, 360), rotationSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1));
        }
    }
}
