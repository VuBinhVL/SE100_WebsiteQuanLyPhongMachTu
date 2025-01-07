import React, { useEffect, useState } from "react";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox"; // Import hàm hiển thị thông báo lỗi
import { fetchGet } from "../../../../lib/httpHandler"; // Import hàm gọi API
import "./MedicalImaging.css";

export default function MedicalImaging({ diseaseId, onClose }) {
  const [information, setInformation] = useState({
    tenBenhLy: "",
    image: "",
    ketLuan: "",
    gia: "",
  }); //Thông tin

  //Đinh dạng tiền
  const formatCurrency = (value) => {
    return new Intl.NumberFormat("vi-VN", {
      style: "decimal",
      minimumFractionDigits: 0,
    }).format(value);
  };

  //Gọi API lấy thông tin
  useEffect(() => {
    const uri = `/api/quan-li-chi-tiet-ho-so-benh-an/chup-chieu?chiTietKhamBenhId=${diseaseId}`;
    fetchGet(
      uri,
      (data) => {
        setInformation({
          tenBenhLy: data.tenBenhLy || "",
          image: data.image || "", // Gán mảng ảnh
          ketLuan: data.ketLuan || "",
          gia: data.gia || "",
        });
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
      <div className="medical-imaging-overlay"></div>
      <div className="medical-imaging-popup">
        <div className="popup-header">
          <h3 className="popup-title">{information.tenBenhLy}</h3>
          <button className="popup-close" onClick={onClose}>
            X
          </button>
        </div>
        <div className="popup-body">
          <h4 className="popup-section-title">Ảnh chụp chiếu:</h4>
          <div className="medical-imaging-grid">
            <img
              src={information.image} // URL ảnh từ API
              alt="Ảnh chụp chiếu"
              className="medical-imaging-item"
            />
          </div>
          <hr className="divider" />

          <h4 className="popup-section-title">Kết luận:</h4>
          <textarea
            className="medical-imaging-conclusion"
            readOnly
            value={information.ketLuan}
          />
          <p className="medical-imaging-price">
            Đơn giá: {formatCurrency(information.gia)} đ
          </p>
        </div>
      </div>
    </>
  );
}
