using System;

namespace ThongKeVaDaoMang
{
    class Program
    {
        static void Main(string[] args)
        {
            // Đọc số phần tử n từ bàn phím và chuyển sang kiểu int để xác định kích thước mảng
            int n = int.Parse(Console.ReadLine());

            // Tạo mảng số nguyên a gồm n phần tử để lưu dữ liệu input
            int[] a = new int[n];

            // Đọc chuỗi chứa n số nguyên phân tách bởi khoảng trắng và tách thành mảng các chuỗi
            string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Vòng lặp lưu các giá trị từ chuỗi nhập vào mảng số nguyên a
            for (int i = 0; i < n; i++)
            {
                // Chuyển kiểu từng chuỗi số sang kiểu int và gán vào phần tử a[i] tương ứng
                a[i] = int.Parse(input[i]);
            }

            // Khởi tạo biến tổng mang kiểu long để tránh tràn số khi cộng dồn tối đa 100,000 số int
            long sum = a[0];

            // Khởi tạo min là phần tử đầu tiên để đảm bảo so sánh đúng kể cả khi tất cả các số đều lớn hơn 0
            int min = a[0];

            // Khởi tạo max là phần tử đầu tiên để đảm bảo so sánh đúng kể cả khi tất cả các số đều nhỏ hơn 0
            int max = a[0];

            // Khởi tạo biến đếm số lượng phần tử có giá trị âm (< 0)
            int countNegative = 0;

            // Khởi tạo biến đếm số lượng phần tử có giá trị dương (> 0)
            int countPositive = 0;

            // Khởi tạo biến đếm số lượng phần tử có giá trị bằng 0
            int countZero = 0;

            // Kiểm tra phần tử đầu tiên a[0] để cập nhật các biến đếm phân loại số âm, dương, zero
            if (a[0] < 0) countNegative++;
            else if (a[0] > 0) countPositive++;
            else countZero++;

            // Duyệt một lần các phần tử còn lại từ chỉ số 1 đến n - 1 để thực hiện thống kê
            for (int i = 1; i < n; i++)
            {
                // Cộng giá trị a[i] vào biến tổng sum; dữ liệu sum tăng thêm giá trị a[i]
                sum += a[i];

                // Nếu phần tử a[i] nhỏ hơn giá trị min hiện tại thì cập nhật min bằng a[i]
                if (a[i] < min) min = a[i];

                // Nếu phần tử a[i] lớn hơn giá trị max hiện tại thì cập nhật max bằng a[i]
                if (a[i] > max) max = a[i];

                // Kiểm tra nếu a[i] là số âm thì tăng biến đếm số âm lên 1 unit
                if (a[i] < 0) countNegative++;
                // Kiểm tra nếu a[i] là số dương thì tăng biến đếm số dương lên 1 unit
                else if (a[i] > 0) countPositive++;
                // Nếu không âm và không dương thì chắc chắn là 0, tăng biến đếm số 0 lên 1 unit
                else countZero++;
            }

            // Ép kiểu sum sang double trước khi chia cho n để thực hiện phép chia thực, lấy trung bình chính xác
            double avg = (double)sum / n;

            // In tổng sum, min, max, trung bình cộng làm tròn 2 chữ số thập phân (định dạng F2)
            Console.WriteLine($"Tong: {sum}, Min: {min}, Max: {max}, Trung binh: {avg:F2}");

            // In số lượng các số âm, dương và bằng 0 ra màn hình
            Console.WriteLine($"Am: {countNegative}, Duong: {countPositive}, Bang 0: {countZero}");

            // Khởi tạo chỉ số left bắt đầu từ vị trí đầu tiên của mảng (chỉ số 0) để chuẩn bị đảo mảng
            int left = 0;

            // Khởi tạo chỉ số right bắt đầu từ vị trí cuối cùng của mảng (chỉ số n - 1)
            int right = n - 1;

            // Lặp hoán đổi hai phần tử ở hai đầu mảng cho đến khi hai chỉ số left và right gặp nhau hoặc vượt qua nhau
            while (left < right)
            {
                // Lưu tạm giá trị a[left] vào biến temp để không bị mất dữ liệu khi ghi đè
                int temp = a[left];

                // Gán giá trị của a[right] vào vị trí a[left]; dữ liệu tại a[left] đổi thành a[right]
                a[left] = a[right];

                // Gán giá trị temp (giá trị ban đầu của a[left]) vào a[right]; hoàn tất hoán đổi 2 phần tử
                a[right] = temp;

                // Dịch chuyển chỉ số left sang phải 1 vị trí để chuẩn bị hoán đổi cặp tiếp theo
                left++;

                // Dịch chuyển chỉ số right sang trái 1 vị trí để chuẩn bị hoán đổi cặp tiếp theo
                right--;
            }

            // In thông báo mảng sau khi đã đảo ngược tại chỗ
            Console.Write("Mang sau khi dao: ");

            // Vòng lặp duyệt qua tất cả phần tử của mảng a để in kết quả mảng đã đảo
            for (int i = 0; i < n; i++)
            {
                // In từng phần tử a[i] kèm khoảng trắng
                Console.Write(a[i] + " ");
            }
            // Xuống dòng sau khi in xong mảng
            Console.WriteLine();
        }
    }
}