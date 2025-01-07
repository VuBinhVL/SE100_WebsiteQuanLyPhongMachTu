import React, { useEffect, useState } from "react";
import { fetchGet, fetchPost } from "../../../lib/httpHandler";
import { showErrorMessageBox } from "../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox"; // ErrorMessageBox
import { showSuccessMessageBox } from "../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox"; // ErrorMessageBox
import "./Parameter.css";

export default function Parameter() {
  const [listParameter, setListParameter] = useState([]);
  const [editMode, setEditMode] = useState(false);
  const [editedParameters, setEditedParameters] = useState([]);

  // Lấy danh sách tham số
  useEffect(() => {
    const uri = "/api/admin/quan-li-tham-so";
    fetchGet(
      uri,
      (sus) => {
        setListParameter(sus);
        setEditedParameters(JSON.parse(JSON.stringify(sus))); // Tạo bản sao hoàn chỉnh
        console.log(listParameter);
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        showErrorMessageBox("Kết nối máy chủ thất bại");
      }
    );
  }, []);

  const handleEditClick = () => {
    setEditMode(true);
  };

  const handleCancelClick = () => {
    setEditMode(false);
    setEditedParameters(JSON.parse(JSON.stringify(listParameter))); // Tạo bản sao hoàn chỉnh
  };

  //Gọi API để lưu
  const handleSaveClick = () => {
    setEditMode(false);
    const uri = "/api/admin/quan-li-tham-so/update";
    const dataToSend = {
      soLanHuyLichKhamToiDaChoPhep:
        editedParameters[0]?.soLanHuyLichKhamToiDaChoPhep || 0,
      heSoBan: editedParameters[0]?.heSoBan || 0,
      soPhutNgungDangKyTruocKetThuc:
        editedParameters[0]?.soPhutNgungDangKyTruocKetThuc || 0,
    };
    console.log("Dữ liệu gửi lên API:", dataToSend); // Kiểm tra dữ liệu gửi đi

    fetchPost(
      uri,
      dataToSend,
      (success) => {
        showSuccessMessageBox("Lưu thành công!");
        setListParameter([...editedParameters]); // Cập nhật dữ liệu hiển thị
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        showErrorMessageBox("Kết nối máy chủ thất bại");
      }
    );
  };

  const handleParameterChange = (index, field, value) => {
    const updatedParameters = [...editedParameters];
    updatedParameters[index][field] = value;
    setEditedParameters(updatedParameters);
  };

  return (
    <div className="parameter-management">
      <div className="title py-3 fs-5 mb-2 d-flex justify-content-between">
        <span>Bảng tham số hệ thống</span>
        {!editMode && (
          <button
            className="btn btn-primary btn-edit"
            onClick={handleEditClick}
          >
            Chỉnh sửa
          </button>
        )}
      </div>
      <div className="row mx-0 my-0">
        <div className="contain_Table mx-0 col-12 bg-white rounded-2">
          <table className="table table-hover">
            <thead>
              <tr>
                <th>STT</th>
                <th>Tên tham số</th>
                <th>Giá trị</th>
              </tr>
            </thead>
            <tbody>
              {editedParameters.map((item, index) => (
                <React.Fragment key={item.id}>
                  <tr>
                    <td>{index + 1}</td>
                    <td>Số lần hủy lịch khám tối đa cho phép</td>
                    <td>
                      {editMode ? (
                        <input
                          type="number"
                          value={item.soLanHuyLichKhamToiDaChoPhep}
                          onChange={(e) =>
                            handleParameterChange(
                              index,
                              "soLanHuyLichKhamToiDaChoPhep",
                              e.target.value
                            )
                          }
                          className="form-control"
                        />
                      ) : (
                        `${item.soLanHuyLichKhamToiDaChoPhep} lần`
                      )}
                    </td>
                  </tr>
                  <tr>
                    <td>{index + 2}</td>
                    <td>Hệ số bán</td>
                    <td>
                      {editMode ? (
                        <input
                          type="number"
                          value={item.heSoBan}
                          onChange={(e) =>
                            handleParameterChange(
                              index,
                              "heSoBan",
                              e.target.value
                            )
                          }
                          className="form-control"
                        />
                      ) : (
                        item.heSoBan
                      )}
                    </td>
                  </tr>
                  <tr>
                    <td>{index + 3}</td>
                    <td>Số phút ngừng đăng ký trước khi kết thúc</td>
                    <td>
                      {editMode ? (
                        <input
                          type="number"
                          value={item.soPhutNgungDangKyTruocKetThuc}
                          onChange={(e) =>
                            handleParameterChange(
                              index,
                              "soPhutNgungDangKyTruocKetThuc",
                              e.target.value
                            )
                          }
                          className="form-control"
                        />
                      ) : (
                        `${item.soPhutNgungDangKyTruocKetThuc} phút`
                      )}
                    </td>
                  </tr>
                </React.Fragment>
              ))}
            </tbody>
          </table>
        </div>
        {editMode && (
          <div className="d-flex justify-content-end mt-3">
            <button
              className="btn btn-secondary me-2"
              onClick={handleCancelClick}
            >
              Hủy
            </button>
            <button className="btn btn-success" onClick={handleSaveClick}>
              Lưu
            </button>
          </div>
        )}
      </div>
    </div>
  );
}
