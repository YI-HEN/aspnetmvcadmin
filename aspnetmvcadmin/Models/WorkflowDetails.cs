//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace aspnetmvcadmin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkflowDetails
    {
        public int Id { get; set; }
        public bool IsClose { get; set; }
        public bool IsApprove { get; set; }
        public bool IsReject { get; set; }
        public string MasterGuidNo { get; set; }
        public string RouteGuidNo { get; set; }
        public string RouteOrder { get; set; }
        public string RoleNo { get; set; }
        public string RoleName { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string AgentNo { get; set; }
        public string AgentName { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<System.DateTime> UserReadTime { get; set; }
        public Nullable<System.DateTime> AgentReadTime { get; set; }
        public string SignUserNo { get; set; }
        public string SignUserName { get; set; }
        public Nullable<System.DateTime> SignTime { get; set; }
        public string SignComment { get; set; }
        public string Remark { get; set; }
        public string GuidNo { get; set; }
    }
}