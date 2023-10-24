using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models
{
    public partial class MauSac
    {
        public MauSac()
        {
            ChiTietSps = new HashSet<ChiTietSp>();
        }

        public string MaMauSac { get; set; } = null!;
        public string? TenMauSac { get; set; }

        public virtual ICollection<ChiTietSp> ChiTietSps { get; set; }
    }
}
