using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CTDLGT_Buoi03_31251022054
{
    internal class Bai1
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n========== MENU BÀI 1 ==========");
                Console.WriteLine("1. Tự nhập chuỗi ký tự");
                Console.WriteLine("2. Chạy tự động 8 ca kiểm thử (Ca 1 - Ca 8)");
                Console.WriteLine("3. Ca lớn 9 (500.000 cặp ngoặc)");
                Console.WriteLine("4. Ca lớn 10 (1.000.000 ngoặc mở)");
                Console.WriteLine("5. Thoát chương trình");
                Console.Write("Nhập lựa chọn của bạn (1-5): ");

                string luaChon = Console.ReadLine() ?? "";

                if (luaChon == "1")
                {
                    Console.Write("Nhập chuỗi ký tự cần kiểm tra: ");
                    string chuoiNhap = Console.ReadLine() ?? "";
                    string ketQua = KiemTraNgoacCanBang(chuoiNhap);
                    Console.WriteLine($"-> Kết quả: {ketQua}");
                }
                else if (luaChon == "2")
                {
                    Console.WriteLine("\n--- KẾT QUẢ 8 CA KIỂM THỬ ---");
                    // Tập hợp 8 ca kiểm thử bắt buộc theo Bảng 2 của đề bài.
                    string[] tapCaKiemThu = {
                        "{(a+b)×[c−d]}",  // Ca 1: Thông thường[cite: 1]
                        "{(a+b]×c)",      // Ca 2: Đóng sai loại[cite: 1]
                        "(a+b",           // Ca 3: Mở chưa đóng[cite: 1]
                        "a+b)",           // Ca 4: Đóng thừa[cite: 1]
                        "",               // Ca 5: Chuỗi rỗng[cite: 1]
                        "([)]",           // Ca 6: Các cặp đan xen nhau[cite: 1]
                        "a+b",            // Ca 7: Không có ngoặc[cite: 1]
                        "{[("             // Ca 8: Nhiều ngoặc mở chưa đóng[cite: 1]
                    };
                    for (int i = 0; i < tapCaKiemThu.Length; i++)
                    {
                        string chuoiTest = tapCaKiemThu[i];
                        string ketQuaTest = KiemTraNgoacCanBang(chuoiTest);
                        string hienThiChuoi = chuoiTest == "" ? "(chuỗi rỗng)" : chuoiTest;
                        Console.WriteLine($"Ca {i + 1}: {ketQuaTest,-15} | Chuỗi: {hienThiChuoi}");
                    }
                }
                else if (luaChon == "3")
                {
                    Console.WriteLine("\nĐang chạy Ca 9: 500k cặp ngoặc...");
                    // Kích hoạt thu gom rác để giải phóng bộ nhớ, tránh sai số do tài nguyên tồn dư trước khi đo.
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Stopwatch boDoThoiGian9 = Stopwatch.StartNew();
                    string chuoiCa9 = new string('(', 500000) + new string(')', 500000);
                    string ketQuaCa9 = KiemTraNgoacCanBang(chuoiCa9);
                    boDoThoiGian9.Stop();
                    Console.WriteLine($"-> Kết quả: {ketQuaCa9}");
                    Console.WriteLine($"-> Thời gian chạy: {boDoThoiGian9.Elapsed.TotalSeconds:F4} giây");
                }
                else if (luaChon == "4")
                {
                    Console.WriteLine("\nĐang chạy Ca 10: 1 triệu ngoặc mở...");
                    // Dọn rác bộ nhớ trước khi đo ca lớn để kết quả chính xác nhất.
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Stopwatch boDoThoiGian10 = Stopwatch.StartNew();
                    string chuoiCa10 = new string('(', 1000000);
                    string ketQuaCa10 = KiemTraNgoacCanBang(chuoiCa10);
                    boDoThoiGian10.Stop();
                    Console.WriteLine($"-> Kết quả: {ketQuaCa10}");
                    Console.WriteLine($"-> Thời gian chạy: {boDoThoiGian10.Elapsed.TotalSeconds:F4} giây");
                }
                else if (luaChon == "5")
                {
                    Console.WriteLine("Đã thoát chương trình!");
                    // Phá vỡ vòng lặp vô hạn `while(true)`.
                    break;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng chọn lại từ 1 đến 5.");
                }
            }
        }
        static string KiemTraNgoacCanBang(string chuoiVao)
        {
            // Mỗi ngoặc mở được lưu cùng vị trí để báo đúng chỗ lỗi nếu nó không được đóng.
            Stack<(char kyTu, int viTri)> nganXep = new Stack<(char kyTu, int viTri)>();
            for (int i = 0; i < chuoiVao.Length; i++)
            {
                char c = chuoiVao[i];
                // Đẩy ngoặc mở và vị trí của nó vào Stack để chờ đóng.
                if (c == '(' || c == '[' || c == '{')
                {
                    nganXep.Push((c, i));
                }
                // Gặp ngoặc đóng, tiến hành đối chiếu với ngoặc mở trên đỉnh.
                else if (c == ')' || c == ']' || c == '}')
                {
                    // Gặp ngoặc đóng khi Stack rỗng: không có ngoặc mở nào để ghép, lỗi tại vị trí i.
                    if (nganXep.Count == 0)
                    {
                        return $"SAI tai vi tri {i}";
                    }
                    // Xem trước ngoặc mở trên đỉnh để kiểm tra loại.
                    var dinh = nganXep.Peek();
                    // Nếu cùng loại, lấy phần tử đó ra (Pop) vì đã ghép cặp thành công.
                    if ((c == ')' && dinh.kyTu == '(') ||
                        (c == ']' && dinh.kyTu == '[') ||
                        (c == '}' && dinh.kyTu == '{'))
                    {
                        nganXep.Pop();
                    }
                    // Đóng sai loại so với đỉnh Stack, trả về vị trí lỗi.
                    else
                    {
                        return $"SAI tai vi tri {i}";
                    }
                }
            }
            // Kết thúc duyệt chuỗi mà Stack còn phần tử nghĩa là có ngoặc mở chưa bao giờ được đóng.
            if (nganXep.Count > 0)
            {
                int viTriSomNhat = -1;
                // Pop liên tục đến khi Stack rỗng. Phần tử lấy ra cuối cùng chính là ngoặc được đưa vào sớm nhất.
                while (nganXep.Count > 0)
                {
                    viTriSomNhat = nganXep.Pop().viTri;
                }
                return $"SAI tai vi tri {viTriSomNhat}";
            }
            // Xử lý hết chuỗi và Stack rỗng, ngoặc cân bằng hoàn toàn.
            return "HOP LE";
        }
    }
}