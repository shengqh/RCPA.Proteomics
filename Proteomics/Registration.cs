using System;
using System.Collections.Generic;
using System.Text;
using System.Management;//硬件获取
using System.IO;//
using System.Security.Cryptography;//

namespace RCPA
{
  public static class Registration
  {
    /// <summary>
    /// 获取CPU编号
    /// </summary>
    /// <returns></returns>
    private static string GetCPUSerialNumber()
    {
      string cpuSerialNumber = string.Empty;
      ManagementClass mc = new ManagementClass("Win32_processor");
      ManagementObjectCollection moc = mc.GetInstances();
      foreach (ManagementObject mo in moc)
      {
        cpuSerialNumber = mo["ProcessorId"].ToString();
        break;
      }
      mc.Dispose();
      moc.Dispose();
      return cpuSerialNumber;
    }

    /// <summary>
    /// DES加密字符串
    /// </summary>
    /// <param name="encryptString">待加密的字符串</param>
    /// <param name="passWord">加密密钥,要求为8位</param>
    /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
    private static string EncryptString(string encryptString)
    {
      try
      {
        byte[] IV = { 0x78, 0xCD, 0x56, 0xEF, 0x12, 0x90, 0xAB, 0x34 };
        byte[] rgbKey = Encoding.UTF8.GetBytes("demo3344");
        byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
        DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();

        MemoryStream mStream = new MemoryStream();
        using (CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, IV), CryptoStreamMode.Write))
        {
          cStream.Write(inputByteArray, 0, inputByteArray.Length);
          cStream.FlushFinalBlock();
        }
        return Convert.ToBase64String(mStream.ToArray());
      }
      catch
      {
        return encryptString;
      }
    }

    //-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 生成的用户ID属性
    /// </summary>
    public static string UserId
    {
      get { return GetCPUSerialNumber(); }
    }

    /// <summary>
    /// 获取由程序生成的正确密匙（管理员使用）
    /// </summary>
    /// <param name="userId">用户反馈给管理员的产品编号</param>
    /// <returns>正确的授权号</returns>
    public static string GetUserPassWord(string userId)
    {
      //分析密码是否正确
      string userPassWord = EncryptString(UserId);
      return userPassWord;
    }

    /// <summary>
    /// 获取用户是否通过验证
    /// </summary>
    /// <param name="passWord">用户输入的密码</param>
    /// <returns>true:是会员，false:不是会员</returns>
    public static bool GetUserRule(string passWord)
    {
      bool isUser = false;
      try
      {
        //分析用户输入的密码是否正确
        string userId = GetCPUSerialNumber();//获取CPU编号
        string userPassWord = EncryptString(UserId);//加密CPU编号
        if (passWord == userPassWord)
        {
          isUser = true;
        }
      }
      catch
      { }
      return isUser;
    }

    /// <summary>
    /// 把密码写入配置文档
    /// </summary>
    /// <param name="passWord">用户输入的密码</param>
    /// <param name="userInfoFilePath">保存密码的配置文件路径（保存为TXT或INI格式二进制文件）</param>
    /// <returns>trur: 写入成功，false:写入失败</returns>
    public static bool WriteUserInfo(string passWord, string userInfoFilePath)
    {
      bool isSucess = false;
      try
      {
        bool isUser = GetUserRule(passWord);//获取是否通过会员验证
        if (isUser)
        {
          //创建保存密码的配置文档
          using (StreamWriter sw = File.CreateText(userInfoFilePath))
          {
            sw.WriteLine(passWord);
          }
        }
      }
      catch
      { }
      return isSucess;
    }

    /// <summary>
    /// 获取配置文件中记载的用户密码
    /// </summary>
    /// <param name="userInfoFilePath">配置文件路径</param>
    /// <returns>记载的用户密码</returns>
    public static string readUserInfo(string userInfoFilePath)
    {
      if (File.Exists(userInfoFilePath))//如果存在注册配置文件
      {
        using (StreamReader sr = File.OpenText(userInfoFilePath))
        {
          String input;
          input = sr.ReadLine();
          return input;
        }
      }
      else
      {
        return null;
      }
    }
  }
}