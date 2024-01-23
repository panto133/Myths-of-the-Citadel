using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class SelectingUnits : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int holder;
    [SerializeField] private PlayerBasicLogic playerLogic;
    public void OnPointerClick(PointerEventData eventData)
    {
        //Left Click
        if (eventData.pointerId == -1)
            playerLogic.SelectUnit(holder);
        else if (eventData.pointerId == -3)
            Debug.Log("Middle click");
        else if (eventData.pointerId == -2)
            Debug.Log("Right click");
        Debug.Log("I clicked - " + holder);
    }
}
