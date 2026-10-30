•	Họ tên, mã số sinh viên và lớp học phần.
Trần Võ Vân Khanh - 31251022054
Cấu trúc dữ liệu và giải thuật – 26C1INF50900706
•	Hướng dẫn biên dịch và chạy chương trình của từng bài.
sử dụng Visual Studio 2022 , mở file CTDLGT-Buoi03-31251022054.sln, chọn bài muốn chạy, nhấn Ctrl + F5 để chạy chương trình.
•	Mô tả ngắn giải pháp, độ phức tạp thời gian và bộ nhớ của từng bài.
B1: kiểm tra dấu ngoặc cân bằng:
+ Giải pháp: sử dụng stack lưu trữ các dấu ngoặc mở, khi gặp dấu ngoặc đóng thì kiểm tra với dấu ngoặc mở trên đỉnh stack. Xử lí ngoại lệ như đóng thừa, mở chưa đóng và sai loại
+ Độ phức tạp thời gian: bộ nhớ O(n), thời gian O(n)
B2: mô phỏng quầy dịch vụ:
+ Giải pháp: dùng queue đảo bảo nguyên tắc FIFO. Quản lý trạng thái các quầy, thời điểm phục vụ, mỗi khách hàng khi dequeue sẽ quét tìm quầy rảnh sớm nhất.
+ Độ phức tạp: bộ nhớ O(n) cho hàng đợi và O(k) cho lưu quầy, thời gian O(n*k) với n là số khách hàng, k là số quầy.
B3: hàng đợi vòng:
+ Giải pháp: cài đặt mảng bằng biến front và count. Khi mảng đầy, tạo mảng mới gấp đôi là chép dữ liệu từ front để không đứt gãy.
+ Độ phức tạp: bộ nhớ O(n), thời gian O(1) cho hàm dequeue và enqueue.
•	Các ca kiểm thử đã dùng và thời gian chạy ở các ca lớn.
Ca kiểm thử đã dùng theo file bài tập được cung cấp, thời gian chạy được ghi nhận vào word
•	Công cụ AI đã sử dụng, nếu có.
Đã dùng công cụ AI (Gemini, Claude) để hỗ trợ rà soát luồng xử lý của stack/ queue, kiểm thử vòng lặp. Phần được hỗ trợ gồm: giải thích tính khấu hao của hàng đợi vòng, rà soát lỗi tràn số, cơ chế gom rác (GC.Collect). Tôi đã chạy ca kiểm thử trên máy cá nhân và chịu trách nhiệm về bài nộp.