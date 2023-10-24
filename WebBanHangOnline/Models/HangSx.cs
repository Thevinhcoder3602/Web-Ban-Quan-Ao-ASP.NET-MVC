using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models
{
    public partial class HangSx
    {
        public HangSx()
        {
            DanhMucSps = new HashSet<DanhMucSp>();
        }

        public string MaHangSx { get; set; } = null!;
        public string? HangSx1 { get; set; }
        public string? MaNuocThuongHieu { get; set; }

        public virtual ICollection<DanhMucSp> DanhMucSps { get; set; }
    }
}
