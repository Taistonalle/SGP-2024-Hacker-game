using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlock_EA : CodeBlock_EE {
    void Start() {
        colllider = GetComponent<BoxCollider2D>();
        SetStartPos();
    }
    
}
