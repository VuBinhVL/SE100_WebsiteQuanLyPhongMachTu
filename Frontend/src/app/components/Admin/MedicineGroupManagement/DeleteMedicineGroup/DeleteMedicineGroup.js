import React, { useState } from "react";
import "./DeleteMedicineGroup.css";
import { MdDelete } from "react-icons/md";
import { fetchDelete } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showDeleteMessageBox } from "../../../MessageBox/DeleteMesssageBox/showDeleteMessageBox";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";

function DeleteMedicineGroup(props) {
  const { setListPatien, listPatien } = props;
  const { item } = props;
  const handleDelete = () => {
    showDeleteMessageBox("Bạn có chắc muốn xóa không", () => {
      const uri = `/api/admin/quan-li-loai-thuoc/delete?id=${item.id}`;
      fetchDelete(
        uri,
        "",
        (sus) => {
          showSuccessMessageBox(sus.message);
          const newPatien = listPatien.filter(
            (newItem) => newItem.id !== item.id
          );
          setListPatien(newPatien);
        },
        (fail) => {
          showErrorMessageBox(fail.message);
        },
        () => {
          alert("Không thể kết nối đến máy chủ");
        }
      );
    });
  };
  return (
    <div className="Delete_Medicine_Group d-inline">
      <a>
        <MdDelete onClick={handleDelete} className="icon_delete icon_action" />
      </a>
    </div>
  );
}
export default DeleteMedicineGroup;
