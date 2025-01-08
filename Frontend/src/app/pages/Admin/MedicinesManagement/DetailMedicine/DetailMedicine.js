import React, { useEffect } from "react";
import "./DetailMedicine.css";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import { showSuccessMessageBox } from "../../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox";
import {
  fetchPost,
  fetchGet,
  fetchUpload,
  BE_ENPOINT,
} from "../../../../lib/httpHandler";
import { useState } from "react";

export default function DetailMedicine({ id, onClose, onUpdate }) {
  const [avatar, setAvatar] = useState(""); // Lưu ảnh thuốc
  const [dsLoaiThuoc, setDsLoaiThuoc] = useState([]); // Lưu danh sách loại thuốc
  const [isEditing, setIsEditing] = useState(false); // Quản lý trạng thái chỉnh sửa

  const [thuoc, setThuoc] = useState({
    tenThuoc: "",
    giaNhap: "",
    soLuongTon: "",
    ngaySanXuat: "",
    hanSuDung: "",
    loaiThuocId: "",
    images: "",
  }); //Thông tin thuốc để hiển thị lên view

  const formatDateToInput = (dateString) => {
    if (!dateString) return ""; // Nếu không có giá trị, trả về chuỗi rỗng
    return dateString.split("T")[0]; // Lấy phần trước ký tự "T"
  };

  //Gọi API lấy thông tin
  useEffect(() => {
    //Lấy thông tin loại thuốc
    const uri = "/api/admin/quan-li-loai-thuoc";
    fetchGet(
      uri,
      (sus) => {
        setDsLoaiThuoc(sus);
      },
      (err) => {
        showErrorMessageBox(err.message);
      },
      () => showErrorMessageBox("Máy chủ mất kết nối")
    );

    //Lấy thông tin thuốc
    const uri2 = "/api/admin/quan-li-thuoc/" + id;
    fetchGet(
      uri2,
      (res) => {
        setThuoc({
          tenThuoc: res.tenThuoc || "",
          giaNhap: res.giaNhap || "",
          soLuongTon: res.soLuongTon || "",
          ngaySanXuat: res.ngaySanXuat || "",
          hanSuDung: res.hanSuDung || "",
          loaiThuocId: res.loaiThuocId || "",
          images: res.images || "",
        });
        setAvatar(res.images || ""); // Hiển thị ảnh nếu có
      },
      (err) => {
        console.log(err);
      },
      () => showErrorMessageBox("Máy chủ bị mất kết nối")
    );
  }, []);

  //Giúp lưu ảnh
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

  //hàm kiểm tra lỗi
  const validateData = () => {
    if (!thuoc.tenThuoc || thuoc.tenThuoc.trim() === "") {
      showErrorMessageBox("Tên thuốc không được để trống.");
      return false;
    }
    if (!thuoc.loaiThuocId || thuoc.loaiThuocId === "DEFAULT") {
      showErrorMessageBox("Vui lòng chọn loại thuốc.");
      return false;
    }
    return true; // Dữ liệu hợp lệ
  };

  //API update thuốc
  const handleSave = () => {
    if (!validateData()) {
      return; // Dừng lại nếu dữ liệu không hợp lệ
    }
    //Chuẩn bị data
    const dataToSend = {
      id: id,
      tenThuoc: thuoc.tenThuoc,
      images: avatar || null, // So sánh avatar hiện tại với ảnh gốc
      loaiThuocId: thuoc.loaiThuocId,
    };

    const uri = "/api/admin/quan-li-thuoc/update";
    fetchPost(
      uri,
      dataToSend,
      (res) => {
        showSuccessMessageBox(res.message);
        // Gọi hàm callback để cập nhật danh sách thuốc
        onUpdate({ ...thuoc, id, images: avatar });
        setIsEditing(false);
      },
      (err) => showErrorMessageBox(err.message),
      () => showErrorMessageBox("Máy chủ mất kết nối")
    );
  };

  return (
    <div className="modal-overlay-detail-medicine">
      <div className="modal-container">
        <div className="modal-header">
          <h5 className="modal-title">
            {isEditing ? "Chỉnh sửa thuốc" : "Thông tin thuốc"}
          </h5>

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
                value={thuoc.tenThuoc || ""} // Hiển thị
                placeholder="Nhập tên thuốc"
                onChange={(e) =>
                  setThuoc({ ...thuoc, tenThuoc: e.target.value })
                }
                required
                disabled={!isEditing} // Chỉ cho phép chỉnh sửa khi isEditing = true
              />
            </div>
            <div className="form-group">
              <label htmlFor="soLuong">Số lượng:</label>
              <input
                name="soLuong"
                id="soLuong"
                type="number"
                value={thuoc.soLuongTon || ""} // Hiển thị
                placeholder="Nhập số lượng"
                required
                disabled={!isEditing} // Chỉ cho phép chỉnh sửa khi isEditing = true
                readOnly
              />
            </div>
            <div className="form-group">
              <label htmlFor="donGia">Giá nhập:</label>
              <input
                name="donGia"
                id="donGia"
                type="number"
                value={thuoc.giaNhap || ""} // Hiển thị
                placeholder="Nhập giá nhập"
                disabled={!isEditing} // Chỉ cho phép chỉnh sửa khi isEditing = true
                readOnly
              />
            </div>
            <div className="form-group">
              <label htmlFor="loaiThuocId">Loại thuốc:</label>
              <select
                id="loaiThuocId"
                name="loaiThuocId"
                value={thuoc.loaiThuocId} // Giá trị được chọn
                onChange={(e) =>
                  setThuoc({ ...thuoc, loaiThuocId: e.target.value })
                }
                disabled={!isEditing} // Chỉ cho phép chỉnh sửa khi isEditing = true
              >
                <option value="DEFAULT" hidden disabled>
                  Chọn loại thuốc
                </option>
                {dsLoaiThuoc.map((loai) => (
                  <option key={loai.id} value={loai.id}>
                    {loai.tenLoaiThuoc}
                  </option>
                ))}
              </select>
            </div>
            <div className="form-group">
              <label htmlFor="ngayNhap">Ngày sản xuất:</label>
              <input
                type="date"
                id="ngayNhap"
                name="ngayNhap"
                value={formatDateToInput(thuoc.ngaySanXuat)} // Định dạng ngày
                disabled={!isEditing} // Chỉ cho phép chỉnh sửa khi isEditing = true
                readOnly
              />
            </div>
            <div className="form-group">
              <label htmlFor="hanSuDung">Ngày hết hạn:</label>
              <input
                type="date"
                id="hanSuDung"
                name="hanSuDung"
                value={formatDateToInput(thuoc.hanSuDung)} // Định dạng ngày
                readOnly
                disabled={!isEditing} // Chỉ cho phép chỉnh sửa khi isEditing = true
              />
            </div>
          </form>
          <div className="image-container">
            <img
              className="inner-image"
              src={
                avatar ||
                "https://www.freeiconspng.com/uploads/medicine-icon-5.png"
              }
            />
            {isEditing && (
              <>
                <label htmlFor="upload-avatar" className="change-photo">
                  Chọn ảnh
                </label>
                <input
                  type="file"
                  id="upload-avatar"
                  onChange={handleFileChange} // Gọi hàm upload ảnh khi chọn tệp
                />
              </>
            )}
          </div>
        </div>
        <div className="modal-footer">
          {!isEditing ? (
            // Chỉ hiển thị nút Sửa nếu không ở chế độ chỉnh sửa
            <button className="btn-edit" onClick={() => setIsEditing(true)}>
              Sửa
            </button>
          ) : (
            // Hiển thị nút Lưu và Hủy khi đang chỉnh sửa
            <>
              <button
                className="btn-cancel"
                onClick={() => setIsEditing(false)}
              >
                Hủy
              </button>
              <button className="btn-accept" onClick={handleSave}>
                Lưu
              </button>
            </>
          )}
        </div>
      </div>
    </div>
  );
}
