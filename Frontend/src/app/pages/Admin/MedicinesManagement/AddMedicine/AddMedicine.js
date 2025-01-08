import React from "react";
import "./AddMedicine.css";

export default function AddMedicine({ onClose }) {
  return (
    <div className="modal-overlay-add-medicine">
      <div className="modal-container">
        <div className="modal-header">
          <h5 className="modal-title">Thêm thuốc</h5>
          <button className="modal-close" onClick={onClose}>
            &times;
          </button>
        </div>
        <div className="modal-body">
          <form className="form-container">
            <div className="form-group">
              <label htmlFor="tenThuoc">Tên thuốc:</label>
              <input
                name="tenThuoc"
                id="tenThuoc"
                type="text"
                placeholder="Nhập tên thuốc"
                //  onChange={handleChange}
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="tenThuoc">Số lượng:</label>
              <input
                name="tenThuoc"
                id="tenThuoc"
                type="text"
                placeholder="Nhập số lượng"
                //  onChange={handleChange}
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="giaNhap">Giá nhập:</label>
              <input
                name="giaNhap"
                id="giaNhap"
                type="text"
                placeholder="Nhập giá nhập"
                //  onChange={handleChange}
              />
            </div>
            <div className="form-group">
              <label htmlFor="loaiThuocId">Loại thuốc:</label>
              <select
                id="loaiThuocId"
                name="loaiThuocId"
                defaultValue={"DEFAULT"}
                // onChange={handleChange}
              >
                <option value="DEFAULT" hidden disabled>
                  Chọn loại thuốc
                </option>
                <option value="male">Male</option>
                <option value="female">Female</option>
              </select>
            </div>
            <div className="form-group">
              <label htmlFor="ngaySinh">Ngày sản xuất:</label>
              <input
                type="date"
                id="ngaySinh"
                name="ngaySinh"
                //  onChange={handleChange}
              />
            </div>
            <div className="form-group">
              <label htmlFor="ngaySinh">Ngày hết hạn:</label>
              <input
                type="date"
                id="ngaySinh"
                name="ngaySinh"
                // onChange={handleChange}
              />
            </div>
          </form>
          <div className="image-container">
            <img className="inner-image" />
          </div>
        </div>
        <div className="modal-footer">
          <button className="btn-cancel" onClick={onClose}>
            Hủy
          </button>
          <button className="btn-accept">Thêm</button>
        </div>
      </div>
    </div>
  );
}
