using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// 屬性值管理 Repository
/// </summary>
public class AttributeService : BaseClass
{
    /// <summary>
    /// 取得屬性預設值
    /// </summary>
    /// <example>
    /// 在屬性中設定
    /// [DefaultValue("U001")]
    /// public string StudentNo { get; set; }
    /// 在程式中設定
    /// model.GenderCode = AttributeService.GetDefaultValue<z_metaEmployees>("GenderCode").ToString();
    /// </example>
    /// <typeparam name="T">類別</typeparam>
    /// <param name="propertyName">屬性名稱</param>
    /// <returns></returns>
    public static object GetDefaultValue<T>(string propertyName)
    {
        object defaultValue = null;
        Type type = typeof(T);
        AttributeCollection attributes = TypeDescriptor.GetProperties(type)[propertyName].Attributes;
        DefaultValueAttribute myAttribute = (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
        PropertyInfo info = type.GetProperties().Where(x => x.Name == propertyName).FirstOrDefault();
        if (info != null)
        {
            string str_type = info.PropertyType.Name;
            string str_value = myAttribute.Value.ToString();

            // 用型別判斷
            if (str_type == "String") defaultValue = str_value;
            if (str_type == "Int32") defaultValue = (int)myAttribute.Value;
            if (str_type == "Decimal") defaultValue = (decimal)myAttribute.Value;
            if (str_type == "Boolean") defaultValue = (bool)myAttribute.Value;

            // 用預設值判斷
            if (str_value == "Today") defaultValue = DateTime.Today;
            if (str_value == "Now") defaultValue = DateTime.Now;

            //if (str_type == "enColor") defaultValue = (enColor)Enum.Parse(typeof(enColor), str_value);
        }
        return defaultValue;
    }

    /// 取得顯示的名稱
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public string GetDisplayName<T>(string propName)
    {
        string str_value = "";
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            DisplayAttribute propAttribute = (DisplayAttribute)attributes[typeof(DisplayAttribute)];
            if (propAttribute != null) str_value = propAttribute.Name;
        }
        return str_value;
    }

    /// <summary>
    /// 取得屬性是否為主鍵 Key
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public bool GetKey<T>(string propName)
    {
        bool bln_value = false;
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            KeyAttribute propAttribute = (KeyAttribute)attributes[typeof(KeyAttribute)];
            if (propAttribute != null) bln_value = true;
        }
        return bln_value;
    }

    /// <summary>
    /// 取得屬性是否為必需輸入欄位 Required
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public bool GetRequired<T>(string propName)
    {
        bool bln_value = false;
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            RequiredAttribute propAttribute = (RequiredAttribute)attributes[typeof(RequiredAttribute)];
            if (propAttribute != null) bln_value = true;
        }
        return bln_value;
    }

    /// 取得顯示的名稱
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public string GetDataFormatString<T>(string propName)
    {
        string str_value = "";
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            DisplayFormatAttribute propAttribute = (DisplayFormatAttribute)attributes[typeof(DisplayFormatAttribute)];
            if (propAttribute != null) str_value = propAttribute.DataFormatString;
        }
        return str_value;
    }
}