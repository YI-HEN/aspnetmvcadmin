using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class ViewService
{
    public static enAction Action { get; set; } = enAction.Index;
    public static string ActionName { get { return ActionService.GetActionName(Action); } }
    public static string CardHeaderText { get; set; } = "";
    public static enCardSize CardSize { get; set; } = enCardSize.max;
    public static string CardSizeCss
    {
        get
        {
            string str_css = "card-size-max";
            if (CardSize == enCardSize.small) str_css = "card-size-small";
            if (CardSize == enCardSize.medium) str_css = "card-size-medium";
            if (CardSize == enCardSize.large) str_css = "card-size-large";
            return str_css;
        }
    }
    public static void SetView(enAction action, enCardSize cardSize)
    {
        Action = action;
        CardSize = cardSize;
        CardHeaderText = "";
    }

    public static void SetView(enAction action, enCardSize cardSize, string cardHeaderText)
    {
        Action = action;
        CardSize = cardSize;
        CardHeaderText = cardHeaderText;
    }
}