import React, { useEffect } from "react";
import "./AddMedicine.css";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import { showSuccessMessageBox } from "../../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox";
import {
  fetchPost,
  fetchGet,
  fetchUpload,
  BE_ENPOINT,
} from "../../../../lib/httpHandler";
import { useState } from "react";

export default function AddMedicine({ onClose, onAddMedicine }) {
  const [avatar, setAvatar] = useState(""); // Lưu ảnh thuốc
  const [dsLoaiThuoc, setDsLoaiThuoc] = useState([]); // Lưu danh sách loại thuốc
  const [thuoc, setThuoc] = useState({
    tenThuoc: "",
    donGia: "",
    soLuong: "",
    ngayNhap: "",
    hanSuDung: "",
    loaiThuocId: "",
    images: "",
  }); //Thông tin thuốc

  //Gọi API lấy danh sách loại thuốc
  useEffect(() => {
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
    if (!thuoc.donGia || isNaN(thuoc.donGia) || thuoc.donGia <= 0) {
      showErrorMessageBox("Đơn giá phải là số và lớn hơn 0.");
      return false;
    }
    if (!thuoc.soLuong || isNaN(thuoc.soLuong) || thuoc.soLuong <= 0) {
      showErrorMessageBox("Số lượng phải là số và lớn hơn 0.");
      return false;
    }
    if (!thuoc.ngayNhap || !Date.parse(thuoc.ngayNhap)) {
      showErrorMessageBox("Ngày nhập không hợp lệ.");
      return false;
    }
    if (!thuoc.hanSuDung || !Date.parse(thuoc.hanSuDung)) {
      showErrorMessageBox("Hạn sử dụng không hợp lệ.");
      return false;
    }
    if (new Date(thuoc.ngayNhap) > new Date(thuoc.hanSuDung)) {
      showErrorMessageBox("Hạn sử dụng phải lớn hơn ngày nhập.");
      return false;
    }
    if (!thuoc.loaiThuocId || thuoc.loaiThuocId === "DEFAULT") {
      showErrorMessageBox("Vui lòng chọn loại thuốc.");
      return false;
    }
    if (!avatar) {
      showErrorMessageBox("Vui lòng chọn ảnh cho thuốc.");
      return false;
    }
    return true; // Dữ liệu hợp lệ
  };

  //Thêm thuốc
  const handleSave = () => {
    if (!validateData()) {
      return; // Dừng lại nếu dữ liệu không hợp lệ
    }
    //Chuẩn bị data
    const dataToSend = {
      tenThuoc: thuoc.tenThuoc,
      donGia: thuoc.donGia,
      soLuong: thuoc.soLuong,
      ngayNhap: thuoc.ngayNhap,
      hanSuDung: thuoc.hanSuDung,
      loaiThuocId: thuoc.loaiThuocId,
      images: avatar || null,
    };

    const uri = "/api/admin/quan-li-phieu-nhap-thuoc/add";
    fetchPost(
      uri,
      dataToSend,
      (res) => {
        showSuccessMessageBox(res.message);
        // Tự thêm dữ liệu vào danh sách (giả lập ID mới)
        const newMedicine = {
          id: 1, // Giả lập ID
          ...dataToSend,
        };
        onAddMedicine(newMedicine); // Gửi dữ liệu thuốc mới lên MedicineManagement
        onClose();
      },
      (err) => showErrorMessageBox(err.message),
      () => showErrorMessageBox("Máy chủ mất kết nối")
    );
    console.log(dataToSend);
  };

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
                onChange={(e) =>
                  setThuoc({ ...thuoc, tenThuoc: e.target.value })
                }
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="soLuong">Số lượng:</label>
              <input
                name="soLuong"
                id="soLuong"
                type="number"
                placeholder="Nhập số lượng"
                onChange={(e) =>
                  setThuoc({ ...thuoc, soLuong: e.target.value })
                }
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="donGia">Giá nhập:</label>
              <input
                name="donGia"
                id="donGia"
                type="number"
                placeholder="Nhập giá nhập"
                onChange={(e) => setThuoc({ ...thuoc, donGia: e.target.value })}
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
                onChange={(e) =>
                  setThuoc({ ...thuoc, ngayNhap: e.target.value })
                }
              />
            </div>
            <div className="form-group">
              <label htmlFor="hanSuDung">Ngày hết hạn:</label>
              <input
                type="date"
                id="hanSuDung"
                name="hanSuDung"
                onChange={(e) =>
                  setThuoc({ ...thuoc, hanSuDung: e.target.value })
                }
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
            <label htmlFor="upload-avatar" className="change-photo">
              Chọn ảnh
            </label>
            <input
              type="file"
              id="upload-avatar"
              onChange={handleFileChange} // Gọi hàm upload ảnh khi chọn tệp
            />
          </div>
        </div>
        <div className="modal-footer">
          <button className="btn-cancel" onClick={onClose}>
            Hủy
          </button>
          <button className="btn-accept" onClick={handleSave}>
            Thêm
          </button>
        </div>
      </div>
    </div>
  );
}
