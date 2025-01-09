import React, { useEffect, useState } from "react";
import { fetchGet, fetchPut } from "../../../../lib/httpHandler";
import AddPatien from "../../../../components/Admin/PatienManagement/AddPatien/AddPatien";
import { IoIosSearch } from "react-icons/io";
import { FaLock } from "react-icons/fa";
import "./Patien.css";
import DetailPatien from "../../../../components/Admin/PatienManagement/DetailPatien/DetailPatien";
import DeletePatien from "../../../../components/Admin/PatienManagement/DeletePatien/DeletePatien";
import { showYesNoMessageBox } from "../../../../components/MessageBox/YesNoMessageBox/showYesNoMessgeBox";
import { showSuccessMessageBox } from "../../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";

export default function Patien() {
  const [listPatien, setListPatien] = useState([]);
  const [listPatienShow, setlistPatienShow] = useState([]);
  const [dataSearch, setDataSearch] = useState("");
  // Lấy danh sách bệnh nhân
  useEffect(() => {
    const uri = "/api/admin/quan-li-benh-nhan";
    fetchGet(
      uri,
      (sus) => {
        setListPatien(sus);
        // console.log(">>>>>>>>sus Patien", sus);
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

  // Hàm lock bệnh nhân
  const handleLockPatient = async (patientId) => {
    const patient = listPatienShow.find((p) => p.id === patientId);
    const uri = `/api/admin/quan-li-benh-nhan/lock`;
    const result = await showYesNoMessageBox(
      "Bạn có muốn thay đổi trạng thái tài khoản này không?"
    );
    if (result) {
      fetchPut(
        uri,
        {
          Id: patientId,
          IsLock: !patient.isLock, // Đảo ngược trạng thái khóa
        },
        (sus) => {
          showSuccessMessageBox(sus.message);
          // Tại đây, bạn có thể cập nhật lại trạng thái của bệnh nhân trong state
          setListPatien((prevList) =>
            prevList.map((p) =>
              p.id === patientId ? { ...p, isLock: !p.isLock } : p
            )
          );
        },
        (fail) => {
          showErrorMessageBox(fail.message);
        },
        () => {
          showErrorMessageBox("Server mất kết nối");
        }
      );
    } else {
    }
  };

  return (
    // <>đây là trang bệnh nhân</>
    <>
      <div className="Patiens_Management">
        <div className="title py-3 fs-5 mb-2">
          Total number of Patients: {listPatienShow.length}
        </div>
        <div className="row mx-0 my-0">
          <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
            <div className="d-flex align-items-center col-10">
              <div className="contain_Search position-relative col-4 me-3">
                <input
                  onChange={handleSearch}
                  value={dataSearch}
                  className="search rounded-2 px-3"
                  placeholder="Enter your name or phone number"
                />
                <IoIosSearch className="icon_search translate-middle-y text-secondary" />
              </div>
            </div>
            <AddPatien setListPatien={setListPatien} listPatien={listPatien} />
          </div>
          <div className="contain_Table mx-0 col-12 bg-white rounded-2">
            <table className="table table-hover">
              <thead>
                <tr>
                  <th>STT</th>
                  <th>Full name</th>
                  <th>Gender</th>
                  <th>Date of birth</th>
                  <th>Phone number</th>
                  <th>Address</th>
                  <th>IsLock</th>
                  <th>Action</th>
                </tr>
              </thead>
              <tbody>
                {listPatienShow &&
                  listPatienShow.length > 0 &&
                  listPatienShow.map((item, index) => (
                    <tr key={item.id}>
                      <td>{index + 1}</td>
                      <td>{item.hoTen}</td>
                      <td>{item.gioiTinh}</td>
                      <td>{item.tuoi}</td>
                      <td>{item.soDienThoai}</td>
                      <td>{item.diaChi}</td>
                      <td>
                        <input
                          type="checkbox"
                          checked={item.isLock} // Giả định rằng trong dữ liệu của bạn có trường 'isLock'
                          onChange={() => handleLockPatient(item.Id)}
                          disabled // Disable để tránh người dùng thay đổi trực tiếp checkbox
                        />
                      </td>
                      <td>
                        <div className="list_Action">
                          <FaLock
                            className="icon_Lock icon_action fs-6"
                            onClick={() => handleLockPatient(item.id)}
                          />
                          <DetailPatien
                            item={item}
                            setListPatien={setListPatien}
                            listPatien={listPatien}
                          />
                          <DeletePatien
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
