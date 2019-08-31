using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class LuaCallCSharp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.DoString("require 'LuaCallCSharp'");//从C#建立与lua的连接

        luaEnv.Dispose();
	}
	
}
