using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject player;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.GetComponent<GunController>().buttonDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.GetComponent<GunController>().buttonUp();
    }
}
