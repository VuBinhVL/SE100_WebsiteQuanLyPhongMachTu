import React from "react";
import Anh from "../../../assets/images/clinic1.png";
import pencil from "../../../assets/icons/pencil.png";
import "./Account.css";
import InputField from "../../../components/Customer/Account/TextBox"; // Component bạn đã tạo
import Button from "../../../components/Customer/Account/Button"; // Component bạn đã tạo

export default function Account() {
  return (
    <div className="account-page">
      {/* Phần khoảng cách */}
      <div className="account-description-section"></div>
      <h2 className="account-title" style={{ color: "#fff" }}>
        THÔNG TIN TÀI KHOẢN
      </h2>

      <div className="account-section">
        {/* Phần bên trái: Avatar */}
        <div className="account-left">
          <button className="edit-info-button">
            <img src={pencil} alt="Edit Icon" className="edit-icon" />
            <span>Chỉnh sửa thông tin</span>
          </button>
          <div className="avatar-wrapper">
            <img src={Anh} alt="Avatar" className="account-avatar" />
            <button className="change-photo">Chọn ảnh</button>
          </div>
        </div>

        {/* Phần bên phải: Thông tin cá nhân */}
        <div className="account-right">
          <div className="account-form">
            {/* Nút đổi mật khẩu */}
            <div className="change-password-wrapper">
              <button
                className="change-password-button"
                onClick={() => console.log("Đổi mật khẩu")}
              >
                Đổi mật khẩu
              </button>
            </div>
            <div className="form-row">
              <InputField
                label="Họ và tên"
                type="text"
                placeholder="Nhập họ và tên"
              />
              <div
                className="form-group"
                style={{ width: "50%", paddingLeft: "12px" }}
              >
                <label className="input-label">Giới tính</label>
                <select className="input-box">
                  <option value="Nam">Nam</option>
                  <option value="Nữ">Nữ</option>
                </select>
              </div>
            </div>

            <div className="form-row">
              <InputField
                label="Số điện thoại"
                type="text"
                placeholder="Nhập số điện thoại"
              />
              <InputField label="Ngày sinh" type="date" />
            </div>

            <div className="form-row">
              <InputField label="Email" type="email" placeholder="Nhập email" />
              <InputField
                label="Địa chỉ"
                type="text"
                placeholder="Nhập địa chỉ"
              />
            </div>

            <div className="form-actions">
              <Button
                label="Hủy"
                color="#dc3545"
                onClick={() => console.log("Hủy")}
              />
              <Button
                label="Xác nhận"
                color="#348f6c"
                onClick={() => console.log("Xác nhận")}
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
