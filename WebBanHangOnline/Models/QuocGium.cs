using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models;

public partial class QuocGium
{
    public string MaNuoc { get; set; } = null!;

    public string? TenNuoc { get; set; }

    public virtual ICollection<DanhMucSp> DanhMucSps { get; set; } = new List<DanhMucSp>();
}
