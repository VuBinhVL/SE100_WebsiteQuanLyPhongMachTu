import React from "react";
import "./DiseaseDetail.css"; // File CSS riêng cho giao diện

export default function DiseaseDetail() {
  return (
    <div className="disease-detail-popup">
      <div className="popup-header">
        <h2 className="popup-title">CHI TIẾT BỆNH LÍ KHÁM</h2>
        <button className="popup-close">X</button>
      </div>

      <div className="popup-section">
        <div className="detail-row">
          <p>
            <strong>Bệnh nhân:</strong> Mai Thanh Xuân
          </p>
          <p>
            <strong>Bác sĩ điều trị:</strong> Trần Thanh Trúc
          </p>
        </div>
        <div className="detail-row">
          <p>
            <strong>Tên bệnh lý:</strong> Cao huyết áp
          </p>
          <p>
            <strong>Ngày điều trị:</strong> 20/12/2024
          </p>
        </div>
        <div className="detail-row">
          <p>
            <strong>Chẩn đoán:</strong> Huyết áp bất thường
          </p>
        </div>
      </div>

      <div className="popup-section">
        <h3 className="section-title">CHI TIẾT ĐƠN THUỐC</h3>
        <table className="detail-table">
          <thead>
            <tr>
              <th>STT</th>
              <th>Tên thuốc</th>
              <th>Số lượng</th>
              <th>Đơn giá</th>
              <th>Thành tiền</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>1</td>
              <td>Paracetamol</td>
              <td>20 viên</td>
              <td>2.000 đ</td>
              <td>40.000 đ</td>
            </tr>
            <tr>
              <td>1</td>
              <td>Paracetamol</td>
              <td>20 viên</td>
              <td>2.000 đ</td>
              <td>40.000 đ</td>
            </tr>
          </tbody>
        </table>
        <p className="total">Tổng tiền đơn thuốc: 120.000 đ</p>
      </div>

      <div className="popup-section">
        <h3 className="section-title">KẾT QUẢ XÉT NGHIỆM</h3>
        <table className="detail-table">
          <thead>
            <tr>
              <th>STT</th>
              <th>Loại xét nghiệm</th>
              <th>Kết quả</th>
              <th>Đơn vị tính</th>
              <th>Đánh giá</th>
              <th>Đơn giá</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>1</td>
              <td>Máu</td>
              <td>10</td>
              <td>g/dl</td>
              <td>Vượt mức cho phép</td>
              <td>40.000 đ</td>
            </tr>
          </tbody>
        </table>
        <p className="total">Tổng tiền xét nghiệm: 40.000 đ</p>
      </div>

      <div className="popup-footer">
        <p className="grand-total">Tổng tiền: 160.000 đ</p>
      </div>
    </div>
  );
}
