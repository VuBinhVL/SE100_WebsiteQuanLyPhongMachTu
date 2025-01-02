import React, { useState } from "react";
import "./DetailRecord.css";
import { useParams } from "react-router-dom";

export default function DetailRecord() {
  const { recordId } = useParams(); // Lấy mã hồ sơ từ URL

  const [records] = useState([
    {
      id: "1",
      doctorName: "Trần Thanh Trúc",
      group: "Tim mạch",
      date: "20/12/2024",
      total: "150.000",
    },
    {
      id: "2",
      doctorName: "Nguyễn Văn A",
      group: "Hô hấp",
      date: "20/12/2024",
      total: "200.000",
    },
  ]);

  return (
    <div className="detail-record-page">
      {/* Header */}
      <div className="detail-record-header">
        <h2 className="detail-record-title">
          HỒ SƠ BỆNH ÁN: <span className="record-id">{recordId}</span>
        </h2>
      </div>

      {/* Bảng chi tiết hồ sơ */}
      <div className="detail-record-table-container">
        <table className="detail-record-table">
          <thead>
            <tr>
              <th>Mã PKB</th>
              <th>Tên bác sĩ điều trị</th>
              <th>Thuộc nhóm bệnh</th>
              <th>Ngày khám</th>
              <th>Tổng tiền</th>
              <th>Thao tác</th>
            </tr>
          </thead>
          <tbody>
            {records.map((record, index) => (
              <tr key={index}>
                <td>{record.id}</td>
                <td>{record.doctorName}</td>
                <td>{record.group}</td>
                <td>{record.date}</td>
                <td>{record.total} đ</td>
                <td>
                  <button className="action-button">
                    <span role="img" aria-label="view">
                      👁️
                    </span>
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Phân trang */}
      <div className="pagination">
        <button className="pagination-button">&lt;</button>
        <button className="pagination-button active">1</button>
        <button className="pagination-button">2</button>
        <button className="pagination-button">3</button>
        <span className="pagination-dots">...</span>
        <button className="pagination-button">10</button>
        <button className="pagination-button">&gt;</button>
      </div>
    </div>
  );
}
