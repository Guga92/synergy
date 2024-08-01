using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController
{
    public class Wpn_Sounds : MonoBehaviour
    {
        [Header("SOUNDS PARAMS")]
        [SerializeField] private AudioClip fireClip;
        [SerializeField] private Transform spawnSoundPoint;
        private Wpn_Shoot _wpnShoot;

        private void Awake()
        {
            _wpnShoot = GetComponent<Wpn_Shoot>();
        }

        private void OnEnable()
        {
            _wpnShoot.onWeaponShoot += PlayFireSound;
        }

        private void OnDisable()
        {
            _wpnShoot.onWeaponShoot -= PlayFireSound;
        }

        private void PlayFireSound()
        {
            AudioSource.PlayClipAtPoint(fireClip, spawnSoundPoint.position);
        }
    }
}
