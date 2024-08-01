using UnityEngine;

namespace ThirdPersonController
{
    public class GameBootstrap : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
