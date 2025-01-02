import React, { useState } from "react";
import Anh from "../../../../assets/images/clinic1.png";
import pencil from "../../../../assets/icons/pencil.png";
import "./AccountInformation.css";
import InputField from "../../../../components/Customer/Account/AccountTextBox"; // Component Textbox
import Button from "../../../../components/Customer/Account/AccountButton"; // Component Button
import ChangePassword from "../ChangePassword/ChangePassword"; //  ChangePassword

export default function Account() {
  const [isEditing, setIsEditing] = useState(false); // Trạng thái chỉnh sửa
  const [isPopupOpen, setIsPopupOpen] = useState(false); // Trạng thái hiển thị popup

  const handleEdit = () => {
    setIsEditing(true); // Kích hoạt chế độ chỉnh sửa
  };

  const handleCancel = () => {
    setIsEditing(false); // Quay lại trạng thái không chỉnh sửa
  };

  const handleSave = () => {
    console.log("Thông tin đã được lưu!");
    setIsEditing(false); // Quay lại trạng thái không chỉnh sửa
  };

  const handleOpenPopup = () => {
    setIsPopupOpen(true); // Mở popup
  };

  const handleClosePopup = () => {
    setIsPopupOpen(false); // Đóng popup
  };

  return (
    <div className="account-page">
      {/* Phần khoảng cách */}
      <div className="account-record-header">
        <h2 className="account-record-title">THÔNG TIN TÀI KHOẢN</h2>
      </div>

      <div className="account-section">
        {/* Phần bên trái: Avatar */}
        <div className="account-left">
          <button className="edit-info-button" onClick={handleEdit}>
            <img src={pencil} alt="Edit Icon" className="edit-icon" />
            <span>Chỉnh sửa thông tin</span>
          </button>
          <div className="avatar-wrapper">
            <img
              src="https://vwu.vn/documents/20182/3458479/28_Feb_2022_115842_GMTbsi_thuhien.jpg/c04e15ea-fbe4-415f-bacc-4e5d4cc0204d"
              alt="Avatar"
              className="account-avatar"
            />
            {isEditing && <button className="change-photo">Chọn ảnh</button>}
          </div>
        </div>

        {/* Phần bên phải: Thông tin cá nhân */}
        <div className="account-right">
          <div className="account-form">
            {/* Nút đổi mật khẩu */}
            <div className="change-password-wrapper">
              {!isEditing && (
                <button
                  className="change-password-button"
                  onClick={handleOpenPopup} // Hiển thị popup khi nhấn nút
                >
                  Đổi mật khẩu
                </button>
              )}
            </div>

            <div className="form-row">
              <InputField
                label="Họ và tên"
                type="text"
                placeholder="Nhập họ và tên"
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
              />
              <div className="form-group">
                <label className="input-label">Giới tính</label>
                <select className="input-box" disabled={!isEditing}>
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
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
              />
              <InputField
                label="Ngày sinh"
                type="date"
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
              />
            </div>

            <div className="form-row">
              <InputField
                label="Email"
                type="email"
                placeholder="Nhập email"
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
              />
              <InputField
                label="Địa chỉ"
                type="text"
                placeholder="Nhập địa chỉ"
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
              />
            </div>

            {isEditing && (
              <div className="form-actions">
                <Button label="Hủy" color="#dc3545" onClick={handleCancel} />
                <Button label="Lưu" color="#348f6c" onClick={handleSave} />
              </div>
            )}
          </div>
        </div>
      </div>
      {/* Popup Change Password */}
      {isPopupOpen && <ChangePassword onClose={handleClosePopup} />}
    </div>
  );
}
