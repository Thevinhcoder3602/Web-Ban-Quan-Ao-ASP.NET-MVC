using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models;

public partial class PhanQuyen
{
    public string IdQuyen { get; set; } = null!;

    public string? TenQuyen { get; set; }

    public string? GhiChu { get; set; }
}
