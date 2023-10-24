using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models;

public partial class ChucNang
{
    public string Id { get; set; } = null!;

    public string? TenChucNang { get; set; }

    public string? MaChucNang { get; set; }
}
