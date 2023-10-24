using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models
{
    public partial class User
    {
        public User()
        {
            KhachHangs = new HashSet<KhachHang>();
            NhanViens = new HashSet<NhanVien>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? LoaiUser { get; set; }

        public virtual PhanQuyen? LoaiUserNavigation { get; set; }
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
