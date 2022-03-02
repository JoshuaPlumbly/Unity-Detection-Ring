using UnityEngine;

public class HandheldItemPlayerManager : MonoBehaviour
{
    [SerializeField] private Transform _heldItemTransform;

    private HandheldItemControllerForPlayer _currentItem;
    private GameObject _currentItemInstance;

    public void Equip(HandheldItemControllerForPlayer handheldItem)
    {
        UnEquipAndDestroy();

        if (handheldItem == null)
            return;

        _currentItemInstance = Instantiate(handheldItem.gameObject);

        if (_currentItemInstance != null)
        {
            Transform prefabInstanceTransform = _currentItemInstance.transform;
            prefabInstanceTransform.parent = _heldItemTransform;
            prefabInstanceTransform.localPosition = Vector3.zero;
            prefabInstanceTransform.localRotation = Quaternion.identity;
            prefabInstanceTransform.localScale = Vector3.one;
        }

        _currentItem = _currentItemInstance.GetComponent<HandheldItemControllerForPlayer>();
    }

    public void UnEquip()
    {
        if (_currentItemInstance != null)
            _currentItemInstance.SetActive(false);
    }

    public void UnEquipAndDestroy()
    {
        if (_currentItemInstance != null)
            Destroy(_currentItemInstance);
    }

    private void Update()
    {
        if (_currentItem != null)
            _currentItem.OnUpdate();
    }
}