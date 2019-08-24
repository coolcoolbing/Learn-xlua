using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
public class RunLuaByFile : MonoBehaviour {

    public TextAsset ta2;
	// Use this for initialization
	IEnumerator Start () {

        string filePath = "./Assets/Resources/helloworld.lua.txt";
        if (!File.Exists(filePath)) { Debug.LogError("文件路径不存在");  yield  break;  }

        Debug.Log(ta2);

        LuaEnv luaEnv = new LuaEnv();  //创建lua虚拟机
        
        luaEnv.DoString("require 'helloworld' print('hahahahhaha')");     //执行txt里面的lua程序
        luaEnv.Dispose();

    }
}
