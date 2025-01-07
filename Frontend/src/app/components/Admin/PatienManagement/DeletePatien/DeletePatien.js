import React, { useState } from "react";
import "./DeletePatien.css"
import { MdDelete } from "react-icons/md";
import { fetchDelete } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showDeleteMessageBox } from "../../../MessageBox/DeleteMesssageBox/showDeleteMessageBox";
function DeletePatien(props) {
    // const [isVisibility, setIsVisibility] = useState(false);
    const { setListPatien, listPatien } = props;
    const { item } = props;
    const handleDelete = () => {
        showDeleteMessageBox("Bạn có chắc muốn xóa không", () => {
            const uri = `/api/admin/quan-li-benh-nhan/delete?id=${item.id}`;
            fetchDelete(
                uri, "",
                (sus) => {
                    // console.log(">>>>>>.check sus", sus)
                    showSuccessMessageBox(sus.message)
                    const newPatien = listPatien.filter((newItem) => newItem.id !== item.id);
                    setListPatien(newPatien)
                },
                (fail) => {
                    alert(fail.message);
                },
                () => {
                    alert("Có lỗi xảy ra");
                }
            );
        });
    }
    return (
        <div className="Delete_Patien d-inline">
            <a href="#">
                <MdDelete onClick={handleDelete} className="icon_delete icon_action" />
            </a>
        </div>
    );
}
export default DeletePatien;