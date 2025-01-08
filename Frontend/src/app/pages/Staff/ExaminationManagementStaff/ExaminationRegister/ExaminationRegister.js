import React, { useEffect, useState } from "react";
import "./ExaminationRegister.css";
import { IoIosArrowDown } from "react-icons/io";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import DetailExamination from "../DetailExamination/DetailExamination";
import { fetchGet } from "../../../../lib/httpHandler";

export default function ExaminationRegister() {
  const [listShift, setListShift] = useState([]); // Lưu danh sách ca khám
  const [filters, setFilters] = useState({
    ngayKham: "",
    tenLoaiBenh: "Tất cả",
    khungGio: "Tất cả",
    chuaDangKy: false, // Lọc ca khám chưa có ai đăng ký
  });

  // Gọi API lấy danh sách ca khám
  useEffect(() => {
    const uri = "/api/admin/quan-li-ca-kham";
    fetchGet(
      uri,
      (res) => {
        setListShift(res);
      },
      (err) => showErrorMessageBox(err.message),
      () => showErrorMessageBox("Máy chủ mất kết nối")
    );
  }, []);

  // Chuyển đổi ngày
  const formatDateToDisplay = (dateTimeString) => {
    if (!dateTimeString) return ""; // Nếu không có giá trị, trả về chuỗi rỗng
    const [date] = dateTimeString.split("T"); // Tách phần ngày và thời gian
    const [year, month, day] = date.split("-"); // Tách chuỗi ngày thành năm, tháng, ngày
    return `${day}/${month}/${year}`; // Kết hợp theo định dạng dd/mm/yyyy
  };

  // Hàm chuyển đổi thời gian bắt đầu, kết thúc thành khung giờ
  const convertTimeToSlot = (timeStart, timeEnd) => {
    return `${timeStart.slice(0, 5)} - ${timeEnd.slice(0, 5)}`;
  };

  // Danh sách động: Tên loại bệnh và khung giờ
  const uniqueLoaiBenh = [
    ...new Set(listShift.map((item) => item.tenNhomBenh)),
  ].filter(Boolean); // Lọc ra các loại bệnh duy nhất
  const uniqueKhungGio = [
    ...new Set(
      listShift.map((item) =>
        convertTimeToSlot(item.thoiGianBatDau, item.thoiGianKetThuc)
      )
    ),
  ];

  // Xử lý thay đổi bộ lọc
  const handleFilterChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFilters((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  // Áp dụng bộ lọc
  const filteredShifts = listShift.filter((item) => {
    const matchNgayKham =
      !filters.ngayKham || item.ngayKham.startsWith(filters.ngayKham); // Lọc theo ngày
    const matchLoaiBenh =
      filters.tenLoaiBenh === "Tất cả" ||
      item.tenNhomBenh === filters.tenLoaiBenh; // Lọc theo tên loại bệnh
    const matchKhungGio =
      filters.khungGio === "Tất cả" ||
      convertTimeToSlot(item.thoiGianBatDau, item.thoiGianKetThuc) ===
        filters.khungGio; // Lọc theo khung giờ
    const matchChuaDangKy = !filters.chuaDangKy || !item.bacSiKham; // Lọc ca chưa đăng ký

    return matchNgayKham && matchLoaiBenh && matchKhungGio && matchChuaDangKy;
  });

  return (
    <div className="examination-register">
      <div className="title py-3 fs-5 mb-2">
        Tổng số lượng ca khám: {filteredShifts.length}
      </div>
      <div className="row mx-0 my-0">
        <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
          <div className="d-flex align-items-center col-10">
            {/* Bộ lọc Tên loại bệnh */}
            <div className="form-group d-flex align-items-center position-relative me-3">
              <select
                id="tenLoaiBenh"
                name="tenLoaiBenh"
                className="form-control rounded-3 py-2 TimeSlot"
                value={filters.tenLoaiBenh}
                onChange={handleFilterChange}
              >
                <option value="Tất cả">Tất cả</option>
                {uniqueLoaiBenh.map((loaiBenh, index) => (
                  <option key={index} value={loaiBenh}>
                    {loaiBenh}
                  </option>
                ))}
              </select>
              <IoIosArrowDown className="position-absolute end-0 me-3" />
            </div>

            {/* Bộ lọc Khung giờ */}
            <div className="form-group d-flex align-items-center position-relative me-3">
              <select
                id="khungGio"
                name="khungGio"
                className="form-control rounded-3 py-2 TimeSlot"
                value={filters.khungGio}
                onChange={handleFilterChange}
              >
                <option value="Tất cả">Tất cả</option>
                {uniqueKhungGio.map((khungGio, index) => (
                  <option key={index} value={khungGio}>
                    {khungGio}
                  </option>
                ))}
              </select>
              <IoIosArrowDown className="position-absolute end-0 me-3" />
            </div>

            {/* Bộ lọc Ngày khám */}
            <div className="form-group d-flex align-items-center">
              <input
                type="date"
                id="ngayKham"
                name="ngayKham"
                className="form-control ConsultationDate py-2 rounded-3"
                value={filters.ngayKham}
                onChange={handleFilterChange}
              />
            </div>

            {/* Bộ lọc Chưa đăng ký */}
            <div className="form-group checkbox-container">
              <label className="checkbox-label">
                <input
                  type="checkbox"
                  name="chuaDangKy"
                  className="checkbox-input"
                  checked={filters.chuaDangKy}
                  onChange={handleFilterChange}
                />
                Chưa có ai đăng ký
              </label>
            </div>
          </div>
        </div>

        <div className="contain_Table mx-0 col-12 bg-white rounded-2">
          <table className="table table-hover">
            <thead>
              <tr>
                <th>STT</th>
                <th>Tên ca khám</th>
                <th>Khung giờ</th>
                <th>Ngày khám</th>
                <th>Loại bệnh</th>
                <th>Bác sĩ</th>
                <th>Thao tác</th>
              </tr>
            </thead>
            <tbody>
              {filteredShifts.map((item, index) => (
                <tr key={item.id}>
                  <td>{index + 1}</td>
                  <td>{item.tenCaKham}</td>
                  <td>
                    {convertTimeToSlot(
                      item.thoiGianBatDau,
                      item.thoiGianKetThuc
                    )}
                  </td>
                  <td>{formatDateToDisplay(item.ngayKham)}</td>
                  <td>{item.tenNhomBenh}</td>
                  <td>{item.bacSiKham || "Chưa có ai đăng kí"}</td>
                  <td>
                    <div className="list_Action">
                      <DetailExamination
                        item={item}
                        setListShift={setListShift}
                        listShift={listShift}
                      />
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}
