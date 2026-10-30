## Bài 1: Kiểm tra dấu ngoặc cân bằng (Stack)
| STT | Dữ liệu vào | KQ mong đợi | KQ thực tế | Đạt? | Nhận xét |
|:---|:---|:---|:---|:---:|:---|
| 1 | {(a+b)×[c−d]} | HOP LE | HOP LE | Đạt | Ca thông thường |
| 2 | {(a+b]×c) | SAI tai vi tri 5 | SAI tai vi tri 5 | Đạt | Đóng sai loại |
| 3 | (a+b | SAI tai vi tri 0 | SAI tai vi tri 0 | Đạt | Mở chưa đóng |
| 4 | a+b) | SAI tai vi tri 3 | SAI tai vi tri 3 | Đạt | Đóng thừa |
| 5 | (chuỗi rỗng) | HOP LE | HOP LE | Đạt | Ca biên |
| 6 | ([)] | SAI tai vi tri 2 | SAI tai vi tri 2 | Đạt | Cặp đan xen |
| 7 | a+b | HOP LE | HOP LE | Đạt | Không có ngoặc |
| 8 | {[( | SAI tai vi tri 0 | SAI tai vi tri 0 | Đạt | Nhiều mở chưa đóng |

## Bài 2: Mô phỏng quầy dịch vụ (Queue)
| STT | Dữ liệu vào | KQ mong đợi | KQ thực tế | Đạt? | Nhận xét |
|:---|:---|:---|:---|:---:|:---|
| 1 | k=1; (0,5), (1,3), (2,4) | TB 3.33; Max 6; Xong 12 | TB 3.33; Max 6; Xong 12 | Đạt | Một quầy |
| 2 | k=2; (0,5), (1,3), (2,4) | TB 0.67; Max 2; Xong 8 | TB 0.67; Max 2; Xong 8 | Đạt | Hai quầy |
| 3 | k=2; (0,3), (0,3), (0,3) | TB 1.00; Max 3; Xong 6 | TB 1.00; Max 3; Xong 6 | Đạt | Đến cùng lúc |
| 4 | k=1; (0,2), (10,3) | TB 0.00; Max 0; Xong 13 | TB 0.00; Max 0; Xong 13 | Đạt | Quầy rảnh mới có khách |
| 5 | k=3; (4,6) | TB 0.00; Max 0; Xong 10 | TB 0.00; Max 0; Xong 10 | Đạt | n=1, k > n |

## Bài 3: Tự cài đặt Hàng đợi vòng (Circular Queue)
| STT | Dữ liệu vào | KQ mong đợi | KQ thực tế | Đạt? | Nhận xét |
|:---|:---|:---|:---|:---:|:---|
| 1 | Tạo mới; Dequeue; Peek | Lỗi InvalidOperationException | Lỗi InvalidOperationException | Đạt | Hàng rỗng |
| 2 | Enq A,B,C,D; Peek | Count 4, Cap 4; Peek A | Count 4, Cap 4; Peek A | Đạt | Vừa đầy |
| 3 | Enq A,B,C,D; Deq 2; Enq E,F; Deq 4 | A,B rồi C,D,E,F; Cap 4 | A,B rồi C,D,E,F; Cap 4 | Đạt | Quay vòng, chưa mở rộng |
| 4 | Enq 1,2,3,4,5; Deq 5 | Cap 8; lấy 1,2,3,4,5 | Cap 8; lấy 1,2,3,4,5 | Đạt | Mở rộng khi front = 0 |
| 5 | Enq A,B,C,D; Deq 2; Enq E,F,G; Deq 5 | Cap 8; lấy C,D,E,F,G | Cap 8; lấy C,D,E,F,G | Đạt | Mở rộng khi front khác 0 |

## Thời gian chạy các ca lớn
* **Bài 1 - Ca 9:** 0.0190 s
* **Bài 1 - Ca 10:** 0.0267 s
* **Bài 2 - Ca 6:** 6 ms
* **Bài 3 - Ca 6:** 22 ms
* **Bài 3 - Ca 7:** 3 ms