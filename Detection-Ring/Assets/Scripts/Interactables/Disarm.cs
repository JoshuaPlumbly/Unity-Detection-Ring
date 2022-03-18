using UnityEngine;

namespace Plumbly.Interactables
{
    public class Disarm : HoldToInteract
    {
        [SerializeField] private GameObject _toRemove;

        protected override void Preform()
        {
            GameObject.Destroy(_toRemove);
        }
    }
}