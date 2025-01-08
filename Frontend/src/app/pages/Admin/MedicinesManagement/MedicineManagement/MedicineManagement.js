import React, { useEffect, useState } from "react";
import { fetchGet } from "../../../../lib/httpHandler";
import { IoIosSearch } from "react-icons/io";
import { IoIosInformationCircleOutline } from "react-icons/io";
import { MdDelete } from "react-icons/md";
import { IoIosAddCircleOutline } from "react-icons/io";

import "./MedicineManagement.css";

export default function MedicineManagement() {
  const [listThuoc, setListThuoc] = useState([]); //Lưu trữ danh sách thuốc
  const [dataSearch, setDataSearch] = useState(""); //Phục vụ tìm kiếm thuốc
  // Lấy danh sách thuốc
  useEffect(() => {
    const uri = "/api/admin/quan-li-thuoc";
    fetchGet(
      uri,
      (sus) => {
        console.log(sus);
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

  //Xử lý hiển thị popup thêm
  const handleAdd = () => {
    alert("Đã vào nút thêm");
  };

  //Xử lý nút xóa
  const handleDelete = () => {
    alert("Đã vào nút xóa");
  };

  //Xử lý nút detail
  const handleDetail = () => {
    alert("Đã vào nút detail");
  };

  return (
    // <>đây là trang quản lý thuốc</>
    <>
      <div className="medicine-management">
        <div className="title py-3 fs-5 mb-2">
          Số lượng thuốc: {listThuoc.length}
        </div>
        <div className="row mx-0 my-0">
          <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
            <div className="d-flex align-items-center col-10">
              <div className="contain_Search position-relative col-4 me-3">
                <input
                  //onChange={handleSearch}
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
                {listThuoc &&
                  listThuoc.length > 0 &&
                  listThuoc.map((item, index) => (
                    <tr key={item.id}>
                      <td>{index + 1}</td>
                      <td>{item.tenThuoc}</td>
                      <td>{item.soLuongTon}</td>
                      <td>{item.giaNhap}</td>
                      <td>
                        <div className="list_action">
                          <IoIosInformationCircleOutline
                            className="icon-detail"
                            onClick={handleDetail}
                          />
                          <MdDelete
                            className="icon-delete"
                            onClick={handleDelete}
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
