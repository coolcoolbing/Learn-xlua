using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XLua;   //引入xlua命名空间
public class Runlua : MonoBehaviour {

    LuaEnv luaEnv; //lua的解析器以及虚拟机运行环境


	void Start () {
        luaEnv = new LuaEnv();   //尽量保持这个虚拟机全局唯一

        luaEnv.DoString("print('hello world!')");      //通过该方法编写lua代码并执行
        luaEnv.DoString("CS.UnityEngine.Debug.Log('lua调用C#')");      //通过该方法编写lua代码并执行,通过lua调用c#的类和方法
        luaEnv.Dispose(); //销毁lua解析器及虚拟机
    }
	
    //当场景转换或游戏退出时调用
	private void OnDestroy()
    {
        //luaEnv.Dispose(); //销毁lua解析器及虚拟机
    }
}
