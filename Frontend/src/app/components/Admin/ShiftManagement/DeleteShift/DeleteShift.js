import React, { useState } from "react";
import "./DeleteShift.css"
import { MdDelete } from "react-icons/md";
import { fetchDelete } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showDeleteMessageBox } from "../../../MessageBox/DeleteMesssageBox/showDeleteMessageBox";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";
function DeleteShift(props) {
    // const [isVisibility, setIsVisibility] = useState(false);
    const { setListShift, listShift } = props;
    const { item } = props;
    const handleDelete = () => {
        showDeleteMessageBox("Bạn có chắc muốn xóa không", () => {
            if (item.bacSiKham) {
                showErrorMessageBox("Không thể xóa ca khám vì đã có bác sĩ đăng kí")
                return;
            }
            const uri = `/api/admin/quan-li-ca-kham/delete?id=${item.id}`;
            fetchDelete(
                uri, "",
                (sus) => {
                    // console.log(">>>>>>.check sus", sus)
                    showSuccessMessageBox(sus.message)
                    const newShift = listShift.filter((newItem) => newItem.id !== item.id);
                    setListShift(newShift)
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
        <div className="Delete_Shift d-inline">
            <a href="#">
                <MdDelete onClick={handleDelete} className="icon_delete icon_action" />
            </a>
        </div>
    );
}
export default DeleteShift;