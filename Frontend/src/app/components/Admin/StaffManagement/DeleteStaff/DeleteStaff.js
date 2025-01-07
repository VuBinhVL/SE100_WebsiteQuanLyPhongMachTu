import React, { useState } from "react";
import "./DeleteStaff.css"
import { MdDelete } from "react-icons/md";
import { fetchDelete } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showDeleteMessageBox } from "../../../MessageBox/DeleteMesssageBox/showDeleteMessageBox";
function DeleteStaff(props) {
    // const [isVisibility, setIsVisibility] = useState(false);
    const { setListStaff, listStaff } = props;
    const { item } = props;
    const handleDelete = () => {
        showDeleteMessageBox("Bạn có chắc muốn xóa không", () => {
            const uri = `/api/admin/quan-li-nhan-vien/delete?id=${item.id}`;
            fetchDelete(
                uri, "",
                (sus) => {
                    // console.log(">>>>>>.check sus", sus)
                    showSuccessMessageBox(sus.message)
                    const newStaff = listStaff.filter((newItem) => newItem.id !== item.id);
                    setListStaff(newStaff)
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
        <div className="Delete_Staff d-inline">
            <a href="#">
                <MdDelete onClick={handleDelete} className="icon_delete icon_action" />
            </a>
        </div>
    );
}
export default DeleteStaff;