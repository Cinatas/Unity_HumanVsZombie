using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameContextView : ContextView {

    private void Awake()
    {
        this.context = new MyGameMVCSContext(this); 
    }
}
