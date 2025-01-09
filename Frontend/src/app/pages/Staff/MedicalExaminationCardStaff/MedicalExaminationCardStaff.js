import React, { useEffect, useState } from "react";
import "./MedicalExaminationCardStaff.css";
import { IoIosArrowDown } from "react-icons/io";
import { IoIosSearch } from "react-icons/io";
import { showErrorMessageBox } from "../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import { fetchGet, fetchPut } from "../../../lib/httpHandler";
import DetailExaminationForm from "../../../components/Admin/ExaminationFormManagement/DetailExaminationForm/DetailExaminationForm";
import DeleteExaminationForm from "../../../components/Admin/ExaminationFormManagement/DeleteExaminationForm/DeleteExaminationForm";

export default function MedicalExaminationCardStaff() {
  const [listShift, setListShift] = useState([]); // Lưu danh sách ca khám
  const [listExaminationForm, setListExaminationForm] = useState([]);
  const [listExaminationFormShow, setListExaminationFormShow] = useState([]);
  const [dataSearch, setDataSearch] = useState(""); // Lưu dữ liệu tìm kiếm
  const [filters, setFilters] = useState({
    tenTrangThaiPKB: "Tất cả", // Lưu trạng thái phiếu khám bệnh
  });

  // Gọi API lấy danh sách phiếu khám bệnh của bản thân
  useEffect(() => {
    const uri = "/api/admin/quan-li-phieu-kham-benh/my-self";
    fetchGet(
      uri,
      (res) => {
        setListShift(res);
      },
      (err) => showErrorMessageBox(err.message),
      () => showErrorMessageBox("Máy chủ mất kết nối")
    );
  }, []);

  // api lấy danh sách biểu mẫu khám bệnh
  useEffect(() => {
    const uri = "/api/admin/quan-li-phieu-kham-benh";
    fetchGet(
      uri,
      (sus) => {
        setListExaminationForm(sus);
      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
      }
    );
  }, []);
  // Chuyển đổi ngày
  const formatDateTimeToDisplay = (dateTimeString) => {
    if (!dateTimeString) return ""; // Nếu không có giá trị, trả về chuỗi rỗng
    const [date, time] = dateTimeString.split("T"); // Tách phần ngày và thời gian
    const [year, month, day] = date.split("-"); // Tách chuỗi ngày thành năm, tháng, ngày
    const [hour, minute] = time.split(":"); // Tách giờ và phút
    return `${hour}:${minute} - ${day}/${month}/${year}`; // Kết hợp thành định dạng hh:mm - dd/mm/yyyy
  };

  // // Xử lý thay đổi bộ lọc
  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  //Tìm kiếm theo tên hoặc sdt
  const handleSearch = (e) => {
    setDataSearch(e.target.value.toLowerCase()); // Cập nhật giá trị tìm kiếm, chuyển thành chữ thường để so sánh
  };

  // Áp dụng bộ lọc
  const filteredShifts = listShift.filter((item) => {
    const matchSearch =
      !dataSearch ||
      item.tenBenhNhan.toLowerCase().includes(dataSearch) || // Lọc theo tên bệnh nhân
      item.soDienThoai.includes(dataSearch); // Lọc theo số điện thoại
    const matchTrangThai =
      filters.tenTrangThaiPKB === "Tất cả" || // Hiển thị tất cả nếu không lọc
      item.tenTrangThaiPKB === filters.tenTrangThaiPKB; // Lọc theo trạng thái phiếu khám bệnh
    return matchSearch && matchTrangThai;
  });

  return (
    <div className="examination-list-staff">
      <div className="title py-3 fs-5 mb-2">
        Tổng số lượng ca khám: {filteredShifts.length}
      </div>
      <div className="row mx-0 my-0">
        <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
          <div className="d-flex align-items-center col-9">
            <div className="contain_Search position-relative me-3">
              <input
                onChange={handleSearch}
                value={dataSearch}
                className="search rounded-2 px-3 me-1"
                placeholder="Nhập tên bệnh nhân hoặc SĐT cần tìm"
              />
              <IoIosSearch className="icon_search translate-middle-y text-secondary" />
            </div>
          </div>
          {/* Bộ lọc trạng thái khám */}
          <div className="form-group d-flex align-items-center position-relative me-1">
            <select
              id="trangThaiPKB"
              name="tenTrangThaiPKB"
              className="form-control ConsultationDate py-2 rounded-3"
              value={filters.tenTrangThaiPKB}
              onChange={handleFilterChange}
            >
              <option value="Tất cả">Tất cả</option>
              <option value="Đang chờ">Đang chờ</option>
              <option value="Đang khám">Đang khám</option>
              <option value="Hoàn tất">Hoàn tất</option>
              <option value="Đã hủy">Đã hủy</option>
            </select>
            <IoIosArrowDown className="position-absolute end-0 me-3" />
          </div>
        </div>

        <div className="contain_Table mx-0 col-12 bg-white rounded-2">
          <table className="table table-hover">
            <thead>
              <tr>
                <th>STT</th>
                <th>Tên bệnh nhân</th>
                <th>Số điện thoại</th>
                <th>Thời gian tạo</th>
                <th>Trạng thái</th>
                <th>Thao tác</th>
              </tr>
            </thead>
            <tbody>
              {filteredShifts.map((item, index) => (
                <tr key={item.id}>
                  <td>{index + 1}</td>
                  <td>{item.tenBenhNhan}</td>
                  <td>{item.soDienThoai}</td>
                  <td>{formatDateTimeToDisplay(item.ngayTao)}</td>
                  <td>{item.tenTrangThaiPKB}</td>

                  <td>
                    <div className="list_Action">
                      <DetailExaminationForm
                        item={item}
                        setListExaminationForm={setListExaminationForm}
                        listExaminationForm={listExaminationForm}
                      />
                      <DeleteExaminationForm
                        item={item}
                        setListExaminationForm={setListExaminationForm}
                        listExaminationForm={listExaminationForm}
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
