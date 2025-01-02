import React, { useState } from "react";
import Button from "../../../../components/Customer/Account/AccountButton"; // Component bạn đã tạo
import "./ChangePassword.css";
import { fetchGet } from "../../../../lib/httpHandler";

export default function ChangePassword({ onClose }) {
  const [currentPassword, setCurrentPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const handleSave = () => {
    if (newPassword !== confirmPassword) {
      setErrorMessage("Mật khẩu mới và xác nhận mật khẩu không khớp!");
      return;
    }
    const uri = "/api/quan-li-ho-so-benh-an/hien-thi-ho-so-benh-an";
    fetchGet(
      uri,
      (sus) => {
        console.log(sus);
      },
      (fail) => {
        console.log(fail);
      },
      () => {
        console.log("Có lỗi xảy ra");
      }
    );

    // Logic xử lý lưu mật khẩu
    alert("Đổi mật khẩu thành công!");
    onClose(); // Đóng popup sau khi lưu
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
