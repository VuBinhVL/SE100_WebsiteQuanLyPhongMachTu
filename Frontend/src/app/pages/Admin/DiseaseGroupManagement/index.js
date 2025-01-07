import React, { useEffect, useState } from "react";
import { fetchGet } from "../../../lib/httpHandler";
import AddDiseaseGroup from "../../../components/Admin/DiseaseGroupManagement/AddDiseaseGroup/AddDiseaseGroup";
import { IoIosSearch } from "react-icons/io";
import "./DiseaseGroup.css";
import DetailDiseaseGroup from "../../../components/Admin/DiseaseGroupManagement/DetailDiseaseGroup/DetailDiseaseGroup";
import DeleteDiseaseGroup from "../../../components/Admin/DiseaseGroupManagement/DeleteDiseaseGroup/DeleteDiseaseGroup";

export default function DiseaseGroup() {
  const [listPatien, setListPatien] = useState([]);
  const [listPatienShow, setlistPatienShow] = useState([]);
  const [dataSearch, setDataSearch] = useState("");
  // Lấy danh sách loại bệnh
  useEffect(() => {
    const uri = "/api/admin/quan-li-nhom-benh";
    fetchGet(
      uri,
      (sus) => {
        setListPatien(sus);
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
    let filteredList = [...listPatien];
    // Tìm kiếm theo họ tên hoặc số điện thoại
    if (searchValue.trim()) {
      const lowercasedSearch = searchValue.toLowerCase();
      filteredList = filteredList.filter(
        (item) =>
          item.hoTen.toLowerCase().includes(lowercasedSearch) ||
          item.soDienThoai.includes(lowercasedSearch)
      );
    }
    setlistPatienShow(filteredList);
  };
  // Cập nhật danh sách hiển thị khi listPatien thay đổi
  useEffect(() => {
    setlistPatienShow(listPatien);
  }, [listPatien]);

  return (
    // <>đây là trang quản lý loại bệnh</>
    <>
      <div className="disease-group">
        <div className="title py-3 fs-5 mb-2">
          Số lượng nhóm bệnh: {listPatienShow.length}
        </div>
        <div className="row mx-0 my-0">
          <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
            <div className="d-flex align-items-center col-10">
              <div className="contain_Search position-relative col-4 me-3">
                <input
                  onChange={handleSearch}
                  value={dataSearch}
                  className="search rounded-2 px-3"
                  placeholder="Nhập tên nhóm bệnh muốn tìm"
                />
                <IoIosSearch className="icon_search translate-middle-y text-secondary" />
              </div>
            </div>
            <AddDiseaseGroup
              setListPatien={setListPatien}
              listPatien={listPatien}
            />
          </div>
          <div className="contain_Table mx-0 col-12 bg-white rounded-2">
            <table className="table table-hover">
              <thead>
                <tr>
                  <th>STT</th>
                  <th>Tên nhóm bệnh</th>
                  <th>Thao tác</th>
                </tr>
              </thead>
              <tbody>
                {listPatienShow &&
                  listPatienShow.length > 0 &&
                  listPatienShow.map((item, index) => (
                    <tr key={item.id}>
                      <td>{index + 1}</td>
                      <td>{item.tenNhomBenh}</td>
                      <td>
                        <div className="list_Action">
                          <DetailDiseaseGroup
                            item={item}
                            setListPatien={setListPatien}
                            listPatien={listPatien}
                          />
                          <DeleteDiseaseGroup
                            item={item}
                            setListPatien={setListPatien}
                            listPatien={listPatien}
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
