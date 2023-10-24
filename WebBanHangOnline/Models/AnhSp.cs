using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models;

public partial class AnhSp
{
    public string MaSp { get; set; } = null!;

    public string TenFileAnh { get; set; } = null!;

    public short? ViTri { get; set; }

    public virtual DanhMucSp MaSpNavigation { get; set; } = null!;
}
