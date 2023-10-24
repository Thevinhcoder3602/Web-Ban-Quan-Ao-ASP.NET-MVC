using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models
{
    public partial class ChatLieu
    {
        public ChatLieu()
        {
            DanhMucSps = new HashSet<DanhMucSp>();
        }

        public string MaChatLieu { get; set; } = null!;
        public string? ChatLieu1 { get; set; }

        public virtual ICollection<DanhMucSp> DanhMucSps { get; set; }
    }
}
