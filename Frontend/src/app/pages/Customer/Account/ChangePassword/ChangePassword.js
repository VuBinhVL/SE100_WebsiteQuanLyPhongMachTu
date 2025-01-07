import React, { useState } from "react";
import Button from "../../../../components/Customer/Account/AccountButton"; // Component bạn đã tạo
import { showSuccessMessageBox } from "../../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox";
import "./ChangePassword.css";
import { fetchPost } from "../../../../lib/httpHandler";

export default function ChangePassword({ onClose }) {
  const [currentPassword, setCurrentPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const handleSave = () => {
    if (!newPassword || !currentPassword || !confirmPassword) {
      setErrorMessage("Vui lòng nhập đầy đủ thông tin!");
      return;
    } else if (newPassword !== confirmPassword) {
      setErrorMessage("Mật khẩu mới không khớp!");
      return;
    }
    const payload = {
      matKhauHienTai: currentPassword,
      matKhauMoi: newPassword,
      matKhauNhapLai: confirmPassword,
    };
    const uri = "/api/change-password";
    fetchPost(
      uri,
      payload,
      (sus) => {
        showSuccessMessageBox(sus.message);
        onClose();
      },
      (error) => {
        setErrorMessage(error.message);
      },
      () => {
        setErrorMessage("Không thể kết nối đến máy chủ");
      }
    );
  };

  return (
    <div className="change-password">
      <div className="container">
        <h3 className="title">Đổi mật khẩu</h3>
        <div className="body">
          <div className="form-group">
            <label className="form-label">Mật khẩu hiện tại</label>
            <input
              type="password"
              value={currentPassword}
              onChange={(e) => setCurrentPassword(e.target.value)}
              className="popup-input"
              placeholder="Nhập mật khẩu hiện tại"
            />
          </div>
          <div className="form-group">
            <label className="form-label">Mật khẩu mới</label>
            <input
              type="password"
              value={newPassword}
              onChange={(e) => setNewPassword(e.target.value)}
              className="popup-input"
              placeholder="Nhập mật khẩu mới"
            />
          </div>
          <div className="form-group">
            <label className="form-label">Xác nhận mật khẩu mới</label>
            <input
              type="password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              className="popup-input"
              placeholder="Xác nhận mật khẩu mới"
            />
            {errorMessage && <p className="error-message">{errorMessage}</p>}
          </div>
        </div>
        <div className="actions">
          <Button label="Hủy" color="#dc3545" onClick={onClose}>
            Hủy
          </Button>
          <Button label="Lưu" color="#348f6c" onClick={handleSave}>
            Lưu
          </Button>
        </div>
      </div>
    </div>
  );
}
