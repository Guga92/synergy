using UnityEngine;

namespace ThirdPersonController
{
    public interface IWallet
    {
        public void Add(int count);

        public void Remove(int count);
    }
}
