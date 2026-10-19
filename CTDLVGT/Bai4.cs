using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLVGT
{
    internal class MangĐong_Capacity
    {
        // Cài đặt lớp DynamicArray<T> mô phỏng cơ chế của List<T> bằng mảng tĩnh T[]
        public class DynamicArray<T>
        {
            // Mảng tĩnh nội bộ dùng làm vùng lưu trữ dữ liệu thực tế
            private T[] items;

            // Thuộc tính chỉ đọc bên ngoài, trả về số lượng phần tử thực tế hiện có trong mảng
            public int Count { get; private set; }

            // Thuộc tính trả về dung lượng (sức chứa) tổng cộng hiện tại của mảng nội bộ
            public int Capacity => items.Length;

            // Biến static dùng để theo dõi tổng số lần phần tử được sao chép khi tăng dung lượng
            public static int TotalCopies = 0;

            // Constructor khởi tạo mảng động với dung lượng mặc định là 4
            public DynamicArray(int capacity = 4)
            {
                // Kiểm tra nếu dung lượng truyền vào nhỏ hơn 0 thì ném ngoại lệ
                if (capacity < 0)
                {
                    // Ném ArgumentOutOfRangeException thông báo dung lượng khởi tạo không thể là số âm
                    throw new ArgumentOutOfRangeException(nameof(capacity), "Dung luong khoi tao khong the am!");
                }

                // Cấp phát mảng tĩnh items với dung lượng ban đầu là capacity
                items = new T[capacity];

                // Gán số lượng phần tử ban đầu trong mảng động bằng 0
                Count = 0;
            }

            // Phương thức kiểm tra và mở rộng dung lượng mảng khi bị đầy
            private void EnsureCapacity()
            {
                // Nếu số phần tử thực tế bằng với sức chứa hiện tại thì tiến hành mở rộng mảng
                if (Count == Capacity)
                {
                    // Tính dung lượng mới gấp đôi dung lượng cũ (nếu dung lượng cũ = 0 thì gán bằng 4)
                    int newCapacity = Capacity == 0 ? 4 : Capacity * 2;

                    // Tạo một mảng mới có kích thước bằng newCapacity để chứa dữ liệu mở rộng
                    T[] newItems = new T[newCapacity];

                    // Duyệt qua toàn bộ Count phần tử hiện có để sao chép từ mảng cũ sang mảng mới
                    for (int i = 0; i < Count; i++)
                    {
                        // Chép phần tử từ mảng cũ sang vị trí tương ứng ở mảng mới
                        newItems[i] = items[i];

                        // Tăng biến đếm tổng số phần tử đã phải sao chép để phục vụ phân tích khấu hao
                        TotalCopies++;
                    }

                    // Chuyển con trỏ items sang trỏ tới mảng mới; mảng cũ sẽ được thu gom rác Garbage Collector
                    items = newItems;
                }
            }

            // Phương thức thêm một phần tử item vào cuối mảng động
            public void Add(T item)
            {
                // Tự động kiểm tra và mở rộng mảng nếu sức chứa không đủ
                EnsureCapacity();

                // Gán giá trị item mới vào vị trí trống tiếp theo (tại chỉ số Count)
                items[Count] = item;

                // Tăng số lượng phần tử hợp lệ Count lên 1 unit
                Count++;
            }

            // Indexer cho phép truy cập và cập nhật phần tử theo chỉ số index giống như mảng thông thường
            public T this[int index]
            {
                get
                {
                    // Kiểm tra chỉ số index có nằm trong vùng hợp lệ [0, Count - 1] hay không
                    if (index < 0 || index >= Count)
                    {
                        // Ném ngoại lệ khi index nằm ngoài phạm vi phần tử hiện có
                        throw new ArgumentOutOfRangeException(nameof(index), "Chi so index nam ngoai pham vi!");
                    }

                    // Trả về giá trị tại vị trí index trong mảng nội bộ
                    return items[index];
                }
                set
                {
                    // Kiểm tra chỉ số index có nằm trong vùng hợp lệ [0, Count - 1] hay không
                    if (index < 0 || index >= Count)
                    {
                        // Ném ngoại lệ khi index nằm ngoài phạm vi phần tử hiện có
                        throw new ArgumentOutOfRangeException(nameof(index), "Chi so index nam ngoai pham vi!");
                    }

                    // Cập nhật giá trị mới cho phần tử tại vị trí index
                    items[index] = value;
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("================ CHUONG TRINH KIEM THU DYNAMIC ARRAY ================\n");

                // Khởi tạo mảng động các số nguyên với Capacity mặc định = 4
                DynamicArray<int> arr = new DynamicArray<int>();

                Console.WriteLine("--- 1. Them lan luot cac so tu 1 den 10 va theo doi Count, Capacity ---");

                // Vòng lặp thêm lần lượt các số từ 1 đến 10 vào mảng động
                for (int i = 1; i <= 10; i++)
                {
                    // Lưu lại Capacity trước khi thực hiện thêm phần tử
                    int oldCapacity = arr.Capacity;

                    // Thêm phần tử i vào mảng động
                    arr.Add(i);

                    // In thông tin số vừa thêm, Count và Capacity hiện tại
                    Console.Write($"Them {i,2}: Count = {arr.Count}, Capacity = {arr.Capacity}");

                    // Kiểm tra xem lần Add này có làm tăng Capacity từ 4 lên 8 hay không để ghi nhận log
                    if (oldCapacity == 4 && arr.Capacity == 8)
                    {
                        Console.Write("  <-- [Su kien: Capacity tang tu 4 len 8]");
                    }

                    // Xuống dòng sau mỗi lần in thông tin
                    Console.WriteLine();
                }

                Console.WriteLine($"\nTong so luot sao chep phan tu ghi nhận đuoc: {DynamicArray<int>.TotalCopies} lan.");

                Console.WriteLine("\n--- 2. Chung minh du lieu cu giu nguyen thu tu sau khi mo rong ---");
                Console.Write("Cac phan tu trong mang: ");

                // Duyệt từ 0 đến Count - 1 thông qua indexer để kiểm tra thứ tự dữ liệu
                for (int i = 0; i < arr.Count; i++)
                {
                    // In từng phần tử đọc qua Indexer
                    Console.Write(arr[i] + " ");
                }
                Console.WriteLine();

                Console.WriteLine("\n--- 3. Doc va cap nhat phan tu qua Indexer ---");

                // Đọc phần tử tại vị trí index 2
                Console.WriteLine($"Gia tri hien tai tai index 2: {arr[2]}");

                // Cập nhật giá trị mới 999 vào vị trí index 2
                arr[2] = 999;
                Console.WriteLine($"Gia tri tai index 2 sau khi cap nhat arr[2] = 999: {arr[2]}");

                Console.WriteLine("\n--- 4. Kiem tra cac truong hop ngoai le Indexer ---");

                // 4a. Kiểm tra index âm
                try
                {
                    Console.Write("Thu truy cap index = -1: ");
                    int val = arr[-1];
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bat duoc loi -> {ex.GetType().Name}: {ex.Message}");
                }

                // 4b. Kiểm tra index bằng Count
                try
                {
                    Console.Write($"Thu truy cap index = Count ({arr.Count}): ");
                    int val = arr[arr.Count];
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bat duoc loi -> {ex.GetType().Name}: {ex.Message}");
                }

                // 4c. Kiểm tra khởi tạo dung lượng âm
                try
                {
                    Console.Write("Thu khoi tao DynamicArray voi capacity = -5: ");
                    DynamicArray<int> invalidArr = new DynamicArray<int>(-5);
                }
                catch (Exception ex)
                {
                    //in thông bao đã bắt được lỗi lên màn hình
                    Console.WriteLine($"Bat duoc loi -> {ex.GetType().Name}: {ex.Message}");
                }
            }
        }
    }
}
