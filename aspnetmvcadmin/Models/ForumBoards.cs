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
    
    public partial class ForumBoards
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public string SortNo { get; set; }
        public string BoardNo { get; set; }
        public string BoardName { get; set; }
        public string IconName { get; set; }
        public string DescriptionText { get; set; }
        public string Remark { get; set; }
        public string GuidNo { get; set; }
    }
}
