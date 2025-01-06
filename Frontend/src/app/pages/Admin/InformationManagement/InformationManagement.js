import { React, useEffect, useState } from "react";
import pencil from "../../../assets/icons/pencil.png";
import "./InformationManagement.css";
import InputField from "../../../components/Customer/Account/AccountTextBox"; // Component Textbox
import Button from "../../../components/Customer/Account/AccountButton"; // Component Button
import ChangePassword from "../../Customer/Account/ChangePassword/ChangePassword"; //  ChangePassword
import { showErrorMessageBox } from "../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox"; // ErrorMessageBox
import { showSuccessMessageBox } from "../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox"; // ErrorMessageBox
import { fetchGet } from "../../../lib/httpHandler"; // API

export default function InformationManagement() {
  const [isEditing, setIsEditing] = useState(false); // Trạng thái chỉnh sửa
  const [isPopupOpen, setIsPopupOpen] = useState(false); // Trạng thái hiển thị popup
  const [information, setInformation] = useState({
    hoTen: "",
    gioiTinh: "",
    soDienThoai: "",
    ngaySinh: "",
    email: "",
    diaChi: "",
    vaiTro: "",
    chuyenMon: "",
    image: "",
  }); //Thông tin tài khoản
  const [avatar, setAvatar] = useState(""); // Lưu ảnh đại diện

  //Format ngày sinh
  const formatDateToDisplay = (dateString) => {
    if (!dateString) return "";
    const date = new Date(dateString);
    const day = date.getDate().toString().padStart(2, "0");
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const year = date.getFullYear();
    return `${day}/${month}/${year}`; // Trả về định dạng dd/mm/yyyy
  };

  const formatDateToISO = (dateString) => {
    if (!dateString) return "";
    const [day, month, year] = dateString.split("/");
    return `${year}-${month}-${day}`; // Trả về định dạng YYYY-MM-DD
  };

  //Gọi API lấy thông tin tài khoản
  useEffect(() => {
    const uri = "/api/admin/quan-li-nhan-vien/thong-tin-ca-nhan";
    fetchGet(
      uri,
      (data) => {
        console.log(data);
        setAvatar(data.avatar || ""); // Gán ảnh đại diện nếu có
        setInformation({
          hoTen: data.hoTen || "",
          gioiTinh: data.gioiTinh || "",
          soDienThoai: data.soDienThoai || "",
          ngaySinh: data.ngaySinh || "",
          email: data.email || "",
          diaChi: data.diaChi || "",
          vaiTro: data.vaiTro || "",
          chuyenMon: data.chuyenMon || "",
          image: data.image || "",
        });
      },
      (error) => {
        showErrorMessageBox(error.message);
      },
      () => {
        showErrorMessageBox("Không thể kết nối đến server");
      }
    );
  }, []);

  //Nút chỉnh sửa thông tin
  const handleEdit = () => {
    setIsEditing(true); // Kích hoạt chế độ chỉnh sửa
  };

  //Nút hủy chỉnh sửa
  const handleCancel = () => {
    setIsEditing(false); // Quay lại trạng thái không chỉnh sửa
  };

  //Nút lưu thông tin
  const handleSave = () => {
    console.log("Thông tin đã được lưu!");
    setIsEditing(false); // Quay lại trạng thái không chỉnh sửa
  };

  //Mở popup đổi mật khẩu
  const handleOpenPopup = () => {
    setIsPopupOpen(true); // Mở popup
  };

  //Đóng popup đổi mật khẩu
  const handleClosePopup = () => {
    setIsPopupOpen(false); // Đóng popup
  };

  return (
    <div className="information-page">
      <div className="information-section">
        {/* Phần bên trái: Avatar */}
        <div className="information-left">
          <button className="edit-info-button" onClick={handleEdit}>
            <img src={pencil} alt="Edit Icon" className="edit-icon" />
            <span>Chỉnh sửa thông tin</span>
          </button>
          <div className="information-wrapper">
            <img
              src={information.image}
              alt="Avatar"
              className="information-avatar"
            />
            {isEditing && <button className="change-photo">Chọn ảnh</button>}
            <div class="info-container">
              <p class="info-item">
                <b>Vai trò:</b> {information.vaiTro}
              </p>
              <p class="info-item">
                <b>Chuyên môn:</b> {information.chuyenMon}
              </p>
            </div>
          </div>
        </div>

        {/* Phần bên phải: Thông tin cá nhân */}
        <div className="information-right">
          <div className="information-form">
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
                value={information.hoTen}
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
              />
              <div className="form-group">
                <label className="input-label">Giới tính</label>
                <select
                  className="input-box"
                  value={information.gioiTinh}
                  disabled={!isEditing}
                >
                  <option value="Nam">Nam</option>
                  <option value="Nữ">Nữ</option>
                </select>
              </div>
            </div>

            <div className="form-row">
              <InputField
                label="Số điện thoại"
                type="text"
                value={information.soDienThoai}
                placeholder="Nhập số điện thoại"
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
              />
              <InputField
                label="Ngày sinh"
                type="text"
                value={
                  information.ngaySinh
                    ? formatDateToDisplay(information.ngaySinh)
                    : ""
                } // Hiển thị dd/mm/yyyy
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
              />
            </div>

            <div className="form-row">
              <InputField
                label="Email"
                type="email"
                value={information.email}
                placeholder="Nhập email"
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
              />
              <InputField
                label="Địa chỉ"
                value={information.diaChi}
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
