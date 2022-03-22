using UnityEngine;

public class ControlOverButtons : MonoBehaviour
{
    [Header("Areas of objects that can be picked up:")]
    [SerializeField] private LayerMask _objectsLM;

    [Header("Buttons:")]
    [SerializeField] private GameObject _pickUpButton;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_pickUpButton) { return; }

        if ((_objectsLM.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (!_pickUpButton.activeInHierarchy)
            {
                _pickUpButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_pickUpButton) { return; }

        if ((_objectsLM.value & (1 << collision.gameObject.layer)) != 0)
        {
            _pickUpButton.SetActive(false);
        }
    }
}
