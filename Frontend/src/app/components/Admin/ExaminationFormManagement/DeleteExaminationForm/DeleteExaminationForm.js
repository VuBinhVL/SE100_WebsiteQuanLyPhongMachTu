import React, { useState } from "react";
import "./DeleteExaminationForm.css"
import { MdDelete } from "react-icons/md";
import { fetchDelete } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showDeleteMessageBox } from "../../../MessageBox/DeleteMesssageBox/showDeleteMessageBox";
function DeleteExaminationForm(props) {
    // const [isVisibility, setIsVisibility] = useState(false);
    const { setListExaminationForm, listExaminationForm } = props;
    const { item } = props;
    const handleDelete = () => {
        showDeleteMessageBox("Bạn có chắc muốn xóa không", () => {
            const uri = `/api/admin/quan-li-benh-nhan/delete?id=${item.id}`;
            fetchDelete(
                uri, "",
                (sus) => {
                    // console.log(">>>>>>.check sus", sus)
                    showSuccessMessageBox(sus.message)
                    const newlistExaminationForm = listExaminationForm.filter((newItem) => newItem.id !== item.id);
                    setListExaminationForm(newlistExaminationForm)
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
export default DeleteExaminationForm;