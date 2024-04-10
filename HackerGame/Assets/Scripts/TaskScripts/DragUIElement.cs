using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIElement : MonoBehaviour, IDragHandler, IEndDragHandler {

    /*Documentation about the drag handler Interface: https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.IDragHandler.html
      For some reason there is no documentation page for newer version of Unity

      If you want to drag UI elements Inherit this script to other scripts :)
     */

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    /*
     * Slapped some Polymorphism here: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism
     */
    public virtual void OnEndDrag(PointerEventData eventData) {
        Debug.Log($"Drag ended on {gameObject.name}");
    }

}
