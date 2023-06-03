using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

public static class SessionService
{
    public static int KeyId { get { return GetIntValue("KeyId"); } set { SetValue("KeyId", value); } }
    public static int Id1 { get { return GetIntValue("Id1"); } set { SetValue("Id1", value); } }
    public static int Id2 { get { return GetIntValue("Id2"); } set { SetValue("Id2", value); } }
    public static int Id3 { get { return GetIntValue("Id3"); } set { SetValue("Id3", value); } }
    public static int Id4 { get { return GetIntValue("Id4"); } set { SetValue("Id4", value); } }
    public static int Id5 { get { return GetIntValue("Id5"); } set { SetValue("Id5", value); } }
    public static string No1 { get { return GetValue("No1"); } set { SetValue("No1", value); } }
    public static string No2 { get { return GetValue("No2"); } set { SetValue("No2", value); } }
    public static string No3 { get { return GetValue("No3"); } set { SetValue("No3", value); } }
    public static string No4 { get { return GetValue("No4"); } set { SetValue("No4", value); } }
    public static string No5 { get { return GetValue("No5"); } set { SetValue("No5", value); } }
    public static string Name1 { get { return GetValue("Name1"); } set { SetValue("Name1", value); } }
    public static string Name2 { get { return GetValue("Name2"); } set { SetValue("Name2", value); } }
    public static string Name3 { get { return GetValue("Name3"); } set { SetValue("Name3", value); } }
    public static string Name4 { get { return GetValue("Name4"); } set { SetValue("Name4", value); } }
    public static string Name5 { get { return GetValue("Name5"); } set { SetValue("Name5", value); } }
    public static DateTime Date1 { get { return GetDateValue("Date1"); } set { SetValue("Date1", value); } }
    public static DateTime Date2 { get { return GetDateValue("Date2"); } set { SetValue("Date2", value); } }
    public static DateTime Date3 { get { return GetDateValue("Date3"); } set { SetValue("Date3", value); } }
    public static DateTime Date4 { get { return GetDateValue("Date4"); } set { SetValue("Date4", value); } }
    public static DateTime Date5 { get { return GetDateValue("Date5"); } set { SetValue("Date5", value); } }

    public static string GetValue(string sessionName)
    {
        return GetValue(sessionName, "");
    }

    public static string GetValue(string sessionName, string defaultValue)
    {
        object obj_value = HttpContext.Current.Session[sessionName];
        if (obj_value == null) return defaultValue;
        return obj_value.ToString();
    }

    public static bool GetBoolValue(string sessionName)
    {
        return GetBoolValue(sessionName, false);
    }

    public static bool GetBoolValue(string sessionName, bool defaultValue)
    {
        object obj_value = HttpContext.Current.Session[sessionName];
        if (obj_value == null) return defaultValue;
        string str_value = obj_value.ToString();
        bool bln_value = false;
        if (!bool.TryParse(str_value, out bln_value)) bln_value = false;
        return bln_value;
    }

    public static int GetIntValue(string sessionName)
    {
        return GetIntValue(sessionName, 0);
    }

    public static int GetIntValue(string sessionName, int defaultValue)
    {
        object obj_value = HttpContext.Current.Session[sessionName];
        if (obj_value == null) return defaultValue;
        string str_value = obj_value.ToString();
        int int_value = 0;
        if (!int.TryParse(str_value, out int_value)) int_value = 0;
        return int_value;
    }

    public static DateTime GetDateValue(string sessionName)
    {
        return GetDateValue(sessionName, DateTime.MinValue);
    }

    public static DateTime GetDateValue(string sessionName, DateTime defaultValue)
    {
        object obj_value = HttpContext.Current.Session[sessionName];
        if (obj_value == null) return defaultValue;
        string str_value = obj_value.ToString();
        DateTime dtm_value = DateTime.MinValue;
        if (!DateTime.TryParse(str_value, out dtm_value)) dtm_value = DateTime.MinValue;
        return dtm_value;
    }

    public static void SetValue(string sessionName, object value)
    {
        HttpContext.Current.Session[sessionName] = value;
    }

    public static enAction GetAvtionValue(string sessionName, enAction defaultValue)
    {
        object obj_value = HttpContext.Current.Session[sessionName];
        if (obj_value == null) return defaultValue;
        string str_action_name = obj_value.ToString();
        return (enAction)Enum.Parse(typeof(enAction), str_action_name);
    }

    public static void SetAvtionValue(string sessionName, enAction actionValue)
    {
        HttpContext.Current.Session[sessionName] = Enum.GetName(typeof(enAction), actionValue);
    }
}