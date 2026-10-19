using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLVGT
{
    internal class Chen_Xoa
    {
        class Program
        {
            // Phương thức chèn giá trị value vào vị trí index của mảng data có vùng trống
            static void Insert(int[] data, ref int count, int index, int value)
            {
                // Kiểm tra chỉ số index có hợp lệ hay không (phải từ 0 đến count); nếu không thì ném ngoại lệ
                if (index < 0 || index > count)
                {
                    // Ném ngoại lệ ArgumentOutOfRangeException thông báo chỉ số chèn nằm ngoài phạm vi cho phép
                    throw new ArgumentOutOfRangeException(nameof(index), "Index chen khong hop le!");
                }

                // Kiểm tra mảng đã đầy chưa bằng cách so sánh số phần tử hợp lệ với kích thước thực tế của mảng
                if (count >= data.Length)
                {
                    // Ném ngoại lệ InvalidOperationException khi mảng không còn ô trống để chèn thêm
                    throw new InvalidOperationException("Mang da day, khong the chen them!");
                }

                // Duyệt từ phải sang trái (từ count - 1 xuống index) để dồn các phần tử sang phải 1 vị trí
                for (int i = count - 1; i >= index; i--)
                {
                    // Gán giá trị của phần tử phía trước sang vị trí bên phải kề nó; dữ liệu tại data[i + 1] thay đổi
                    data[i + 1] = data[i];
                }

                // Gán giá trị mới value vào ô trống đã được tạo ra tại vị trí index
                data[index] = value;

                // Tăng biến count thêm 1 unit vì mảng vừa tiếp nhận thêm 1 phần tử hợp lệ
                count++;
            }

            // Phương thức xóa phần tử tại vị trí index của mảng data
            static void RemoveAt(int[] data, ref int count, int index)
            {
                // Kiểm tra chỉ số index có hợp lệ hay không (phải từ 0 đến count - 1); nếu không thì ném ngoại lệ
                if (index < 0 || index >= count)
                {
                    // Ném ngoại lệ ArgumentOutOfRangeException thông báo chỉ số xóa nằm ngoài phạm vi cho phép
                    throw new ArgumentOutOfRangeException(nameof(index), "Index xoa khong hop le!");
                }

                // Duyệt từ trái sang phải (từ index đến count - 2) để dồn các phần tử phía sau sang trái 1 vị trí
                for (int i = index; i < count - 1; i++)
                {
                    // Gán giá trị của phần tử bên phải kề nó đè lên vị trí hiện tại; dữ liệu tại data[i] thay đổi
                    data[i] = data[i + 1];
                }

                // Giảm biến count đi 1 unit vì một phần tử hợp lệ đã bị loại bỏ khỏi mảng
                count--;
            }

            // Hàm hỗ trợ in các phần tử hợp lệ hiện có trong mảng
            static void PrintArray(int[] data, int count)
            {
                // In thông báo bắt đầu mảng
                Console.Write("[ ");

                // Vòng lặp duyệt qua tất cả các phần tử hợp lệ từ chỉ số 0 đến count - 1
                for (int i = 0; i < count; i++)
                {
                    // In từng phần tử hợp lệ kèm khoảng trắng
                    Console.Write(data[i] + " ");
                }

                // In dấu đóng ngoặc vuông và xuống dòng sau khi in xong
                Console.WriteLine($"] (count = {count}, capacity = {data.Length})");
            }

            static void Main(string[] args)
            {
                // Cấp phát mảng data gồm 10 ô nhớ để có sẵn vùng trống cho thao tác chèn
                int[] data = new int[10];

                // Khởi tạo 5 phần tử ban đầu [4, 7, 2, 9, 5]
                data[0] = 4; data[1] = 7; data[2] = 2; data[3] = 9; data[4] = 5;

                // Đặt số lượng phần tử hợp lệ ban đầu count = 5
                int count = 5;

                // In trạng thái ban đầu của mảng
                Console.Write("Ban dau: ");
                PrintArray(data, count);

                // ------------------ CA KIỂM THỬ YÊU CẦU ------------------

                // Ca 1: Chèn 99 tại index 2, sau đó xóa phần tử tại index 1
                Console.WriteLine("\n--- Ca 1: Chen 99 tai index 2, xoa tai index 1 ---");
                Insert(data, ref count, 2, 99);
                Console.Write("Sau khi chen 99 tai index 2: ");
                PrintArray(data, count);

                RemoveAt(data, ref count, 1);
                Console.Write("Sau khi xoa tai index 1: ");
                PrintArray(data, count);

                // Ca 2: Chèn tại đầu (index 0) và chèn tại cuối (index count)
                Console.WriteLine("\n--- Ca 2: Chen tai dau va chen tai cuoi ---");
                Insert(data, ref count, 0, 100); // Chèn tại đầu
                Console.Write("Sau khi chen 100 tai dau (index 0): ");
                PrintArray(data, count);

                Insert(data, ref count, count, 200); // Chèn tại cuối
                Console.Write("Sau khi chen 200 tai cuoi (index = count): ");
                PrintArray(data, count);

                // Ca 3: Xóa tại đầu (index 0) và xóa phần tử tại cuối (index count - 1)
                Console.WriteLine("\n--- Ca 3: Xoa tai dau va xoa tai cuoi ---");
                RemoveAt(data, ref count, 0); // Xóa tại đầu
                Console.Write("Sau khi xoa tai dau (index 0): ");
                PrintArray(data, count);

                RemoveAt(data, ref count, count - 1); // Xóa tại cuối
                Console.Write("Sau khi xoa tai cuoi (index = count - 1): ");
                PrintArray(data, count);

                // Ca 4: Thử các trường hợp ngoại lệ
                Console.WriteLine("\n--- Ca 4: Thu cac truong hop ngoai le ---");

                // 4a. Thử index âm khi Insert
                try
                {
                    Insert(data, ref count, -1, 50);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Loi (Insert index -1): {ex.GetType().Name} - {ex.Message}");
                }

                // 4b. Thử index bằng count khi RemoveAt
                try
                {
                    RemoveAt(data, ref count, count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Loi (RemoveAt index = count): {ex.GetType().Name} - {ex.Message}");
                }

                // 4c. Thử chèn khi mảng đã đầy
                int[] fullArray = new int[3] { 1, 2, 3 };
                int fullCount = 3;
                try
                {
                    Insert(fullArray, ref fullCount, 1, 999);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Loi (Insert vao mang day): {ex.GetType().Name} - {ex.Message}");
                }
            }
        }
    }
}
