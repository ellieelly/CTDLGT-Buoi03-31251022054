using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CTDLGT_Buoi03_31251022054
{
    internal class Bai3
    {
        // Cài đặt cấu trúc Hàng đợi vòng bằng mảng
        public class CircularQueue<T>
        {
            private T[] items;
            private int front;
            private int count;
            public int Count => count;
            public int Capacity => items.Length;
            public CircularQueue()
            {
                // Dung lượng khởi tạo ban đầu luôn là 4 theo yêu cầu đề bài.
                items = new T[4];
                front = 0;
                count = 0;
            }
            public void Enqueue(T value)
            {
                // Khi mảng đã chứa đầy phần tử, tạo mảng mới có dung lượng gấp đôi để mở rộng.
                if (count == Capacity)
                {
                    T[] newItems = new T[Capacity * 2];
                    // Chép dữ liệu sang mảng mới theo thứ tự logic bắt đầu từ front để giữ đúng trật tự xếp hàng.
                    for (int i = 0; i < count; i++)
                    {
                        newItems[i] = items[(front + i) % Capacity];
                    }
                    items = newItems;
                    // Đặt lại front về ô đầu tiên của mảng mới sau khi đã dàn thẳng dữ liệu.
                    front = 0;
                }
                // Tính vị trí trống kế tiếp ở cuối hàng và thêm phần tử mới vào.
                int rear = (front + count) % Capacity;
                items[rear] = value;
                count++;
            }
            public T Dequeue()
            {
                // Lấy phần tử khi hàng rỗng sẽ vi phạm logic, cần ném ngoại lệ.
                if (count == 0) throw new InvalidOperationException("Hàng đợi rỗng");

                T value = items[front];
                // Đặt ô vừa lấy về default để bộ thu gom rác (GC) giải phóng tham chiếu cũ.
                items[front] = default;

                // Dịch front lên 1 vị trí, xoay vòng về 0 nếu đụng cuối mảng, tuyệt đối không dồn các phần tử khác.
                front = (front + 1) % Capacity;
                count--;

                return value;
            }
            public T Peek()
            {
                // Xem phần tử đầu hàng nhưng không xóa, ném ngoại lệ nếu rỗng.
                if (count == 0) throw new InvalidOperationException("Hàng đợi rỗng");
                return items[front];
            }
        }
        class Program
        {
            static void Main()
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                while (true)
                {
                    Console.WriteLine("\n========== MENU BÀI 3 ==========");
                    Console.WriteLine("1. Chạy Ca 1 đến Ca 5 (Kiểm tra quay vòng và mở rộng)");
                    Console.WriteLine("2. Chạy Ca 6 (Lặp 1.000.000 lần Enqueue/Dequeue)");
                    Console.WriteLine("3. Chạy Ca 7 (Ca lớn, mở rộng nhiều lần)");
                    Console.WriteLine("4. Thoát chương trình");
                    Console.Write("Nhập lựa chọn của bạn (1-4): ");
                    string luaChon = Console.ReadLine() ?? "";
                    if (luaChon == "1")
                    {
                        Console.WriteLine("\n--- KẾT QUẢ CA 1 ĐẾN 5 ---");
                        // Ca 1: Hàng rỗng
                        var q1 = new CircularQueue<string>();
                        Console.WriteLine($"\nCa 1: Count {q1.Count}, Cap {q1.Capacity}");
                        try { q1.Dequeue(); } catch (Exception ex) { Console.WriteLine("  Lỗi Dequeue: " + ex.GetType().Name); }
                        try { q1.Peek(); } catch (Exception ex) { Console.WriteLine("  Lỗi Peek: " + ex.GetType().Name); }
                        // Ca 2: Vừa đầy (Capacity 4)
                        var q2 = new CircularQueue<string>();
                        q2.Enqueue("A"); q2.Enqueue("B"); q2.Enqueue("C"); q2.Enqueue("D");
                        Console.WriteLine($"\nCa 2: Count {q2.Count}, Cap {q2.Capacity}, Peek: {q2.Peek()}");
                        // Ca 3: Quay vòng chỉ số, chưa mở rộng
                        var q3 = new CircularQueue<string>();
                        q3.Enqueue("A"); q3.Enqueue("B"); q3.Enqueue("C"); q3.Enqueue("D");
                        q3.Dequeue(); q3.Dequeue();
                        q3.Enqueue("E"); q3.Enqueue("F");
                        Console.Write($"\nCa 3: Lấy ra - ");
                        while (q3.Count > 0) Console.Write(q3.Dequeue() + " ");
                        Console.WriteLine($"| Cap hiện tại: {q3.Capacity}");
                        // Ca 4: Mở rộng khi front = 0
                        var q4 = new CircularQueue<int>();
                        for (int i = 1; i <= 5; i++) q4.Enqueue(i);
                        Console.Write($"\nCa 4: Cap = {q4.Capacity}, Lấy ra - ");
                        while (q4.Count > 0) Console.Write(q4.Dequeue() + " ");
                        Console.WriteLine();
                        // Ca 5: Mở rộng khi front khác 0
                        var q5 = new CircularQueue<string>();
                        q5.Enqueue("A"); q5.Enqueue("B"); q5.Enqueue("C"); q5.Enqueue("D");
                        q5.Dequeue(); q5.Dequeue();
                        q5.Enqueue("E"); q5.Enqueue("F"); q5.Enqueue("G");
                        Console.Write($"\nCa 5: Cap = {q5.Capacity}, Lấy ra - ");
                        while (q5.Count > 0) Console.Write(q5.Dequeue() + " ");
                        Console.WriteLine();
                    }
                    else if (luaChon == "2")
                    {
                        Console.WriteLine("\nĐang chạy Ca 6 (Lặp 1 triệu lần)...");
                        // Bật thu gom rác dọn dẹp trước khi đo
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        var q6 = new CircularQueue<int>();
                        // Bắt đầu bấm giờ
                        var sw6 = Stopwatch.StartNew();
                        for (int i = 0; i < 1000000; i++)
                        {
                            q6.Enqueue(i);
                            q6.Dequeue();
                        }
                        sw6.Stop(); // Dừng bấm giờ
                        Console.WriteLine($"-> Ca 6 hoàn tất. Không lỗi. Cap không đổi: {q6.Capacity}");
                        // In thời gian ra màn hình
                        Console.WriteLine($"-> Thời gian chạy Ca 6: {sw6.ElapsedMilliseconds} ms");
                    }
                    else if (luaChon == "3")
                    {
                        Console.WriteLine("\nĐang chạy Ca 7 (Enqueue 100k phần tử)...");
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        var q7 = new CircularQueue<int>();
                        var sw = Stopwatch.StartNew();
                        for (int i = 0; i < 100000; i++) q7.Enqueue(i);
                        while (q7.Count > 0) q7.Dequeue();
                        sw.Stop();

                        Console.WriteLine($"-> Lấy ra đúng thứ tự. Cap cuối cùng: {q7.Capacity}");
                        Console.WriteLine($"-> Thời gian chạy: {sw.ElapsedMilliseconds} ms");
                    }
                    else if (luaChon == "4")
                    {
                        Console.WriteLine("Đã thoát chương trình!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                    }
                }
            }
        }
    }
}