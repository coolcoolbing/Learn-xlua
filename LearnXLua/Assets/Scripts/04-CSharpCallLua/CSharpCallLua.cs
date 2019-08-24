using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class CSharpCallLua : MonoBehaviour {

    LuaEnv luaEnv;
    // Use this for initialization
    void Start () {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'CSharpCallLua'");

        //GetLuaGlobal();// 获得lua里的全局变量
        GetLuaTable();

        luaEnv.Dispose();
	}

	/// <summary>
    /// 获得lua里的全局变量
    /// </summary>
    void GetLuaGlobal()
    {
        int a=luaEnv.Global.Get<int>("a");//获得lua里面的全局变量a
        print(a);
        string str = luaEnv.Global.Get<string>("str");//获得lua里面的全局变量a
        print(str);
        bool ok = luaEnv.Global.Get<bool>("isOk");//获得lua里面的全局变量a
        print(ok);
    }

    /// <summary>
    /// 获取lua的表
    /// </summary>
    void GetLuaTable()
    {
        //自动将表的键值对映射到类的对应名字字段里.注意这里只是值拷贝，不会修改表person的内容
        Person p = luaEnv.Global.Get<Person>("person");
        print(p.name+"---"+p.age);
    }
    class Person
    {
        public string name;
        public int age;
    }
}
