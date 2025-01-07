import React, { useState } from "react";
import "./DeleteDiseaseGroup.css";
import { MdDelete } from "react-icons/md";
import { fetchDelete } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showDeleteMessageBox } from "../../../MessageBox/DeleteMesssageBox/showDeleteMessageBox";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";

function DeleteDiseaseGroup(props) {
  const { setlistDiseaseGroup, listDiseaseGroup } = props;
  const { item } = props;
  const handleDelete = () => {
    showDeleteMessageBox("Bạn có chắc muốn xóa không", () => {
      const uri = `/api/admin/quan-li-nhom-benh/delete?id=${item.id}`;
      fetchDelete(
        uri,
        "",
        (sus) => {
          showSuccessMessageBox(sus.message);
          const newPatien = listDiseaseGroup.filter(
            (newItem) => newItem.id !== item.id
          );
          setlistDiseaseGroup(newPatien);
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
    <div className="Delete_Diseae_Group d-inline">
      <a>
        <MdDelete onClick={handleDelete} className="icon_delete icon_action" />
      </a>
    </div>
  );
}
export default DeleteDiseaseGroup;
