using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace CTDLGT_Buoi03_31251022054
{
    internal class Bai2
    {
        // Class chứa thông tin khách hàng, dùng class thay vì record cho dễ tiếp cận 
        class KhachHang
        {
            public int SoThuTu { get; set; }
            public long ThoiDiemDen { get; set; }
            public int ThoiGianPhucVu { get; set; }
        }
        class Program
        {
            static void Main()
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                while (true)
                {
                    Console.WriteLine("\n========== MENU BÀI 2 ==========");
                    Console.WriteLine("1. Chạy 5 ca kiểm thử đầu tiên (Ca 1 - Ca 5)");
                    Console.WriteLine("2. Chạy Ca lớn 6 (100.000 khách, 10 quầy)");
                    Console.WriteLine("3. Thoát chương trình");
                    Console.Write("Nhập lựa chọn của bạn (1-3): ");
                    string luaChon = Console.ReadLine() ?? "";
                    if (luaChon == "1")
                    {
                        Console.WriteLine("\n--- KẾT QUẢ CA 1 ĐẾN 5 ---");

                        Console.WriteLine("\nCa 1 (1 quầy, 3 khách):");
                        MoPhongQuay(3, 1, new long[] { 0, 1, 2 }, new int[] { 5, 3, 4 }, true);

                        Console.WriteLine("\nCa 2 (2 quầy, 3 khách nối tiếp):");
                        MoPhongQuay(3, 2, new long[] { 0, 1, 2 }, new int[] { 5, 3, 4 }, true);

                        Console.WriteLine("\nCa 3 (2 quầy, 3 khách đến cùng lúc):");
                        MoPhongQuay(3, 2, new long[] { 0, 0, 0 }, new int[] { 3, 3, 3 }, true);

                        Console.WriteLine("\nCa 4 (Quầy rảnh rồi mới có khách):");
                        MoPhongQuay(2, 1, new long[] { 0, 10 }, new int[] { 2, 3 }, true);

                        Console.WriteLine("\nCa 5 (Số quầy nhiều hơn khách):");
                        MoPhongQuay(1, 3, new long[] { 4 }, new int[] { 6 }, true);
                    }
                    else if (luaChon == "2")
                    {
                        Console.WriteLine("\nĐang chạy Ca 6 (100k khách)...");
                        int n = 100000;
                        int k = 10;
                        long[] den = new long[n];
                        int[] phucVu = new int[n];
                        // Khách thứ i đến lúc i - 1, thời gian phục vụ là 10.
                        for (int i = 0; i < n; i++)
                        {
                            den[i] = i > 0 ? i - 1 : 0; // Tránh khách đầu tiên có thời gian âm.
                            phucVu[i] = 10;
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        Stopwatch sw = Stopwatch.StartNew();
                        // Tham số false để không in chi tiết từng người ra màn hình, tránh treo Console.
                        MoPhongQuay(n, k, den, phucVu, false);
                        sw.Stop();
                        Console.WriteLine($"-> Thời gian chạy Ca 6: {sw.ElapsedMilliseconds} ms");
                    }
                    else if (luaChon == "3")
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
            // Hàm xử lý chính tính toán thời gian chờ ở quầy
            static void MoPhongQuay(int n, int k, long[] mangThoiDiemDen, int[] mangThoiGianPhucVu, bool inChiTiet)
            {
                // Dùng Queue (FIFO) để khách đến trước chắc chắn được phục vụ trước.
                Queue<KhachHang> hangDoi = new Queue<KhachHang>();

                for (int i = 0; i < n; i++)
                {
                    hangDoi.Enqueue(new KhachHang
                    {
                        SoThuTu = i + 1,
                        ThoiDiemDen = mangThoiDiemDen[i],
                        ThoiGianPhucVu = mangThoiGianPhucVu[i]
                    });
                }
                // Mảng lưu thời điểm rảnh của từng quầy. Các quầy ban đầu rảnh lúc 0.
                long[] thoiDiemRanh = new long[k];
                // Bắt buộc dùng long vì tổng thời gian chờ của 100k khách có thể vượt giới hạn 2 tỷ của int.
                long tongCho = 0;
                long choLauNhat = 0;
                long hoanThanhLuc = 0;
                // Xử lý từng khách cho đến khi hết hàng đợi.
                while (hangDoi.Count > 0)
                {
                    var khach = hangDoi.Dequeue();
                    // Quét qua tất cả các quầy để tìm quầy rảnh việc sớm nhất. Chi phí đoạn này là Theta(k).
                    int quayTotNhat = 0;
                    for (int i = 1; i < k; i++)
                    {
                        if (thoiDiemRanh[i] < thoiDiemRanh[quayTotNhat])
                        {
                            quayTotNhat = i;
                        }
                    }
                    // Nếu khách đến trễ hơn lúc quầy rảnh thì phục vụ ngay. Nếu đến sớm hơn lúc quầy rảnh thì phải chờ.
                    long thoiDiemBatDau = Math.Max(thoiDiemRanh[quayTotNhat], khach.ThoiDiemDen);
                    long thoiGianCho = thoiDiemBatDau - khach.ThoiDiemDen;
                    long thoiDiemXong = thoiDiemBatDau + khach.ThoiGianPhucVu;
                    // Cộng dồn vào tổng và cập nhật người chờ lâu nhất.
                    tongCho += thoiGianCho;
                    if (thoiGianCho > choLauNhat) choLauNhat = thoiGianCho;
                    // Cập nhật lại thời điểm rảnh của quầy vừa nhận khách.
                    thoiDiemRanh[quayTotNhat] = thoiDiemXong;
                    // Ghi nhận thời điểm quầy đóng cửa (khách cuối cùng rời đi).
                    if (thoiDiemXong > hoanThanhLuc) hoanThanhLuc = thoiDiemXong;
                    if (inChiTiet)
                    {
                        Console.WriteLine($"KH {khach.SoThuTu}: den {khach.ThoiDiemDen}, quay {quayTotNhat + 1}, bat dau {thoiDiemBatDau}, cho {thoiGianCho}, xong {thoiDiemXong}");
                    }
                }
                // In kết quả tổng hợp bằng định dạng số thực 2 chữ số thập phân (dấu chấm).
                double trungBinhCho = (double)tongCho / n;
                Console.WriteLine($"Cho trung binh: {trungBinhCho.ToString("0.00", CultureInfo.InvariantCulture)}");
                Console.WriteLine($"Cho lau nhat: {choLauNhat}");
                Console.WriteLine($"Hoan thanh luc: {hoanThanhLuc}");
            }
        }
    }
} 