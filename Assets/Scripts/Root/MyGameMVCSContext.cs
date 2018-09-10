using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameMVCSContext : MVCSContext {

	public MyGameMVCSContext(MonoBehaviour mono) : base(mono)
    {
        Debug.Log("MVCSContext实例已创建");
    }

    protected override void mapBindings()
    {
        Debug.Log("mapBinding已完成");
    }
}
