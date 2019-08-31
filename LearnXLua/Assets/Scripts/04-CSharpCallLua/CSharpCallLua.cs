using System;
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
        //GetLuaTableByClass();
        //GetLuaTableByInterface();
        //GetLuaTableByDictionary();
        //GetLuaTableByList();
        //GetLuaTableByLuaTable();

        //GetLuaFunction();
        GetLuaFunctionByLuaFunction();
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
    /// 1.通过类获取lua的表
    /// </summary>
    void GetLuaTableByClass()
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

    /// <summary>
    /// 2.通过接口获得表
    /// </summary>
    void GetLuaTableByInterface()
    {
        //自动将表的键值对映射到接口的对应名字字段里.注意这里是引用，会修改表person的内容
        IPerson p = luaEnv.Global.Get<IPerson>("person");
        print("p:"+p.name + "---" + p.age);

        //修改接口的内容看表的内容会不会跟着改变
        p.name = "陈华大";
        p.age = 30;

        luaEnv.DoString("print(person.name..':'..person.age)");
        p.showinf(50,30);
    }

    [CSharpCallLua]//使用代码生成器会生成这个interface的实例
    interface IPerson
    {
        //接口的字段要和表的键名同名
        string name{ get; set; }
        int age { get; set; }
        void showinf(int a,int b);
    }

    /// <summary>
    /// 3.通过字典获得lua的表
    /// </summary>
    void GetLuaTableByDictionary()
    {
        Dictionary<string, object> dict = luaEnv.Global.Get<Dictionary<string, object>>("person");
        foreach(var i in dict)
        {
            print(i);
        }
    }

    /// <summary>
    /// 3.通过集合获得lua的表
    /// </summary>
    void GetLuaTableByList()
    {
        //用object是因为c#的所有类型都继承于object，这样就可以获得表中所有类型的元素
        //通过list的话，只能拿那些没有键的元素，此时的表相当于数组
        List<object> l = luaEnv.Global.Get<List<object>>("person");
        foreach (var i in l)
        {
            print(i);
        }
    }

    /// <summary>
    /// 通过xlua提供的luatable获得lua的表，但是性能降低
    /// </summary>
    void GetLuaTableByLuaTable()
    {
        LuaTable luaTable = luaEnv.Global.Get<LuaTable>("person");
        //输出表中的内容
        print(luaTable.Get<string>("name"));
        print(luaTable.Get<int>("age"));
        //获得表中的所有键
        foreach(var i in luaTable.GetKeys())
        {
            print(i);
        }
    }

    /// <summary>
    /// 获取lua中的函数
    /// </summary>
    void GetLuaFunction()
    {
        //通过委托来获取一个函数，装入一个函数到委托
        Add add= luaEnv.Global.Get<Add>("add");
        int resa,resb;
        int res=add(50,90,out resa,out resb); //调用委托
        print("res:"+res+"resa:"+resa+"resb:"+resb);
        add = null;   //将委托引用lua的函数释放掉，使lua虚拟机可以正常释放

    }
    [CSharpCallLua]  //需要代码生成器生成一个委托实例
    delegate int Add(int a, int b,out int resa,out int resb);

    /// <summary>
    /// 通过LuaFunction获取lua中的函数，但是性能低
    /// </summary>
    void GetLuaFunctionByLuaFunction()
    {
        LuaFunction func = luaEnv.Global.Get<LuaFunction>("add");
        object[] ob=func.Call(3,4);  //通过call调用函数，call可以传入任意类型，多参数。返回多个值
        foreach(var i in ob)
        {
            print(i);
        }
    }


}
