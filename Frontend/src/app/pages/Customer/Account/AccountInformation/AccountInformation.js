import { React, useEffect, useState } from "react";
import pencil from "../../../../assets/icons/pencil.png";
import Button from "../../../../components/Customer/Account/AccountButton"; // Component Button
import InputField from "../../../../components/Customer/Account/AccountTextBox"; // Component Textbox
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox"; // ErrorMessageBox
import { showSuccessMessageBox } from "../../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox"; // ErrorMessageBox
import {
  BE_ENPOINT,
  fetchGet,
  fetchPut,
  fetchUpload,
} from "../../../../lib/httpHandler"; // API
import ChangePassword from "../ChangePassword/ChangePassword"; //  ChangePassword
import "./AccountInformation.css";

export default function AccountInformation() {
  const [initialAvatar, setInitialAvatar] = useState(""); // Lưu giá trị avatar ban đầu từ API
  const [isEditing, setIsEditing] = useState(false); // Trạng thái chỉnh sửa
  const [isPopupOpen, setIsPopupOpen] = useState(false); // Trạng thái hiển thị popup
  const [information, setInformation] = useState({
    hoTen: "",
    gioiTinh: "",
    soDienThoai: "",
    ngaySinh: "",
    email: "",
    diaChi: "",
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
    const uri = "/api/quan-li-thong-tin-ca-nhan";
    fetchGet(
      uri,
      (data) => {
        if (data.image === "no_img.png") {
          data.image =
            "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSqjT_C1CiFUhnOHmAO4XmgiCvnn36NGG2YLw&s";
        }
        setInitialAvatar(data.image || null); // Lưu giá trị avatar ban đầu
        setAvatar(data.image || null); // Gán ảnh đại diện nếu có

        setInformation({
          hoTen: data.tenNguoiDung || "",
          gioiTinh: data.gioiTinh || "",
          soDienThoai: data.soDienThoai || "",
          ngaySinh: data.ngaySinh || "",
          email: data.email || "",
          diaChi: data.diaChi || "",
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

    //Chuẩn bị data
    const dataToSend = {
      hoTen: information.hoTen,
      gioiTinh: information.gioiTinh,
      soDienThoai: information.soDienThoai,
      ngaySinh: information.ngaySinh, // Chuyển ngày sinh về định dạng YYYY-MM-DD
      email: information.email,
      diaChi: information.diaChi,
      image: avatar !== initialAvatar ? avatar : null, // Nếu avatar không thay đổi, đặt là null
    };

    const uri = "/api/update-info";
    console.log(dataToSend);
    //Gọi API cập nhật thông tin
    fetchPut(
      uri,
      dataToSend,
      (success) => {
        showSuccessMessageBox("Cập nhật thông tin thành công!");
        setIsEditing(false); // Thoát chế độ chỉnh sửa
      },
      (fail) => {
        showErrorMessageBox(fail.message); // Thông báo lỗi từ server
      },
      () => {
        showErrorMessageBox("Không thể kết nối đến server"); // Xử lý lỗi kết nối
      }
    );
  };

  //Mở popup đổi mật khẩu
  const handleOpenPopup = () => {
    setIsPopupOpen(true); // Mở popup
  };

  //Đóng popup đổi mật khẩu
  const handleClosePopup = () => {
    setIsPopupOpen(false); // Đóng popup
  };

  const handleFileChange = (e) => {
    const selectedFile = e.target.files[0];
    if (!selectedFile) return;

    const formData = new FormData();
    formData.append("file", selectedFile);

    // Upload ảnh lên server
    fetchUpload(
      "/api/asset/upload-image", // Endpoint upload
      formData,
      (data) => {
        // showSuccessMessageBox("Ảnh đã được upload thành công!");
        setAvatar(`${BE_ENPOINT}/api/asset/view-image/${data.fileName}`); // Cập nhật ảnh đại diện
      },
      (fail) => {
        showErrorMessageBox(fail.message); // Thông báo lỗi
      },
      (exception) => {
        console.error("Không thể kết nối đến sever"); // Xử lý lỗi sập server
      }
    );
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
            <img src={avatar} alt="Avatar" className="account-avatar" />
            {isEditing && (
              <div>
                <label htmlFor="upload-avatar" className="change-photo">
                  Chọn ảnh
                </label>
                <input
                  type="file"
                  id="upload-avatar"
                  style={{ display: "none" }}
                  onChange={handleFileChange} // Gọi hàm upload ảnh khi chọn tệp
                />
              </div>
            )}
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
                value={information.hoTen}
                onChange={(e) =>
                  setInformation({ ...information, hoTen: e.target.value })
                } // Cập nhật state
                disabled={!isEditing}
              />

              <div className="form-group">
                <label className="input-label">Giới tính</label>
                <select
                  className="input-box"
                  value={information.gioiTinh}
                  disabled={!isEditing}
                  onChange={(e) =>
                    setInformation({ ...information, gioiTinh: e.target.value })
                  }
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
                onChange={(e) =>
                  setInformation({
                    ...information,
                    soDienThoai: e.target.value,
                  })
                } // Cập nhật state
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
              />
              <InputField
                label="Ngày sinh"
                type="text"
                value={
                  information.ngaySinh
                    ? formatDateToDisplay(information.ngaySinh)
                    : ""
                }
                onChange={(e) =>
                  setInformation({
                    ...information,
                    ngaySinh: formatDateToISO(e.target.value), // Cập nhật ngày sinh
                  })
                }
                disabled={!isEditing}
              />
            </div>

            <div className="form-row">
              <InputField
                label="Email"
                type="email"
                value={information.email}
                placeholder="Nhập email"
                disabled={!isEditing} // Không thể sửa nếu không ở chế độ chỉnh sửa
                onChange={(e) =>
                  setInformation({ ...information, email: e.target.value })
                } // Cập nhật state
              />
              <InputField
                label="Địa chỉ"
                value={information.diaChi}
                type="text"
                placeholder="Nhập địa chỉ"
                onChange={(e) =>
                  setInformation({ ...information, diaChi: e.target.value })
                } // Cập nhật state
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
