import React, { useEffect, useState } from "react";
import { fetchGet, fetchDelete } from "../../../../lib/httpHandler";
import { IoIosSearch } from "react-icons/io";
import { IoIosInformationCircleOutline } from "react-icons/io";
import { MdDelete } from "react-icons/md";
import { IoIosAddCircleOutline } from "react-icons/io";
import AddMedicine from "../AddMedicine/AddMedicine";
import DetailMedicine from "../DetailMedicine/DetailMedicine";
import "./MedicineManagement.css";
import { showSuccessMessageBox } from "../../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import { showDeleteMessageBox } from "../../../../components/MessageBox/DeleteMesssageBox/showDeleteMessageBox";

export default function MedicineManagement() {
  const [listThuoc, setListThuoc] = useState([]); //Lưu trữ danh sách thuốc
  const [dataSearch, setDataSearch] = useState(""); //Phục vụ tìm kiếm thuốc
  const [isModalOpen, setIsModalOpen] = useState(false); //Phục vụ cho việc mở view thêm thuốc
  const [isModalOpenDetail, setIsModalOpenDetail] = useState(false); //Phục vụ cho việc mở view detail
  const [selectedMedicineId, setSelectedMedicineId] = useState(null); // Lưu ID thuốc được chọn

  // Lấy danh sách thuốc
  useEffect(() => {
    const uri = "/api/admin/quan-li-thuoc";
    fetchGet(
      uri,
      (sus) => {
        setListThuoc(sus);
      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
      }
    );
  }, []);

  //Tìm kiếm
  const handleSearch = (e) => {
    const value = e.target.value.toLowerCase(); // Chuyển chuỗi thành chữ thường để so sánh không phân biệt hoa thường
    setDataSearch(value);
  };
  // Lọc danh sách thuốc
  const filteredThuoc = listThuoc.filter((item) =>
    item.tenThuoc?.toLowerCase().includes(dataSearch)
  );

  //Thêm thuốc
  const handleAdd = () => {
    setIsModalOpen(true); // Mở view thêm thuốc
  };

  //Xử lý nút xóa
  const handleDelete = (id) => {
    showDeleteMessageBox("Bạn có muốn xóa thuốc này không", () => {
      //Gọi API để xóa thuốc
      fetchDelete(
        `/api/admin/quan-li-thuoc/delete?id=${id}`,
        "",
        (response) => {
          showSuccessMessageBox(response.message);
          setListThuoc((prevList) => prevList.filter((item) => item.id !== id));
        },
        (err) => {
          showErrorMessageBox(err.message);
        },
        () => showErrorMessageBox("Máy chủ mất kết nối")
      );
    });
  };

  //Xử lý nút detail
  const handleUpdateMedicine = (updatedMedicine) => {
    setListThuoc((prevList) =>
      prevList.map((medicine) =>
        medicine.id === updatedMedicine.id ? updatedMedicine : medicine
      )
    );
  };

  return (
    // <>đây là trang quản lý thuốc</>
    <>
      <div className="medicine-management">
        <div className="title py-3 fs-5 mb-2">
          Số lượng thuốc: {filteredThuoc.length}
        </div>
        <div className="row mx-0 my-0">
          <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
            <div className="d-flex align-items-center col-10">
              <div className="contain_Search position-relative col-4 me-3">
                <input
                  onChange={handleSearch}
                  value={dataSearch}
                  className="search rounded-2 px-3"
                  placeholder="Nhập tên thuốc muốn tìm"
                />
                <IoIosSearch className="icon_search translate-middle-y text-secondary" />
              </div>
            </div>
            <button className="button_add" onClick={handleAdd}>
              <IoIosAddCircleOutline className="icon-add" /> Thêm
            </button>
          </div>
          <div className="contain_Table mx-0 col-12 bg-white rounded-2">
            <table className="table table-hover">
              <thead>
                <tr>
                  <th>STT</th>
                  <th>Tên thuốc</th>
                  <th>Số lượng tồn</th>
                  <th>Giá nhập</th>
                  <th>Thao tác</th>
                </tr>
              </thead>
              <tbody>
                {filteredThuoc.length > 0 &&
                  filteredThuoc.map((item, index) => (
                    <tr key={item.id}>
                      <td>{index + 1}</td>
                      <td>{item.tenThuoc}</td>
                      <td>{item.soLuongTon || item.soLuong}</td>
                      <td>{item.giaNhap || item.donGia}</td>
                      <td>
                        <div className="list_action">
                          <IoIosInformationCircleOutline
                            className="icon-detail"
                            onClick={() => {
                              setSelectedMedicineId(item.id); // Lưu ID thuốc được chọn
                              setIsModalOpenDetail(true); // Mở modal
                            }}
                          />

                          <MdDelete
                            className="icon-delete"
                            onClick={() => handleDelete(item.id)} // Truyền id của item vào hàm handleDelete
                          />
                        </div>
                      </td>
                    </tr>
                  ))}
              </tbody>
            </table>
            {/* <nav className="contain_pagination">
              <ul className="pagination">
                <li className="page-item">
                  <a className="page-link page-link-two" href="#">
                    &laquo;
                  </a>
                </li>
                <li className="page-item active">
                  <a className="page-link" href="#">
                    1
                  </a>
                </li>
                <li className="page-item">
                  <a className="page-link" href="#">
                    2
                  </a>
                </li>
                <li className="page-item">
                  <a className="page-link" href="#">
                    3
                  </a>
                </li>
                <li className="page-item">
                  <a className="page-link page-link-two" href="#">
                    &raquo;
                  </a>
                </li>
              </ul>
            </nav> */}
          </div>
        </div>
        {/* Hiển thị popup view thêm thuốc */}
        {isModalOpen && (
          <AddMedicine
            onClose={() => setIsModalOpen(false)}
            onAddMedicine={(newMedicine) => {
              setListThuoc((prevList) => [...prevList, newMedicine]); // Thêm thuốc mới vào danh sách
            }}
          />
        )}

        {/* Hiển thị popup view detail thuốc */}
        {isModalOpenDetail && (
          <DetailMedicine
            onClose={() => setIsModalOpenDetail(false)}
            id={selectedMedicineId} // Truyền ID thuốc được chọn
            onUpdate={handleUpdateMedicine}
          />
        )}
      </div>
    </>
  );
}
