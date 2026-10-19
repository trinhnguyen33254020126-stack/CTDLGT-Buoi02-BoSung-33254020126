using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLVGT
{
    internal class QuanLyList
    {
        // Lớp đại diện cho đối tượng Sinh viên
        public class Student
        {
            // Lưu mã duy nhất dùng để nhận diện sinh viên
            public string Id { get; set; } = "";

            // Lưu họ tên dùng cho hiển thị và tìm kiếm
            public string FullName { get; set; } = "";

            // Lưu điểm tổng kết trong đoạn từ 0 đến 10
            public double Score { get; set; }
        }

        class Program
        {
            // Phương thức tìm sinh viên theo Id thủ công bằng vòng lặp (không dùng LINQ)
            static Student FindById(List<Student> list, string id)
            {
                // Duyệt tuyến tính lần lượt từng sinh viên trong danh sách từ đầu đến cuối
                for (int i = 0; i < list.Count; i++)
                {
                    // So sánh mã sinh viên hiện tại với mã cần tìm (không phân biệt hoa/thường)
                    if (list[i].Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                    {
                        // Trả về đối tượng Student nếu tìm thấy mã trùng khớp
                        return list[i];
                    }
                }
                // Trả về null nếu đã duyệt hết danh sách mà không tìm thấy sinh viên nào phù hợp
                return null;
            }

            // Chức năng 1: Thêm sinh viên mới
            static void AddStudent(List<Student> list)
            {
                // Hiển thị tiêu đề chức năng thêm sinh viên
                Console.WriteLine("\n--- THEM SINH VIEN MOI ---");

                // Nhập mã sinh viên từ bàn phím và loại bỏ khoảng trắng thừa hai đầu
                Console.Write("Nhap ma sinh vien: ");
                string id = Console.ReadLine().Trim();

                // Kiểm tra mã đã tồn tại trong danh sách chưa bằng phương thức FindById
                if (FindById(list, id) != null)
                {
                    // Thông báo từ chối và thoát khỏi hàm nếu phát hiện mã bị trùng
                    Console.WriteLine("Loi: Ma sinh vien da ton tai trong danh sach!");
                    return;
                }

                // Nhập họ và tên sinh viên
                Console.Write("Nhap ho ten: ");
                string fullName = Console.ReadLine().Trim();

                // Nhập điểm tổng kết từ bàn phím
                Console.Write("Nhap diem tong ket (0 - 10): ");
                // Thực hiện chuyển đổi chuỗi nhập thành kiểu double
                if (!double.TryParse(Console.ReadLine(), out double score) || score < 0 || score > 10)
                {
                    // Thông báo từ chối nếu điểm nhập không phải số hoặc nằm ngoài đoạn [0, 10]
                    Console.WriteLine("Loi: Diem tong ket phai la so thuc trong doan [0, 10]!");
                    return;
                }

                // Tạo đối tượng Student mới với các thông tin đã kiểm tra hợp lệ
                Student newStudent = new Student { Id = id, FullName = fullName, Score = score };

                // Thêm đối tượng sinh viên mới vào cuối danh sách; dữ liệu list.Count tăng thêm 1
                list.Add(newStudent);

                // Thông báo thêm thành công
                Console.WriteLine("Them sinh vien thanh cong!");
            }

            // Chức năng 2: Cập nhật thông tin sinh viên theo Id
            static void UpdateStudent(List<Student> list)
            {
                // Hiển thị tiêu đề chức năng cập nhật
                Console.WriteLine("\n--- CAP NHAT SINH VIEN ---");

                // Nhập mã sinh viên cần cập nhật
                Console.Write("Nhap ma sinh vien can cap nhat: ");
                string id = Console.ReadLine().Trim();

                // Tìm đối tượng sinh viên trong danh sách theo Id
                Student student = FindById(list, id);

                // Kiểm tra nếu không tìm thấy sinh viên
                if (student == null)
                {
                    // In thông báo lỗi và kết thúc phương thức nếu không tìm thấy
                    Console.WriteLine("Loi: Khong tim thay sinh vien voi ma nay!");
                    return;
                }

                // Nhập họ tên mới; nếu để trống thì giữ nguyên họ tên cũ
                Console.Write($"Nhap ho ten moi (De trong neu giu nguyen: '{student.FullName}'): ");
                string newName = Console.ReadLine().Trim();
                // Nếu người dùng có nhập chuỗi mới thì cập nhật lại FullName của sinh viên
                if (!string.IsNullOrEmpty(newName))
                {
                    student.FullName = newName;
                }

                // Nhập điểm mới; nếu để trống thì giữ nguyên điểm cũ
                Console.Write($"Nhap diem moi (De trong neu giu nguyen: {student.Score}): ");
                string inputScore = Console.ReadLine().Trim();
                // Kiểm tra người dùng có nhập dữ liệu mới hay không
                if (!string.IsNullOrEmpty(inputScore))
                {
                    // Chuyển đổi và kiểm tra tính hợp lệ của điểm mới trong khoảng [0, 10]
                    if (double.TryParse(inputScore, out double newScore) && newScore >= 0 && newScore <= 10)
                    {
                        // Cập nhật giá trị thuộc tính Score của sinh viên
                        student.Score = newScore;
                    }
                    else
                    {
                        // Thông báo khi điểm mới nhập vào không hợp lệ
                        Console.WriteLine("Loi: Diem khong hop le, giu nguyen diem cu.");
                    }
                }

                // Thông báo cập nhật thành công
                Console.WriteLine("Cap nhat thong tin sinh vien thanh cong!");
            }

            // Chức năng 3: Xóa sinh viên theo Id
            static void DeleteStudent(List<Student> list)
            {
                // Hiển thị tiêu đề chức năng xóa
                Console.WriteLine("\n--- XOA SINH VIEN ---");

                // Nhập mã sinh viên cần xóa
                Console.Write("Nhap ma sinh vien can xoa: ");
                string id = Console.ReadLine().Trim();

                // Lặp duyệt thủ công để lấy chỉ số index của sinh viên cần xóa
                int index = -1;
                for (int i = 0; i < list.Count; i++)
                {
                    // So sánh mã sinh viên tại vị trí i với mã nhập vào
                    if (list[i].Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                    {
                        // Gán chỉ số i tìm được vào biến index và ngắt vòng lặp
                        index = i;
                        break;
                    }
                }

                // Kiểm tra nếu không tìm thấy sinh viên tương ứng
                if (index == -1)
                {
                    // Thông báo không tìm thấy mã cần xóa
                    Console.WriteLine("Loi: Khong tim thay sinh vien voi ma nay!");
                    return;
                }

                // Xóa sinh viên tại vị trí index; các phần tử phía sau sẽ được dịch sang trái, list.Count giảm 1
                list.RemoveAt(index);

                // Thông báo xóa thành công
                Console.WriteLine("Xoa sinh vien thanh cong!");
            }

            // Chức năng 4: Tìm kiếm sinh viên theo một phần họ tên (không phân biệt hoa/thường)
            static void SearchByName(List<Student> list)
            {
                // Hiển thị tiêu đề chức năng tìm kiếm
                Console.WriteLine("\n--- TIM KIEM SINH VIEN THEO HO TEN ---");

                // Nhập từ khóa tìm kiếm từ bàn phím
                Console.Write("Nhap tu khoa ho ten can tim: ");
                string keyword = Console.ReadLine().Trim();

                // Biến đánh dấu kiểm tra có tìm thấy kết quả nào không
                bool found = false;

                // Duyệt qua từng sinh viên trong danh sách để kiểm tra từ khóa
                for (int i = 0; i < list.Count; i++)
                {
                    // Kiểm tra FullName có chứa từ khóa không (không phân biệt hoa/thường)
                    if (list[i].FullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // In tiêu đề cột ở lần đầu tiên tìm thấy kết quả
                        if (!found)
                        {
                            Console.WriteLine($"{"Ma SV",-10} | {"Ho va Ten",-25} | {"Diem",-5}");
                            Console.WriteLine(new string('-', 46));
                            found = true;
                        }
                        // In thông tin chi tiết của sinh viên tìm thấy
                        Console.WriteLine($"{list[i].Id,-10} | {list[i].FullName,-25} | {list[i].Score,-5:F2}");
                    }
                }

                // Nếu duyệt hết danh sách mà biến found vẫn bằng false thì thông báo không tìm thấy
                if (!found)
                {
                    Console.WriteLine("Khong tim thay sinh vien nao phu hop voi tu khoa!");
                }
            }

            // Chức năng 5: Hiển thị danh sách, điểm trung bình và số sinh viên đạt (điểm >= 5)
            static void DisplayAndStatistics(List<Student> list)
            {
                // Hiển thị tiêu đề danh sách và thống kê
                Console.WriteLine("\n--- DANH SACH VÀ THONG KE SINH VIEN ---");

                // Xử lý trường hợp danh sách rỗng để tránh lỗi chia cho 0
                if (list.Count == 0)
                {
                    // Thông báo danh sách chưa có sinh viên nào
                    Console.WriteLine("Danh sach sinh vien hien dang rong!");
                    return;
                }

                // In tiêu đề các cột dữ liệu
                Console.WriteLine($"{"Ma SV",-10} | {"Ho va Ten",-25} | {"Diem",-5}");
                Console.WriteLine(new string('-', 46));

                // Khởi tạo biến tích lũy tổng điểm kiểu double
                double totalScore = 0;

                // Khởi tạo biến đếm số lượng sinh viên đạt điểm >= 5
                int countPassed = 0;

                // Duyệt qua tất cả sinh viên để in thông tin và tính toán thống kê
                for (int i = 0; i < list.Count; i++)
                {
                    // In ra thông tin sinh viên hiện tại
                    Console.WriteLine($"{list[i].Id,-10} | {list[i].FullName,-25} | {list[i].Score,-5:F2}");

                    // Cộng dồn điểm của sinh viên hiện tại vào biến totalScore
                    totalScore += list[i].Score;

                    // Kiểm tra nếu điểm của sinh viên từ 5.0 trở lên thì tăng biến đếm sinh viên đạt
                    if (list[i].Score >= 5.0)
                    {
                        countPassed++;
                    }
                }

                // Tính điểm trung bình bằng cách lấy tổng điểm chia cho tổng số sinh viên list.Count
                double avgScore = totalScore / list.Count;

                // In đường phân cách và hiển thị các thông số thống kê
                Console.WriteLine(new string('-', 46));
                Console.WriteLine($"Tong so sinh vien: {list.Count}");
                Console.WriteLine($"Diem trung binh  : {avgScore:F2}");
                Console.WriteLine($"So SV dat (>=5.0): {countPassed}");
            }

            static void Main(string[] args)
            {
                // Khởi tạo danh sách động chứa các đối tượng sinh viên
                List<Student> studentList = new List<Student>();

                // Vòng lặp chính duy trì menu tương tác cho đến khi người dùng chọn thoát
                while (true)
                {
                    // In menu chức năng của chương trình
                    Console.WriteLine("\n===== CHUONG TRINH QUAN LY SINH VIEN =====");
                    Console.WriteLine("1. Them sinh vien");
                    Console.WriteLine("2. Cap nhat sinh vien theo ma");
                    Console.WriteLine("3. Xoa sinh vien theo ma");
                    Console.WriteLine("4. Tim kiem sinh vien theo ho ten");
                    Console.WriteLine("5. Hien thi danh sach va thong ke");
                    Console.WriteLine("6. Thoat");
                    Console.Write("Chon chuc nang (1-6): ");

                    // Đọc lựa chọn từ bàn phím
                    string choice = Console.ReadLine().Trim();

                    // Dùng cấu trúc switch-case để điều hướng gọi các phương thức chức năng tương ứng
                    switch (choice)
                    {
                        case "1":
                            AddStudent(studentList);
                            break;
                        case "2":
                            UpdateStudent(studentList);
                            break;
                        case "3":
                            DeleteStudent(studentList);
                            break;
                        case "4":
                            SearchByName(studentList);
                            break;
                        case "5":
                            DisplayAndStatistics(studentList);
                            break;
                        case "6":
                            Console.WriteLine("Da thoat chuong trinh.");
                            return; // Kết thúc hàm Main để thoát chương trình
                        default:
                            Console.WriteLine("Lua chon khong hop le! Vui long chon tu 1 den 6.");
                            break;
                    }
                }
            }
        }
    }
}
