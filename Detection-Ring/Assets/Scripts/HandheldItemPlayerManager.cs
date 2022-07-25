using UnityEngine;

namespace Plumbly
{
    public class HandheldItemPlayerManager : MonoBehaviour
    {
        [SerializeField] private Transform _heldItemTransform;

        private PlayerImplement _currentItem;
        private GameObject _currentHeldItemGameObject;

        public void Equip(PlayerImplement newHeldItem)
        {
            UnEquip();

            if (newHeldItem == null)
                return;

            _currentHeldItemGameObject = newHeldItem.gameObject;
            _currentHeldItemGameObject.SetActive(true);

            if (_currentHeldItemGameObject != null)
            {
                Transform prefabInstanceTransform = _currentHeldItemGameObject.transform;
                prefabInstanceTransform.parent = _heldItemTransform;
                prefabInstanceTransform.localPosition = Vector3.zero;
                prefabInstanceTransform.localRotation = Quaternion.identity;
                prefabInstanceTransform.localScale = Vector3.one;
            }

            _currentItem = newHeldItem;
        }

        public void UnEquip()
        {
            if (_currentHeldItemGameObject != null)
                _currentHeldItemGameObject.SetActive(false);
        }
    }
}