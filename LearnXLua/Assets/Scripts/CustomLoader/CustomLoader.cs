using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class CustomLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LuaEnv env = new LuaEnv();

        env.AddLoader(MyLoader);  //添加自己的loader到委托里

        //使用loader去寻找lua模块，只要有一个loader返回了内容，就停下剩余loader的寻找
        env.DoString("require 'test007'");

        env.Dispose();
	}
	
    /// <summary>
    /// 自定义的loader
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns></returns>
    private byte[] MyLoader(ref string filePath)
    {
        print(Application.streamingAssetsPath);//输出streamingAssetsPath文件夹的路径

        //通过streamingAssetsPath文件夹自定义的路径下寻找文件，加好文件后缀
        string absPath = Application.streamingAssetsPath+"/"+ filePath+".lua.txt";

        string s=System.IO.File.ReadAllText(absPath);//读取txt文件的每一行内容
        
        return System.Text.Encoding.UTF8.GetBytes(s);//将找到的lua代码变成字节数组
    }
}
