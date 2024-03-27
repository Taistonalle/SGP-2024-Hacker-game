using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeBlock_EE : MonoBehaviour, IDragHandler, IEndDragHandler {
    [SerializeField] private int id;
    public int Id {
        get { return id; }
        set { id = value; }
    }
    void Start() {

    }

    /*Documentation about the drag handler Interface: https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.IDragHandler.html
      For some reason there is no documentation page for newer version of Unity
     */
    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log($"Drag ended on {gameObject.name}");
    }
}
