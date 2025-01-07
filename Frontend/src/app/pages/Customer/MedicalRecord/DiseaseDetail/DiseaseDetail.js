import React, { useEffect, useState } from "react";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox"; // Import hàm hiển thị thông báo lỗi
import { fetchGet } from "../../../../lib/httpHandler"; // Import hàm gọi API
import "./DiseaseDetail.css"; // File CSS riêng cho giao diện

export default function DiseaseDetail({ diseaseId, onClose }) {
  const [information, setInformation] = useState({
    tenBenhNhan: "",
    tenBacSi: "",
    tenBenhLy: "",
    chanDoan: "",
    ngayTaoPKB: "",
    tongTienThuoc: "",
    tongTienXetNghiem: "",
    tongTien: "",
  }); //Thông tin
  const [medicineList, setMedicineList] = useState([]); //Danh sách thuốc
  const [testList, setTestList] = useState([]); //Danh sách test

  //Đinh dạng tiền
  const formatCurrency = (value) => {
    return new Intl.NumberFormat("vi-VN", {
      style: "decimal",
      minimumFractionDigits: 0,
    }).format(value);
  };

  //Định dạng ngày khám
  const formatDate = (dateString) => {
    const date = new Date(dateString);
    const day = date.getDate().toString().padStart(2, "0");
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
  };
  //Gọi API lấy chi tiết bệnh lý
  useEffect(() => {
    const uri = `/api/quan-li-chi-tiet-ho-so-benh-an/chi-tiet-kham-benh?chiTietKhamBenhId=${diseaseId}`;
    fetchGet(
      uri,
      (data) => {
        console.log(data);
        setInformation({
          tenBenhNhan: data.tenBenhNhan || "",
          tenBacSi: data.tenBacSi || "",
          tenBenhLy: data.tenBenhLy || "",
          chanDoan: data.chanDoan || "",
          ngayTaoPKB: data.ngayTaoPKB || "",
          tongTienThuoc: data.tongTienThuoc || "",
          tongTienXetNghiem: data.tongTienXetNghiem || "",
          tongTien: data.tongTien || "",
        });
        setMedicineList(data.danhSachThuoc || []); // Lưu danh sách thuốc
        setTestList(data.danhSachXetNghiem || []); // Lưu danh sách xét nghiệm
      },
      (error) => {
        showErrorMessageBox(error.message);
      },
      () => {
        showErrorMessageBox("Lỗi kết nối đến máy chủ");
      }
    );
  }, [diseaseId]);

  return (
    <>
      <div className="disease-info-overlay"></div>
      <div className="disease-detail-popup">
        <div className="popup-header">
          <h2 className="popup-title">Chi tiết bệnh lý khám</h2>
          <button className="popup-close" onClick={onClose}>
            X
          </button>
        </div>

        <div className="popup-section">
          <div className="detail-row">
            <p>
              <strong>Bệnh nhân:</strong> {information.tenBenhNhan}
            </p>
            <p>
              <strong>Bác sĩ điều trị:</strong> {information.tenBacSi}
            </p>
          </div>
          <div className="detail-row">
            <p>
              <strong>Tên bệnh lý:</strong> {information.tenBenhLy}
            </p>
            <p>
              <strong>Ngày điều trị:</strong>{" "}
              {formatDate(information.ngayTaoPKB)}
            </p>
          </div>
          <div className="detail-row">
            <p>
              <strong>Chẩn đoán:</strong> {information.chanDoan}
            </p>
          </div>
        </div>
        <hr className="divider" />
        <div className="popup-section">
          <h3 className="section-title">CHI TIẾT ĐƠN THUỐC</h3>
          {medicineList.length > 0 ? (
            <table className="detail-table">
              <thead>
                <tr>
                  <th>Tên thuốc</th>
                  <th>Số lượng</th>
                  <th>Đơn giá</th>
                  <th>Thành tiền</th>
                </tr>
              </thead>
              <tbody>
                {medicineList.map((medicine, index) => (
                  <tr key={index}>
                    <td>{medicine.tenThuoc}</td>
                    <td>{medicine.soLuongThuoc}</td>
                    <td>{formatCurrency(medicine.donGiaThuoc)}</td>
                    <td>{formatCurrency(medicine.thanhTien)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          ) : (
            <p>Không có thông tin thuốc</p>
          )}
          <p className="total">
            Tổng tiền đơn thuốc: {formatCurrency(information.tongTienThuoc)} đ
          </p>
        </div>
        <hr className="divider" />
        <div className="popup-section">
          <h3 className="section-title">KẾT QUẢ XÉT NGHIỆM</h3>
          {testList.length > 0 ? (
            <table className="detail-table">
              <thead>
                <tr>
                  <th>Tên xét nghiệm</th>
                  <th>Đơn vị tính</th>
                  <th>Kết quả</th>
                  <th>Đánh giá</th>
                  <th>Giá xét nghiệm</th>
                </tr>
              </thead>
              <tbody>
                {testList.map((test, index) => (
                  <tr key={index}>
                    <td>{test.tenXetNghiem}</td>
                    <td>{test.dvt}</td>
                    <td>{test.ketQua}</td>
                    <td>{test.danhGia}</td>
                    <td>{formatCurrency(test.giaXetNghiem)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          ) : (
            <p>Không có thông tin xét nghiệm</p>
          )}

          <p className="total">
            Tổng tiền xét nghiệm:{" "}
            {formatCurrency(information.tongTienXetNghiem)} đ
          </p>
        </div>

        <div className="popup-footer">
          <p className="grand-total">
            Tổng tiền: {formatCurrency(information.tongTien)} đ
          </p>
        </div>
      </div>
    </>
  );
}
