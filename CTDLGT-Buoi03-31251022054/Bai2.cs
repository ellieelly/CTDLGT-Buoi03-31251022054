using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace CTDLGT_Buoi03_31251022054
{
    internal class Bai2
    {
        // Class chứa thông tin khách hàng
        class Customer
        {
            public int Id { get; set; }
            public long Arrival { get; set; } // Thời điểm đến
            public long Service { get; set; } // Thời gian phục vụ
        }
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("--- KẾT QUẢ KIỂM THỬ BÀI 2 ---");
                // Ca 1: 1 quầy, 3 khách
                Console.WriteLine("\nCa 1:");
                MoPhongQuay(3, 1, new long[] { 0, 1, 2 }, new long[] { 5, 3, 4 });
                // Ca 2: 2 quầy, 3 khách
                Console.WriteLine("\nCa 2:");
                MoPhongQuay(3, 2, new long[] { 0, 1, 2 }, new long[] { 5, 3, 4 });
                // Ca 3: 2 quầy, 3 khách đến cùng lúc
                Console.WriteLine("\nCa 3:");
                MoPhongQuay(3, 2, new long[] { 0, 0, 0 }, new long[] { 3, 3, 3 });
                // Ca 4: Quầy rảnh rồi mới có khách
                Console.WriteLine("\nCa 4:");
                MoPhongQuay(2, 1, new long[] { 0, 10 }, new long[] { 2, 3 });
                // Ca 5: k lớn hơn n (3 quầy, 1 khách)
                Console.WriteLine("\nCa 5:");
                MoPhongQuay(1, 3, new long[] { 4 }, new long[] { 6 });
                // Ca 6: Ca lớn n = 100.000, k = 10
                Console.WriteLine("\nCa 6 (Đang chạy ca lớn 100k khách...):");
                int n_lon = 100000;
                long[] arr_lon = new long[n_lon];
                long[] srv_lon = new long[n_lon];
                for (int i = 0; i < n_lon; i++)
                {
                    arr_lon[i] = i; // Khách i đến lúc i (theo đề là i-1, ở đây khởi tạo theo logic vòng lặp)
                    srv_lon[i] = 10;
                }
                var sw = Stopwatch.StartNew();
                // Đóng console output tạm cho ca lớn để đo thời gian chính xác, mình chạy ngầm
                MoPhongQuay(n_lon, 10, arr_lon, srv_lon, inChiTiet: false);
                sw.Stop();
                Console.WriteLine($"Thời gian chạy Ca 6: {sw.ElapsedMilliseconds} ms");
                Console.ReadLine();
            }
            // Hàm tổng quát mô phỏng
            static void MoPhongQuay(int n, int k, long[] arrivals, long[] services, bool inChiTiet = true)
            {
                // Dùng Queue để lưu khách hàng theo đúng thứ tự đến (FIFO).
                Queue<Customer> q = new Queue<Customer>();
                for (int i = 0; i < n; i++)
                {
                    q.Enqueue(new Customer { Id = i + 1, Arrival = arrivals[i], Service = services[i] });
                }
                // Mảng lưu thời điểm rảnh của từng quầy. Ban đầu các quầy rảnh ở thời điểm 0.
                long[] freeTime = new long[k];
                long totalWait = 0;
                long maxWait = 0;
                long finishTime = 0;
                while (q.Count > 0)
                {
                    // Lấy khách hàng ở đầu hàng đợi ra để xếp quầy.
                    var cus = q.Dequeue();
                    // Quét qua k quầy để tìm quầy rảnh sớm nhất.
                    int bestCounter = 0;
                    for (int j = 1; j < k; j++)
                    {
                        if (freeTime[j] < freeTime[bestCounter])
                        {
                            bestCounter = j;
                        }
                    }
                    // Thời điểm bắt đầu là giá trị lớn hơn giữa lúc quầy rảnh và lúc khách đến.
                    long start = Math.Max(freeTime[bestCounter], cus.Arrival);
                    long wait = start - cus.Arrival;
                    long end = start + cus.Service;
                    // Cộng dồn tổng thời gian chờ và cập nhật kỷ lục chờ lâu nhất.
                    totalWait += wait;
                    if (wait > maxWait) maxWait = wait;
                    // Cập nhật lại thời điểm rảnh của quầy vừa phục vụ.
                    freeTime[bestCounter] = end;
                    // Cập nhật thời điểm hoàn thành chung của toàn hệ thống.
                    if (end > finishTime) finishTime = end;

                    if (inChiTiet)
                    {
                        Console.WriteLine($"KH {cus.Id}: den {cus.Arrival}, quay {bestCounter + 1}, bat dau {start}, cho {wait}, xong {end}");
                    }
                }
                // In ra thống kê cuối cùng
                double avgWait = (double)totalWait / n;
                Console.WriteLine($"Cho trung binh: {avgWait.ToString("0.00", CultureInfo.InvariantCulture)}");
                Console.WriteLine($"Cho lau nhat: {maxWait}");
                Console.WriteLine($"Hoan thanh luc: {finishTime}");
            }
        }
    }
}
