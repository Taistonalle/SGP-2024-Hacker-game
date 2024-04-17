using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlock_EP : CodeBlock_EE
{
    void Start()
    {
        colllider = GetComponent<BoxCollider2D>();
        SetStartPos();
    }
}
