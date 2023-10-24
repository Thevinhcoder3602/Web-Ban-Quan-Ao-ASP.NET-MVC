using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models;

public partial class KichThuoc
{
    public string MaKichThuoc { get; set; } = null!;

    public string? KichThuoc1 { get; set; }

    public virtual ICollection<ChiTietSp> ChiTietSps { get; set; } = new List<ChiTietSp>();
}
