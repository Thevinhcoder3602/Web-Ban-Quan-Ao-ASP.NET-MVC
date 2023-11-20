using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.ViewModels
{
    public class RegisterVM
    {
        
        public string MaKhachHang { get; set; } = null!;
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string? TenKhachHang { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Display(Name = "Điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [Remote(action: "VailidatePhone", controller: "Access")]
        public string? SoDienThoai { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; } = null!;


        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiếu 5 ký tự")]
        public string Password { get; set; } = null!;

		[Required(ErrorMessage = "Vui lòng nhập xác nhận mật khẩu.")]
		[MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiếu 5 ký tự")]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password",ErrorMessage = "Vui lòng nhập mật khẩu giống nhau")]
        public string? ConfirmPassword { get; set; }

    }
}
