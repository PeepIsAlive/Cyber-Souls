using UnityEngine;

public class ControlOverButtons : MonoBehaviour
{
    [SerializeField] private GameObject _pickUpButton;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_pickUpButton) { return; }

        if (collision.gameObject.layer == 6)
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

        if (collision.gameObject.layer == 6)
        {
            _pickUpButton.SetActive(false);
        }
    }
}
