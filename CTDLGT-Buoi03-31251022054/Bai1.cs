using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CTDLGT_Buoi03_31251022054
{
    internal class Bai1
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Ca 1 đến 8
            string[] testCases = {
                "{(a+b)×[c−d]}",  // Ca 1: Thông thường
                "{(a+b]×c)",      // Ca 2: Đóng sai loại
                "(a+b",           // Ca 3: Mở chưa đóng
                "a+b)",           // Ca 4: Đóng thừa
                "",               // Ca 5: Chuỗi rỗng
                "([)]",           // Ca 6: Các cặp đan xen nhau
                "a+b",            // Ca 7: Không có ngoặc
                "{[("             // Ca 8: Nhiều ngoặc mở chưa đóng
            };
            for (int i = 0; i < testCases.Length; i++)
            {
                string kq = KiemTraNgoac(testCases[i]);
                Console.WriteLine($"Ca {i + 1}: {kq,-15} | Chuỗi: {(testCases[i] == "" ? "(chuỗi rỗng)" : testCases[i])}");
            }
            // Ca 9: 500 000 dấu ( rồi 500 000 dấu )
            string ca9 = new string('(', 500000) + new string(')', 500000);
            var sw9 = Stopwatch.StartNew();
            string kq9 = KiemTraNgoac(ca9);
            sw9.Stop();
            Console.WriteLine($"Ca 9: {kq9,-15} | Thời gian: {sw9.ElapsedMilliseconds} ms (Chuỗi 500k mở, 500k đóng)");
            // Ca 10: 1 000 000 dấu (
            string ca10 = new string('(', 1000000);
            var sw10 = Stopwatch.StartNew();
            string kq10 = KiemTraNgoac(ca10);
            sw10.Stop();
            Console.WriteLine($"Ca 10: {kq10,-14} | Thời gian: {sw10.ElapsedMilliseconds} ms (Chuỗi 1 triệu mở)");
            Console.ReadLine();
        }
        //HÀM TỔNG QUÁT XỬ LÝ MỌI CHUỖI
        static string KiemTraNgoac(string s)
        {
            // Mỗi ngoặc mở được lưu cùng vị trí để báo đúng chỗ lỗi nếu nó không được đóng.
            var st = new Stack<(char ch, int pos)>();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '(' || c == '[' || c == '{')
                {
                    st.Push((c, i));
                }
                else if (c == ')' || c == ']' || c == '}')
                {
                    // Gặp ngoặc đóng khi Stack rỗng: không có ngoặc mở nào để ghép, lỗi tại vị trí i.
                    if (st.Count == 0) return $"SAI tai vi tri {i}";

                    // Đặt ô vừa lấy về để kiểm tra loại ngoặc
                    var top = st.Pop();

                    if ((c == ')' && top.ch != '(') ||
                        (c == ']' && top.ch != '[') ||
                        (c == '}' && top.ch != '{'))
                    {
                        return $"SAI tai vi tri {i}";
                    }
                }
            }
            if (st.Count > 0)
            {
                int minPos = s.Length;
                foreach (var item in st)
                {
                    if (item.pos < minPos) minPos = item.pos;
                }
                return $"SAI tai vi tri {minPos}";
            }
            return "HOP LE";
        }
    }
}
