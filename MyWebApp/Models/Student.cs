using System;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Student
    {
        public int Id { get; set; } // Mã sinh viên

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải từ 4 đến 100 ký tự.")]
        public string? Name { get; set; }

        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Email bắt buộc phải được nhập.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email phải có định dạng chứa đuôi @gmail.com.")]
        public string? Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 ký tự trở lên.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "Mật khẩu phải chứa ít nhất 1 chữ hoa, 1 chữ thường, 1 chữ số và 1 ký tự đặc biệt.")]
        public string? Password { get; set; }

        [Display(Name = "Ngành học")]
        [Required(ErrorMessage = "Vui lòng chọn ngành học.")]
        public Branch? Branch { get; set; }

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
        public Gender? Gender { get; set; }

        [Display(Name = "Hệ đào tạo")]
        public bool IsRegular { get; set; } // True: Chính quy, False: Phi chính quy

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh.")]
        [Range(typeof(DateTime), "1/1/1963", "12/31/2005", ErrorMessage = "Ngày sinh phải nằm trong khoảng từ 01/01/1963 đến 31/12/2005.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBorth { get; set; }

        [Display(Name = "Điểm tổng kết")]
        [Required(ErrorMessage = "Điểm bắt buộc phải nhập.")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải nằm trong khoảng từ 0.0 đến 10.0.")]
        public double Score { get; set; }

        public string? AvatarUrl { get; set; }
    }
}