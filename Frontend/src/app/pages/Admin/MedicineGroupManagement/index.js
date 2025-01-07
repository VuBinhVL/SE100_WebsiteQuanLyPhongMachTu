import React, { useEffect, useState } from "react";
import { fetchGet } from "../../../lib/httpHandler";
import AddMedicineGroup from "../../../components/Admin/MedicineGroupManagement/AddMedicineGroup/AddMedicineGroup";
import { IoIosSearch } from "react-icons/io";
import { FaLock } from "react-icons/fa";
import "./MedicineGroup.css";
import DetailMedicineGroup from "../../../components/Admin/MedicineGroupManagement/DetailMedicineGroup/DetailMedicineGroup";
import DeleteMedicineGroup from "../../../components/Admin/MedicineGroupManagement/DeleteMedicineGroup/DeleteMedicineGroup";

export default function MedicineGroup() {
  const [listGroupMedicine, setlistGroupMedicine] = useState([]);
  const [listGroupMedicineShow, setlistGroupMedicineShow] = useState([]);
  const [dataSearch, setDataSearch] = useState("");
  // Lấy danh sách loại thuốc
  useEffect(() => {
    const uri = "/api/admin/quan-li-loai-thuoc";
    fetchGet(
      uri,
      (sus) => {
        setlistGroupMedicine(sus);
      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
      }
    );
  }, []);
  // Hàm xử lý tìm kiếm
  const handleSearch = (e) => {
    const value = e.target.value;
    setDataSearch(value);
    applySearch(value);
  };
  // Hàm áp dụng tìm kiếm và lọc
  const applySearch = (searchValue) => {
    let filteredList = [...listGroupMedicine];
    // Tìm kiếm theo tên loại bệnh
    if (searchValue.trim()) {
      const lowercasedSearch = searchValue.toLowerCase();
      filteredList = filteredList.filter((item) =>
        item.tenLoaiThuoc.toLowerCase().includes(lowercasedSearch)
      );
    }
    setlistGroupMedicineShow(filteredList);
  };
  // Cập nhật danh sách hiển thị khi listGroupMedicine thay đổi
  useEffect(() => {
    setlistGroupMedicineShow(listGroupMedicine);
  }, [listGroupMedicine]);
  return (
    // <>đây là trang loại thuốc</>
    <>
      <div className="medicine-group-management">
        <div className="title py-3 fs-5 mb-2">
          Số lượng loại thuốc: {listGroupMedicineShow.length}
        </div>
        <div className="row mx-0 my-0">
          <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
            <div className="d-flex align-items-center col-10">
              <div className="contain_Search position-relative col-4 me-3">
                <input
                  onChange={handleSearch}
                  value={dataSearch}
                  className="search rounded-2 px-3"
                  placeholder="Nhập tên loại thuốc cần tìm"
                />
                <IoIosSearch className="icon_search translate-middle-y text-secondary" />
              </div>
            </div>
            <AddMedicineGroup
              setlistGroupMedicine={setlistGroupMedicine}
              listGroupMedicine={listGroupMedicine}
            />
          </div>
          <div className="contain_Table mx-0 col-12 bg-white rounded-2">
            <table className="table table-hover">
              <thead>
                <tr>
                  <th>STT</th>
                  <th>Tên loại thuốc</th>
                  <th>Thao tác</th>
                </tr>
              </thead>
              <tbody>
                {listGroupMedicineShow &&
                  listGroupMedicineShow.length > 0 &&
                  listGroupMedicineShow.map((item, index) => (
                    <tr key={item.id}>
                      <td>{index + 1}</td>
                      <td>{item.tenLoaiThuoc}</td>

                      <td>
                        <div className="list_Action">
                          <DetailMedicineGroup
                            item={item}
                            setlistGroupMedicine={setlistGroupMedicine}
                            listGroupMedicine={listGroupMedicine}
                          />
                          <DeleteMedicineGroup
                            item={item}
                            setlistGroupMedicine={setlistGroupMedicine}
                            listGroupMedicine={listGroupMedicine}
                          />
                        </div>
                      </td>
                    </tr>
                  ))}
              </tbody>
            </table>
            <nav className="contain_pagination">
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
            </nav>
          </div>
        </div>
      </div>
    </>
  );
}
