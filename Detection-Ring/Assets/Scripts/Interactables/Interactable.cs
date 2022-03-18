using UnityEngine;

namespace Plumbly.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void OnEnter(PlayerManager subject);
        public abstract void OnExit(PlayerManager subject);
    }
}