using System.ComponentModel.DataAnnotations;

namespace WebBanHangOnline.ViewModels
{
    public class LoginViewModel
    {

        [MaxLength(100)]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; } = null!;

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiếu 5 ký tự")]
        public string Password { get; set; } = null!;
    }
}
