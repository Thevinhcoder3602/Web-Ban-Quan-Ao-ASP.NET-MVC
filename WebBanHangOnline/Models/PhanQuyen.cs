using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models
{
    public partial class PhanQuyen
    {
        public PhanQuyen()
        {
            Users = new HashSet<User>();
        }

        public string LoaiUser { get; set; } = null!;
        public string? TenQuyen { get; set; }
        public string? GhiChu { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
