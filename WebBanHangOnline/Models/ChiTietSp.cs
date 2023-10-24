using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models;

public partial class ChiTietSp
{
    public string MaChiTietSp { get; set; } = null!;

    public string? MaSp { get; set; }

    public string? MaKichThuoc { get; set; }

    public string? MaMauSac { get; set; }

    public string? AnhDaiDien { get; set; }

    public string? Video { get; set; }

    public double? DonGiaBan { get; set; }

    public double? GiamGia { get; set; }

    public int? Slton { get; set; }

    public virtual ICollection<AnhChiTietSp> AnhChiTietSps { get; set; } = new List<AnhChiTietSp>();

    public virtual ICollection<ChiTietHdban> ChiTietHdbans { get; set; } = new List<ChiTietHdban>();

    public virtual KichThuoc? MaKichThuocNavigation { get; set; }

    public virtual MauSac? MaMauSacNavigation { get; set; }

    public virtual DanhMucSp? MaSpNavigation { get; set; }
}
