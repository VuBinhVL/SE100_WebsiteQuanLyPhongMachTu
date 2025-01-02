import React, { useState } from "react";
import "./MedicalImaging.css";

export default function MedicalImaging() {
  const [isVisible, setIsVisible] = useState(true); // Quản lý trạng thái hiển thị popup

  const handleClose = () => {
    setIsVisible(false); // Ẩn popup khi nhấn nút X
  };

  if (!isVisible) {
    return null; // Không hiển thị popup
  }

  return (
    <div className="medical-imaging-popup">
      <div className="popup-header">
        <h3 className="popup-title">Cao huyết áp</h3>
        <button className="popup-close" onClick={handleClose}>
          X
        </button>
      </div>
      <div className="popup-body">
        <h4 className="popup-section-title">Ảnh chụp chiếu</h4>
        <div className="medical-imaging-grid">
          <img
            src="https://via.placeholder.com/150"
            alt="Ảnh chụp chiếu 1"
            className="medical-imaging-item"
          />
          <img
            src="https://via.placeholder.com/150"
            alt="Ảnh chụp chiếu 2"
            className="medical-imaging-item"
          />
        </div>
        <h4 className="popup-section-title">Kết luận:</h4>
        <textarea
          className="medical-imaging-conclusion"
          readOnly
          value="Lượng đường trong máu cao nên giảm uống nước ngọt"
        />
        <p className="medical-imaging-price">Đơn giá: 30.000đ</p>
      </div>
    </div>
  );
}
