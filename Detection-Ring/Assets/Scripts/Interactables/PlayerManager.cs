using System;
using UnityEngine;

namespace Plumbly
{
    public class PlayerManager : MonoBehaviour
    {
        public event Action UpdateTick;

        public void Update()
        {
            UpdateTick?.Invoke();
        }
    }
}