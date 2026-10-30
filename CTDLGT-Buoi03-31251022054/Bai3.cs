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
                // Khi mảng đã chứa đầy phần tử, tiến hành mở rộng mảng gấp đôi.
                if (count == Capacity)
                {
                    T[] newItems = new T[Capacity * 2];
                    for (int i = 0; i < count; i++)
                    {
                        // Chép phần tử theo thứ tự logic bắt đầu từ front sang mảng mới.
                        newItems[i] = items[(front + i) % Capacity];
                    }
                    items = newItems;
                    // Đặt lại front về đầu mảng mới sau khi đã dàn thẳng dữ liệu.
                    front = 0;
                }

                // Tính vị trí cuối hàng hiện tại bằng công thức quay vòng và thêm phần tử.
                int rear = (front + count) % Capacity;
                items[rear] = value;
                count++;
            }

            public T Dequeue()
            {
                // Không có phần tử để lấy ra.
                if (count == 0) throw new InvalidOperationException("Hàng đợi rỗng");

                T value = items[front];
                // Đặt ô vừa lấy về default để bộ thu gom rác giải phóng tham chiếu cũ.
                items[front] = default;

                // Dịch front lên 1 vị trí, xoay vòng về 0 nếu đụng cuối mảng, không dồn mảng.
                front = (front + 1) % Capacity;
                count--;
                return value;
            }

            public T Peek()
            {
                if (count == 0) throw new InvalidOperationException("Hàng đợi rỗng");
                return items[front];
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine("--- KẾT QUẢ KIỂM THỬ BÀI 3 ---");

                // Ca 1: Hàng rỗng
                var q1 = new CircularQueue<string>();
                Console.WriteLine($"Ca 1: Count {q1.Count}, Cap {q1.Capacity}");
                try { q1.Dequeue(); } catch (Exception ex) { Console.WriteLine("  Lỗi Dequeue: " + ex.GetType().Name); }
                try { q1.Peek(); } catch (Exception ex) { Console.WriteLine("  Lỗi Peek: " + ex.GetType().Name); }

                // Ca 2: Vừa đầy
                var q2 = new CircularQueue<string>();
                q2.Enqueue("A"); q2.Enqueue("B"); q2.Enqueue("C"); q2.Enqueue("D");
                Console.WriteLine($"\nCa 2: Count {q2.Count}, Cap {q2.Capacity}, Peek: {q2.Peek()}");

                // Ca 3: Quay vòng chỉ số, chưa mở rộng
                var q3 = new CircularQueue<string>();
                q3.Enqueue("A"); q3.Enqueue("B"); q3.Enqueue("C"); q3.Enqueue("D");
                q3.Dequeue(); q3.Dequeue(); // Lấy A, B
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
                q5.Dequeue(); q5.Dequeue(); // Lấy A, B (front chuyển tới index 2)
                q5.Enqueue("E"); q5.Enqueue("F"); q5.Enqueue("G"); // Gây đầy và kích hoạt mở rộng
                Console.Write($"\nCa 5: Cap = {q5.Capacity}, Lấy ra - ");
                while (q5.Count > 0) Console.Write(q5.Dequeue() + " ");
                Console.WriteLine();

                // Ca 6: Lặp 1.000.000 lần Enqueue rồi Dequeue (Quay vòng nhiều lần)
                var q6 = new CircularQueue<int>();
                for (int i = 0; i < 1000000; i++)
                {
                    q6.Enqueue(i);
                    q6.Dequeue();
                }
                Console.WriteLine($"\nCa 6: Lặp 1 triệu lần xong. Cap không đổi: {q6.Capacity}");

                // Ca 7: Ca lớn nhiều lần mở rộng
                var q7 = new CircularQueue<int>();
                var sw = Stopwatch.StartNew();
                for (int i = 0; i < 100000; i++) q7.Enqueue(i);
                while (q7.Count > 0) q7.Dequeue();
                sw.Stop();
                Console.WriteLine($"\nCa 7: Enqueue 100k rồi Dequeue hết. Cap cuối: {q7.Capacity}, Thời gian: {sw.ElapsedMilliseconds} ms");

                Console.ReadLine();
            }
        }
    }
}
