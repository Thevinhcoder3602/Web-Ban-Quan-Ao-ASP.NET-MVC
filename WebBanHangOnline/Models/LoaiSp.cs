using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models
{
    public partial class LoaiSp
    {
        public LoaiSp()
        {
            DanhMucSps = new HashSet<DanhMucSp>();
        }

        public string MaLoai { get; set; } = null!;
        public string? Loai { get; set; }

        public virtual ICollection<DanhMucSp> DanhMucSps { get; set; }
    }
}
